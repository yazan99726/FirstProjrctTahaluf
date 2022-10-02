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
    public class ContactUsController : Controller
    {
        private readonly IMarketingRepository<Firstcontactu> contactRepository;

        public ContactUsController(IMarketingRepository<Firstcontactu> contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        // GET: ContactUs
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            return View(contactRepository.List());
        }

        // GET: ContactUs/Details/5
        public IActionResult Details(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firstcontactu = contactRepository.find(id);
            if (firstcontactu == null)
            {
                return NotFound();
            }

            return View(firstcontactu);
        }

        // GET: ContactUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Firstname,Lastname,Email,Phone,Message")] Firstcontactu firstcontactu)
        {
            if (ModelState.IsValid)
            {
                contactRepository.Add(firstcontactu);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: ContactUs/Edit/5
        public IActionResult Edit(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firstcontactu = contactRepository.find(id);
            if (firstcontactu == null)
            {
                return NotFound();
            }
            return View(firstcontactu);
        }

        // POST: ContactUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(decimal id, [Bind("Id,Firstname,Lastname,Email,Phone,Message")] Firstcontactu firstcontactu)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            if (id != firstcontactu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contactRepository.Update(id, firstcontactu);
                }
                catch (DbUpdateConcurrencyException)
                {
                     return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(firstcontactu);
        }

        // GET: ContactUs/Delete/5
        public IActionResult Delete(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firstcontactu = contactRepository.find(id);
            if (firstcontactu == null)
            {
                return NotFound();
            }

            return View(firstcontactu);
        }

        // POST: ContactUs/Delete/5
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
            contactRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
