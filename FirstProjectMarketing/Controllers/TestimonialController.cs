using FirstProjectMarketing.Models;
using FirstProjectMarketing.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstProjectMarketing.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly IMarketingRepository<Firsttestimonial> testRepository;

        public TestimonialController(IMarketingRepository<Firsttestimonial> testRepository)
        {
            this.testRepository = testRepository;
        }
        // GET: TestimonialController
        public ActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var test = testRepository.List();
            return View(test);
        }

        // GET: TestimonialController/Details/5
        public ActionResult Details(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            return View(testRepository.find(id));
        }

        // GET: TestimonialController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestimonialController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Firsttestimonial testimonial)
        {
            try
            {
                var userid = HttpContext.Session.GetInt32("UserId");
                if (testimonial == null || userid == null)
                {
                    return View();
                }
                testimonial.UserId = userid;
                testimonial.Status = "F";
                testRepository.Add(testimonial);
                return RedirectToAction("Index", "Home");
                
            }
            catch
            {
                return View();
            }
        }

        // GET: TestimonialController/Edit/5
        public ActionResult Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var test = testRepository.find(id);
            return View(test);
        }

        // POST: TestimonialController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Firsttestimonial testimonial)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var roleid = HttpContext.Session.GetInt32("RoleId");
                if (userId == null || roleid == null || roleid != 1)
                {

                    return RedirectToAction("Privacy", "Home");
                }
                if (id == 0)
                {
                    return RedirectToAction("Dashboard", "AdminDashboard");
                }
                if (testimonial == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                testimonial.Status = "T";
                testRepository.Update(id, testimonial);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(testimonial);
            }
        }

        // GET: TestimonialController/Delete/5
        public ActionResult Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            return View(testRepository.find(id));
        }

        // POST: TestimonialController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Firsttestimonial testimonial)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var roleid = HttpContext.Session.GetInt32("RoleId");
                if (userId == null || roleid == null || roleid != 1)
                {

                    return RedirectToAction("Privacy", "Home");
                }
                testRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public IActionResult AccOrRej(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var test = testRepository.find(id);
            if (test.Status == "F")
            {
                test.Status = "T";
                testRepository.Update(id, test);
                return Json("F");
            }
            else if (test.Status=="T")
            {
                test.Status = "F";
                testRepository.Update(id, test);
                return Json("T");
            }
            return RedirectToAction("Dashboard", "AdminDashboard");

        }
    }
}
