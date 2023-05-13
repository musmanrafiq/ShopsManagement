using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SM.Business.Interfaces;
using SM.Business.Models;

namespace SM.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IStoreService _storeService;
        // cache
        private readonly IMemoryCache _memoryChache;


        public HomeController(IStoreService storeService, IMemoryCache memoryCache)
        {
            _storeService = storeService;
            _memoryChache = memoryCache;
        }

        // GET: StoreController
        public ActionResult Index()
        {
            var storeList = _memoryChache.Get<List<StoreModel>>("Stores");
            if(storeList is null)
            {
                storeList = _storeService.GetAll();
                _ = _memoryChache.Set("Stores", storeList);

            }
            return View(storeList);
        }

        // GET: StoreController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StoreController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: StoreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreModel model)
        {
            try
            {
                _storeService.Add(model);
                _memoryChache.Remove("Stores");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreController/Edit/5
        public ActionResult Edit(int id)
        {
            var storeModel = _storeService.GetById(id);
            return View(storeModel);
        }

        // POST: StoreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StoreModel model)
        {
            try
            {
                _storeService.Update(model);
                _memoryChache.Remove("Stores");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                _storeService.Delete(id);
                _memoryChache.Remove("Stores");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
