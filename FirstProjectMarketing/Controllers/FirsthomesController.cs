using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstProjectMarketing.Models;
using FirstProjectMarketing.Models.Repository;
using Microsoft.AspNetCore.Hosting;
using FirstProjectMarketing.Global;
using Microsoft.AspNetCore.Http;

namespace FirstProjectMarketing.Controllers
{
    public class FirsthomesController : Controller
    {
        private readonly IMarketingRepository<Firsthome> homeRepository;
        private readonly IWebHostEnvironment hosting;

        public FirsthomesController(IMarketingRepository<Firsthome> homeRepository, IWebHostEnvironment hosting)
        {
            this.homeRepository = homeRepository;
            this.hosting = hosting;
        }

        // GET: Firsthomes
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            return View(homeRepository.List());
        }

        // GET: Firsthomes/Details/5
        public IActionResult Details(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firsthome = homeRepository.find(id);
            if (firsthome == null)
            {
                return NotFound();
            }

            return View(firsthome);
        }

        // GET: Firsthomes/Create
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

        // POST: Firsthomes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Projectname,Logo,Discription,imgfile")] Firsthome firsthome)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            if (ModelState.IsValid)
            {
                UploadFile uploadFile = new UploadFile(hosting);
                firsthome.Logo = uploadFile.imageFile(firsthome.imgfile, "/imgHome");
                homeRepository.Add(firsthome);
                return RedirectToAction(nameof(Index));
            }
            return View(firsthome);
        }

        // GET: Firsthomes/Edit/5
        public IActionResult Edit(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firsthome =homeRepository.find(id);
            if (firsthome == null)
            {
                return NotFound();
            }
            return View(firsthome);
        }

        // POST: Firsthomes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(decimal id, [Bind("Id,Projectname,Logo,Discription,imgfile")] Firsthome firsthome)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            if (id != firsthome.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UploadFile uploadFile = new UploadFile(hosting);
                    firsthome.Logo = uploadFile.imageFile(firsthome.imgfile, firsthome.Logo, "/imgHome");
                    homeRepository.Update(id,firsthome);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Firsthomes/Delete/5
        public IActionResult Delete(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firsthome = homeRepository.find(id);
            if (firsthome == null)
            {
                return NotFound();
            }

            return View(firsthome);
        }

        // POST: Firsthomes/Delete/5
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
            homeRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

       
    }
}
