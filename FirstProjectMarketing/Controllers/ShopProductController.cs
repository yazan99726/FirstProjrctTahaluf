using FirstProjectMarketing.Models;
using FirstProjectMarketing.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstProjectMarketing.Controllers
{
    
    public class ShopProductController : Controller
    {
        private readonly IMarketingRepository<Firstproduct> productRepository;
        private readonly IMarketingRepository<FirstReviewProduct> reviewRepository;

        public ShopProductController(IMarketingRepository<Firstproduct> productRepository, IMarketingRepository<FirstReviewProduct> reviewRepository)
        {
            this.productRepository = productRepository;
            this.reviewRepository = reviewRepository;
        }
        public IActionResult AllProduct()
        {
            var product = productRepository.List();
            var category = product.Select(c => c.Catigory).Distinct().ToList();
            ViewBag.ProductName = product.Select(p => p.ProductName).Distinct().ToList();
            ViewBag.StoreName = product.Select(s => s.Catigory.Store).Distinct().ToList();

            var product_category = Tuple.Create<IEnumerable<Firstproduct>, IEnumerable<Firstcatigory>>(product, category);
            return View(product_category);
        }

        //-------------- Filter Data---------------
        public IActionResult Filter(string StoresName, string categoryName, string productName)
        {
            var product = productRepository.List();
            var category = product.Select(c => c.Catigory).Distinct().ToList();
            ViewBag.ProductName = product.Select(p => p.ProductName).Distinct().ToList();
            ViewBag.StoreName = product.Select(s => s.Catigory.Store).Distinct().ToList();

            if (StoresName!= null && categoryName == null && productName==null)
            {
                var productFilterByStoreName = productRepository.List().Where(c => c.Catigory.Store.StoreName == StoresName);
                var tuple = Tuple.Create<IEnumerable<Firstproduct>, IEnumerable<Firstcatigory>>(productFilterByStoreName, category);
                return View("AllProduct", tuple);
            }
            else if (StoresName == null && categoryName != null && productName == null )
            {
                var productFilterByCategoryName = productRepository.List().Where(c => c.Catigory.CatigoryName == categoryName);
                var tuple = Tuple.Create<IEnumerable<Firstproduct>, IEnumerable<Firstcatigory>>(productFilterByCategoryName, category);
                return View("AllProduct", tuple);
            }
            else if (StoresName == null && categoryName == null && productName != null)
            {
                var productFilterByProductName = productRepository.List().Where(c => c.ProductName == productName);
                var tuple = Tuple.Create<IEnumerable<Firstproduct>, IEnumerable<Firstcatigory>>(productFilterByProductName, category);
                return View("AllProduct", tuple);
            }
            else
            {
                var tuple = Tuple.Create<IEnumerable<Firstproduct>, IEnumerable<Firstcatigory>>(product, category);
                return View("AllProduct", tuple);
            }
                
        }

        //-------------- Product Details---------------
        [HttpGet("ShopProduct/DetailsProduct/{ProductId}")]
        public IActionResult DetailsProduct([FromRoute]int ProductId)
        {
            var review = reviewRepository.List().Where(r => r.ProductId == ProductId).ToList();
            if (review.Count() !=0)
            {
                ViewBag.review = review;
                ViewBag.rating = review.Sum(x => x.Rating) / review.Count();
                ViewBag.count = review.Count();
            }
            
            var product = productRepository.find(ProductId);
            return View(product);
        }

        [HttpPost]
        public JsonResult AddReview(int productId, int rating, string review)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            if (userId == 0)
            {
                return Json(false);
            }
            FirstReviewProduct reviewProduct = new FirstReviewProduct();
            reviewProduct.Id = 0;
            reviewProduct.User_Id = userId;
            reviewProduct.ProductId = productId;
            reviewProduct.Rating = rating;
            reviewProduct.Review = review;
            reviewRepository.Add(reviewProduct);
            return Json(true);
        }

        [HttpPost]
        public JsonResult AddToCart(Firstproduct firstproduct, int productId)
        {

            //var cartId = cartRepository.find(22);
            //Firstcartproduct cartProduct = new Firstcartproduct()
            //{
            //    ProductId = ProductId,
            //    CartId = cartId.CartId,
            //    CartQuantity = quantity,
            //};
            //ProductId = 1;
            //quantity = 1;
            //if (ProductId!=0&&quantity>0)
            //{
            return Json(firstproduct);
            //}
            //cartProductRepository.Add(cartProduct);
            //return new EmptyResult();

        }

        //public IActionResult FilterByStoreName(string StoresName)
        //{
        //    var product = productRepository.List();
        //    ViewBag.ProductName = product.Select(p => p.ProductName).Distinct().ToList();
        //    ViewBag.StoreName = product.Select(s => s.Catigory.Store).Distinct().ToList();

        //    var productFilterByStoreName = productRepository.List().Where(c => c.Catigory.Store.StoreName == StoresName);

        //    var category = product.Select(c => c.Catigory).Distinct().ToList();

        //    var tuple = Tuple.Create<IEnumerable<Firstproduct>, IEnumerable<Firstcatigory>>(productFilterByStoreName, category);

        //    return View("AllProduct", tuple);
        //}

        //public IActionResult FilterByCategoryName(string categoryName)
        //{
        //    var product = productRepository.List();
        //    ViewBag.ProductName = product.Select(p => p.ProductName).Distinct().ToList();
        //    ViewBag.StoreName = product.Select(s => s.Catigory.Store).Distinct().ToList();

        //    var productFilterByCategory = productRepository.List().Where(c => c.Catigory.CatigoryName == categoryName);

        //    var category = product.Select(c => c.Catigory).Distinct().ToList();

        //    var product_category = Tuple.Create<IEnumerable<Firstproduct>, IEnumerable<Firstcatigory>>(productFilterByCategory, category);
        //    return View("AllProduct", product_category);
        //}
        //public IActionResult FilterByProductName(string productName, string categoryName)
        //{
        //    var product = productRepository.List();
        //    ViewBag.ProductName = product.Select(p=>p.ProductName).Distinct().ToList();
        //    ViewBag.StoreName = product.Select(s => s.Catigory.Store).Distinct().ToList();

        //    var filterByProductName = productRepository.List().Where(c => c.ProductName == productName);
        //    var category = product.Select(c => c.Catigory).Distinct().ToList();
        //    var product_category = Tuple.Create<IEnumerable<Firstproduct>, IEnumerable<Firstcatigory>>(filterByProductName, category);
        //    return View("AllProduct", product_category);
        //}
    }
}
