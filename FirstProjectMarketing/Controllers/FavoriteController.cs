using FirstProjectMarketing.Models;
using FirstProjectMarketing.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FirstProjectMarketing.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IMarketingRepository<Firstfavorite> favRepository;
        public FavoriteController(IMarketingRepository<Firstfavorite> favRepository)
        {
            this.favRepository = favRepository;
            
        }
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId==null)
            {
                TempData["msgCart"] = "<script>alert('Please login first');</script>";
                return RedirectToAction("Index", "Home");
            }
            var fav = favRepository.List().Where(p => p.UserId == userId);
            return View(fav);
        }
        [HttpPost]
        public JsonResult AddFavorite(int productId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Json(false);
            }
            var fav = favRepository.List().Where(x=>x.UserId==userId && x.ProductId == productId).ToList();
            if (fav.Count() != 0)
            {
                return Json(false);
            }
            Firstfavorite favorite = new Firstfavorite();
            favorite.ProductId = productId;
            favorite.UserId = userId;
            favRepository.Add(favorite);
            return Json(true);
        }
        [HttpPost]
        public JsonResult DeleteFavorite(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Json(false);
            }
            //var product = favRepository.List().Where(x => x.ProductId == productId && x.UserId == userId).SingleOrDefault();
            favRepository.Delete(id);
            return Json(true);
        }
    }
}
