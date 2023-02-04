using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SM.Business.Interfaces;
using SM.Business.Models;
using SM.Data.Models;

namespace SM.WebApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        // GET: ProductController
        public ActionResult Index(int storeId, string? search = "")
        {
            ViewBag.StoreId = storeId;
            ViewBag.SearchTerm = search;

            var products = _productService.ProductsForStore(storeId, search);
            return View(products);
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
