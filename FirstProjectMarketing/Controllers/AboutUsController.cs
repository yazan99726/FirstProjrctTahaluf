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
    public class AboutUsController : Controller
    {
        private readonly IMarketingRepository<Firstaboutu> aboutRepository;
        private readonly IMarketingRepository<Firstlogin> logRepository;
        //private readonly int userId;
        //private readonly Firstlogin user;
        public AboutUsController(IMarketingRepository<Firstaboutu> aboutRepository, IMarketingRepository<Firstlogin> logRepository)
        {
            this.aboutRepository = aboutRepository;
            this.logRepository = logRepository;
            //userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            //this.user = logRepository.List().Where(x=>x.Userid == userId).SingleOrDefault();
        }

        // GET: AboutUs
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            return View(aboutRepository.List());
        }

        public IActionResult AboutUsForCustomer()
        {
            var abt = aboutRepository.List();
            var about = abt.Where(x=>x.Id==(abt.Max(c=>c.Id))).SingleOrDefault();
            return View(about);
        }

        // GET: AboutUs/Details/5
        public IActionResult Details(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var aboutu = aboutRepository.find(id);
            if (aboutu == null)
            {
                return NotFound();
            }

            return View(aboutu);
        }

        // GET: AboutUs/Create
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

        // POST: AboutUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Discription")] Firstaboutu firstaboutu)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            if (ModelState.IsValid)
            {

                aboutRepository.Add(firstaboutu);
                return RedirectToAction(nameof(Index));
            }
            return View(firstaboutu);
        }

        // GET: AboutUs/Edit/5
        public IActionResult Edit(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firstaboutu = aboutRepository.find(id);
            if (firstaboutu == null)
            {
                return NotFound();
            }
            return View(firstaboutu);
        }

        // POST: AboutUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(decimal id, [Bind("Id,Discription")] Firstaboutu firstaboutu)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            if (id != firstaboutu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    aboutRepository.Update(id, firstaboutu);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(firstaboutu);
        }

        // GET: AboutUs/Delete/5
        public IActionResult Delete(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firstaboutu = aboutRepository.find(id);
            if (firstaboutu == null)
            {
                return NotFound();
            }

            return View(firstaboutu);
        }

        // POST: AboutUs/Delete/5
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
            aboutRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
