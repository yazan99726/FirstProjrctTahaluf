using FirstProjectMarketing.Global;
using FirstProjectMarketing.Models;
using FirstProjectMarketing.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProjectMarketing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMarketingRepository<Firsthome> homeRepository;
        private readonly IMarketingRepository<Firsthomeimage> backgroundRepository;
        private readonly IMarketingRepository<Firstproduct> productRepository;
        private readonly IMarketingRepository<Firstdiscount> discountRepository;
        private readonly IMarketingRepository<Firstuser> userRepository;
        private readonly IMarketingRepository<Firsttestimonial> testRepository;
        private readonly IMarketingRepository<Firstcartproduct> cartProductRepository;

        public HomeController(ILogger<HomeController> logger, IMarketingRepository<Firsthome> homeRepository, IMarketingRepository<Firsthomeimage> backgroundRepository,
             IMarketingRepository<Firstproduct> productRepository, IMarketingRepository<Firstdiscount> discountRepository,
             IMarketingRepository<Firstuser> userRepository, IMarketingRepository<Firsttestimonial> testRepository,
            IMarketingRepository<Firstcartproduct> cartProductRepository)
        {
            _logger = logger;
            this.homeRepository = homeRepository;
            this.backgroundRepository = backgroundRepository;
            this.productRepository = productRepository;
            this.discountRepository = discountRepository;
            this.userRepository = userRepository;
            this.testRepository = testRepository;
            this.cartProductRepository = cartProductRepository;
        }

        public IActionResult Index()
        {
            var id = homeRepository.List().Max(h => h.Id);
            var home = homeRepository.find(id);
            ViewData["name"] = home.Projectname;
            ViewData["logo"] = home.Logo;
            ViewData["discription"] = home.Discription;
            HttpContext.Session.SetString("AppName", (string)home.Projectname);
            HttpContext.Session.SetString("AppLogo", (string)home.Logo);
            HttpContext.Session.SetString("AppDiscription", (string)home.Discription);

            ViewBag.userName = HttpContext.Session.GetString("UserName");
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                var user = userRepository.find(Convert.ToDecimal(userId));
                DataConst.CartNumber = cartProductRepository.List().Where(c => c.Cart.UserId == userId).Count();
                //HttpContext.Session.SetString("UserImage", (string)user.Image);
            }
            

            var slider = backgroundRepository.List();
            var product = productRepository.List();
            var productDiscount = discountRepository.List();
            var testimonial = testRepository.List().Where(t => t.Status == "T");
            var tuple = Tuple.Create<IEnumerable<Firsthomeimage>, IEnumerable<Firstproduct>, IEnumerable<Firstdiscount>, IEnumerable<Firsttestimonial>>(slider, product, productDiscount, testimonial);
            return View(tuple);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
