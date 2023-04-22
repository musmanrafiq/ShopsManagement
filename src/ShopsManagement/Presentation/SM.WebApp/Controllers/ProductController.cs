using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SM.Business.Interfaces;
using SM.Business.Models;
using System.IO;

namespace SM.WebApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        // hosting environment
        private readonly IWebHostEnvironment _webHostEnvironment;
        // product service
        private readonly IProductService _productService;
        // store service
        private readonly IStoreService _storeService;
        // cache injection
        private readonly IMemoryCache _memoryCache;

        public ProductController(IProductService productService, 
            IStoreService storeService,
            IMemoryCache memoryCache,
            IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _memoryCache = memoryCache;
            _storeService = storeService;
            _webHostEnvironment = webHostEnvironment;
        }


        // GET: ProductController
        public ActionResult Index(int storeId, string? search = "")
        {
            ViewBag.StoreId = storeId;
            ViewBag.SearchTerm = search;            
            ViewBag.StoreName = _storeService.GetStoreNameById(storeId);;

            //var productList = _memoryCache.Get<List<ProductModel>>($"Products_{search}");
            //if(productList is null)
            //{
              var  productList = _productService.ProductsForStore(storeId, search);
              //  _ = _memoryCache.Set<List<ProductModel>>($"Products_{search}", productList);
            //}
            
            return View(productList);
        }

        // GET: ProductController/Create
        public ActionResult Create(int? storeId)
        {
            ViewBag.StoreId = storeId;
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel model)
        {
            try
            {
                //get files from the request
                var files = Request.Form.Files;
                if (files.Any())
                {

                    string rootDirectoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
                    // making sure that Upload folder exists in wwwroot directory
                    if (!Directory.Exists(rootDirectoryPath))
                    {
                        Directory.CreateDirectory(rootDirectoryPath);
                    }
                    //var artifactModels = new List<ArtifactModel>();
                    foreach (var file in files)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string newFileNameWithpath = Path.Combine(rootDirectoryPath, fileName);
                        using FileStream fileStream = new FileStream(newFileNameWithpath, FileMode.Create);
                        file.CopyTo(fileStream);
                        var artifactModel = new ArtifactModel { Name = fileName, Path = @$"Uploads/{fileName}"};
                        model.Artifacts.Add(artifactModel);
                    }
                }
                //todo: need to check if that is useful
                model.Store = null;
                _productService.Add(model);
                return RedirectToAction(nameof(Index), new {storeId = model.StoreId});
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id, int storeId)
        {
            var product = _productService.GetAll().Where(x => x.Id == id).FirstOrDefault();

            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel model)
        {
            try
            {
                model.Store = null;
                _productService.Update(model);
                return RedirectToAction(nameof(Index), new { storeId = model.StoreId });
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id, int storeId)
        {
            _productService.Delete(id);
            return RedirectToAction(nameof(Index), new { storeId });
        }
    }
}
