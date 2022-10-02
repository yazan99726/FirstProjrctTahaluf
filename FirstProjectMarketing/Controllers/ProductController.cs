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
    public class ProductController : Controller
    {
        private readonly IMarketingRepository<Firstproduct> productRepository;
        private readonly IMarketingRepository<Firstcatigory> catigoryRepository;
        private readonly IWebHostEnvironment hosting;

        public ProductController(IMarketingRepository<Firstproduct> productRepository, IMarketingRepository<Firstcatigory> CatigoryRepository, IWebHostEnvironment hosting)
        {
            this.productRepository = productRepository;
            catigoryRepository = CatigoryRepository;
            this.hosting = hosting;
        }

        // GET: Product
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            return View(productRepository.List());
        }

        // GET: Product/Details/5
        public IActionResult Details(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firstproduct = productRepository.find(id);
            if (firstproduct == null)
            {
                return NotFound();
            }

            return View(firstproduct);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            ViewData["CatigoryId"] = new SelectList(catigoryRepository.List(), "CatigoryId", "CatigoryName");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("ProductId,ProductName,ProductDicription,ProductPrice,ProductWholesaleprice,ProductImage1,ProductImage2,ProductImage3,ProductQuantity,CatigoryId,imgfile1,imgfile2,imgfile3")] Firstproduct firstproduct)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            UploadFile uploadFile = new UploadFile(hosting);
            var image1 = uploadFile.imageFile(firstproduct.imgfile1, "/imgProduct");
            var image2 = uploadFile.imageFile(firstproduct.imgfile2, "/imgProduct");
            var image3 = uploadFile.imageFile(firstproduct.imgfile3, "/imgProduct");
            if (ModelState.IsValid)
            {
                firstproduct.ProductImage1 = image1;
                firstproduct.ProductImage2 = image2;
                firstproduct.ProductImage3 = image3;
                productRepository.Add(firstproduct);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatigoryId"] = new SelectList(catigoryRepository.List(), "CatigoryId", "CatigoryName", firstproduct.CatigoryId);
            return View(firstproduct);
        }

        // GET: Product/Edit/5
        public IActionResult Edit(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firstproduct = productRepository.find(id);
            if (firstproduct == null)
            {
                return NotFound();
            }
            ViewData["CatigoryId"] = new SelectList(catigoryRepository.List(), "CatigoryId", "CatigoryName", firstproduct.CatigoryId);
            return View(firstproduct);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(decimal id, [Bind("ProductId,ProductName,ProductDicription,ProductPrice,ProductWholesaleprice,ProductImage1,ProductImage2,ProductImage3,ProductQuantity,CatigoryId,imgfile1,imgfile2,imgfile3")] Firstproduct firstproduct)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            if (id != firstproduct.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UploadFile uploadFile = new UploadFile(hosting);
                    var image1 = uploadFile.imageFile(firstproduct.imgfile1, firstproduct.ProductImage1, "/imgProduct");
                    var image2 = uploadFile.imageFile(firstproduct.imgfile2, firstproduct.ProductImage2, "/imgProduct");
                    var image3 = uploadFile.imageFile(firstproduct.imgfile3, firstproduct.ProductImage3, "/imgProduct");

                    firstproduct.ProductImage1 = image1;
                    firstproduct.ProductImage2 = image2;
                    firstproduct.ProductImage3 = image3;
                    productRepository.Update(id,firstproduct);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatigoryId"] = new SelectList(catigoryRepository.List(), "CatigoryId", "CatigoryName", firstproduct.CatigoryId);
            return View(firstproduct);
        }

        // GET: Product/Delete/5
        public IActionResult Delete(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var firstproduct = productRepository.find(id);
            if (firstproduct == null)
            {
                return NotFound();
            }

            return View(firstproduct);
        }

        // POST: Product/Delete/5
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
            var product = productRepository.find(id);
            UploadFile uploadFile = new UploadFile(hosting);
            uploadFile.DeleteFile(product.ProductImage1, "/imgProduct");
            uploadFile.DeleteFile(product.ProductImage2, "/imgProduct");
            uploadFile.DeleteFile(product.ProductImage3, "/imgProduct");
            productRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool FirstproductExists(decimal id)
        //{
        //    return _context.Firstproducts.Any(e => e.ProductId == id);
        //}
    }
}
