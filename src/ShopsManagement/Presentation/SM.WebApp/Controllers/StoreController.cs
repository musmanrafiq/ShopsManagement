using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SM.Business.Interfaces;
using SM.Business.Models;

namespace SM.WebApp.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        // GET: StoreController
        public ActionResult Index()
        {
            var models = _storeService.GetAll();
            return View(models);
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
