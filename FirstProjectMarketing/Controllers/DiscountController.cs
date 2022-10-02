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
    public class DiscountController : Controller
    {
        private readonly IMarketingRepository<Firstdiscount> discountRepository;
        private readonly IMarketingRepository<Firstproduct> productRepository;
        private readonly SelectList _listOfProduct;
        public DiscountController(IMarketingRepository<Firstdiscount> discountRepository, IMarketingRepository<Firstproduct> productRepository)
        {
            this.discountRepository = discountRepository;
            this.productRepository = productRepository;
            this._listOfProduct = new SelectList(productRepository.List(), "ProductId", "ProductName");
        }

        // GET: Discount
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            return View(discountRepository.List());
        }

        // GET: Discount/Details/5
        public IActionResult Details(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firstdiscount = discountRepository.find(id);
            if (firstdiscount == null)
            {
                return NotFound();
            }

            return View(firstdiscount);
        }

        // GET: Discount/Create
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            ViewData["Productid"] = this._listOfProduct;
            return View();
        }

        // POST: Discount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DiscountId,DiscountStartdate,DiscountEnddate,DiscountPercentage,Productid")] Firstdiscount firstdiscount)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            if (ModelState.IsValid)
            {
                discountRepository.Add(firstdiscount);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Productid"] = this._listOfProduct;
            return View(firstdiscount);
        }

        // GET: Discount/Edit/5
        public  IActionResult Edit(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firstdiscount = discountRepository.find(id);
            if (firstdiscount == null)
            {
                return NotFound();
            }
            ViewData["Productid"] = this._listOfProduct;

            return View(firstdiscount);
        }

        // POST: Discount/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(decimal id, [Bind("DiscountId,DiscountStartdate,DiscountEnddate,DiscountPercentage,Productid")] Firstdiscount firstdiscount)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            if (id != firstdiscount.DiscountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    discountRepository.Update(id,firstdiscount);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!FirstdiscountExists(firstdiscount.DiscountId))
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
            ViewData["Productid"] = (this._listOfProduct, firstdiscount.Productid);
            return View(firstdiscount);
        }

        // GET: Discount/Delete/5
        public IActionResult Delete(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firstdiscount = discountRepository.find(id);
            if (firstdiscount == null)
            {
                return NotFound();
            }

            return View(firstdiscount);
        }

        // POST: Discount/Delete/5
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
            discountRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool FirstdiscountExists(decimal id)
        //{
        //    return _context.Firstdiscounts.Any(e => e.DiscountId == id);
        //}
    }
}
