using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstProjectMarketing.Models;
using FirstProjectMarketing.Models.Repository;
using Microsoft.AspNetCore.Http;

namespace FirstProjectMarketing.Controllers
{
    public class StoresController : Controller
    {
        private readonly IMarketingRepository<Firststore> storeRepository;

        public StoresController(IMarketingRepository<Firststore> storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        // GET: Stores
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            return View(storeRepository.List());
        }

        // GET: Stores/Details/5
        public IActionResult Details(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var store = storeRepository.find(id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StoreId,StoreName,StoreDiscription,StoreFloorNumber")] Firststore firststore)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            if (ModelState.IsValid)
            {
                storeRepository.Add(firststore);
                return RedirectToAction(nameof(Index));
            }
            return View(firststore);
        }

        // GET: Stores/Edit/5
        public IActionResult Edit(decimal id)
        {

            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var store = storeRepository.find(id);
            if (store == null)
            {
                return NotFound();
            }
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(decimal id, [Bind("StoreId,StoreName,StoreDiscription,StoreFloorNumber")] Firststore firststore)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            if (id != firststore.StoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    storeRepository.Update(id, firststore);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!FirststoreExists(firststore.StoreId))
                    //{
                        return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(firststore);
        }

        // GET: Stores/Delete/5
        public IActionResult Delete(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var storr = storeRepository.find(id);
            if (storr == null)
            {
                return NotFound();
            }

            return View(storr);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            storeRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool FirststoreExists(decimal id)
        //{
        //    return _context.Firststores.any(e => e.StoreId == id);
        //}
    }
}
