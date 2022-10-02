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
    public class BackgroundImageHomeController : Controller
    {
        private readonly IMarketingRepository<Firsthomeimage> bacgroundRepository;
        private readonly IWebHostEnvironment hosting;

        public BackgroundImageHomeController(IMarketingRepository<Firsthomeimage> bacgroundRepository, IWebHostEnvironment hosting)
        {
            this.bacgroundRepository = bacgroundRepository;
            this.hosting = hosting;
        }

        // GET: BackgroundImageHome
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            return View(bacgroundRepository.List());
        }

        // GET: BackgroundImageHome/Details/5
        public IActionResult Details(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firsthomeimage = bacgroundRepository.find(id);
            if (firsthomeimage == null)
            {
                return NotFound();
            }

            return View(firsthomeimage);
        }

        // GET: BackgroundImageHome/Create
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

        // POST: BackgroundImageHome/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Image,imgfile")] Firsthomeimage firsthomeimage)
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
                firsthomeimage.Image = uploadFile.imageFile(firsthomeimage.imgfile, "/imgHome");
                bacgroundRepository.Add(firsthomeimage);
                return RedirectToAction(nameof(Index));
            }
            return View(firsthomeimage);
        }

        // GET: BackgroundImageHome/Edit/5
        public IActionResult Edit(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firsthomeimage = bacgroundRepository.find(id);
            if (firsthomeimage == null)
            {
                return NotFound();
            }
            return View(firsthomeimage);
        }

        // POST: BackgroundImageHome/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(decimal id, [Bind("Id,Image,imgfile")] Firsthomeimage firsthomeimage)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            if (id != firsthomeimage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UploadFile uploadFile = new UploadFile(hosting);
                    firsthomeimage.Image = uploadFile.imageFile(firsthomeimage.imgfile,firsthomeimage.Image, "/imgHome");
                    bacgroundRepository.Update(id, firsthomeimage);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(firsthomeimage);
        }

        // GET: BackgroundImageHome/Delete/5
        public IActionResult Delete(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firsthomeimage = bacgroundRepository.find(id);
            if (firsthomeimage == null)
            {
                return NotFound();
            }

            return View(firsthomeimage);
        }

        // POST: BackgroundImageHome/Delete/5
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
            var firsthomeimage = bacgroundRepository.find(id);
            UploadFile uploadFile = new UploadFile(hosting);
            uploadFile.DeleteFile(firsthomeimage.Image, "/imgHome");
            bacgroundRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

      
    }
}
