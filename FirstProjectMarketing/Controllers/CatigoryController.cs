using FirstProjectMarketing.Models;
using FirstProjectMarketing.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace FirstProjectMarketing.Controllers
{
    public class CatigoryController : Controller
    {
        private readonly IMarketingRepository<Firstcatigory> catigoryRepository;
        private readonly IMarketingRepository<Firststore> storeRepository;

        public CatigoryController(IMarketingRepository<Firstcatigory> catigoryRepository, IMarketingRepository<Firststore> storeRepository)
        {
            this.catigoryRepository = catigoryRepository;
            this.storeRepository = storeRepository;
        }
        // GET: CatigoryController
        public ActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            return View(catigoryRepository.List());
        }

        // GET: CatigoryController/Details/5
        public ActionResult Details(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var catigory = catigoryRepository.find(id);
            return View(catigory);
        }

        // GET: CatigoryController/Create
        public ActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            ViewData["StoreId"] = new SelectList(storeRepository.List(), "StoreId", "StoreName");
            return View();
        }

        // POST: CatigoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Firstcatigory firstcatigory)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var roleid = HttpContext.Session.GetInt32("RoleId");
                if (userId == null || roleid == null || roleid != 1)
                {

                    return RedirectToAction("Privacy", "Home");
                }
                catigoryRepository.Add(firstcatigory);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CatigoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            ViewData["StoreId"] = new SelectList(storeRepository.List(), "StoreId", "StoreName");
            var catigory = catigoryRepository.find(id);
            return View(catigory);
        }

        // POST: CatigoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Firstcatigory firstcatigory)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var roleid = HttpContext.Session.GetInt32("RoleId");
                if (userId == null || roleid == null || roleid != 1)
                {

                    return RedirectToAction("Privacy", "Home");
                }
                catigoryRepository.Update(id, firstcatigory);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CatigoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var catigory = catigoryRepository.find(id);
            return View(catigory);
        }

        // POST: CatigoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var roleid = HttpContext.Session.GetInt32("RoleId");
                if (userId == null || roleid == null || roleid != 1)
                {

                    return RedirectToAction("Privacy", "Home");
                }
                catigoryRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       
    }
}
