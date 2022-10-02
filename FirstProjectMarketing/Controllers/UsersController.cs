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
    public class UsersController : Controller
    {
        private readonly IMarketingRepository<Firstuser> userRepository;
        private readonly IWebHostEnvironment hosting;
        private readonly IMarketingRepository<Firstcart> cartRepository;
        private readonly IMarketingRepository<Firstlogin> loginRepository;

        public UsersController(IMarketingRepository<Firstuser> userRepository, IWebHostEnvironment hosting, IMarketingRepository<Firstcart> cartRepository,
            IMarketingRepository<Firstlogin> loginRepository )
        {
            this.userRepository = userRepository;
            this.hosting = hosting;
            this.cartRepository = cartRepository;
            this.loginRepository = loginRepository;
        }

        // GET: Users
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            return View(userRepository.List());
        }

        // GET: Users/Details/5
        public IActionResult Details(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var user = userRepository.find(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
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

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var roleid = HttpContext.Session.GetInt32("RoleId");
                if (userId == null || roleid == null || roleid != 1)
                {

                    return RedirectToAction("Privacy", "Home");
                }
                var users = userRepository.List().Where(u => u.Username == userLogin.user.Username).ToList();
                if (userLogin == null)
                {
                    return View();
                }
                else if (users.Count > 0)
                {
                    return ViewBag.message = "this User already exist";
                }
                else
                {
                    UploadFile uploadFile = new UploadFile(hosting);
                    userLogin.user.Image = uploadFile.imageFile(userLogin.user.imgfile, "/imgUsers");
                    userRepository.Add(userLogin.user);

                    Firstlogin userLog = new Firstlogin()
                    {
                        RoleId = 21,
                        Username = userLogin.user.Username,
                        Password = userLogin.log.Password,
                        Userid = userLogin.user.UserId
                    };
                    loginRepository.Add(userLog);
                    Firstcart cart = new Firstcart()
                    {
                        UserId = userLogin.user.UserId,
                    };
                    cartRepository.Add(cart);
                }

            
        
                return RedirectToAction(nameof(Index));
            }
            return View(userLogin);
        }

        // GET: Users/Edit/5
        public IActionResult Edit(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var user = userRepository.find(id);
            if (user == null)
            {
                return NotFound();
            }
            UserLogin userLogin = new UserLogin();
            userLogin.user = user;
            return View(userLogin);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(decimal id, UserLogin userLogin)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            if (id != userLogin.user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UploadFile uploadFile = new UploadFile(hosting);
                    userLogin.user.Image = uploadFile.imageFile(userLogin.user.imgfile, "/imgUsers");
                    userRepository.Update(id, userLogin.user);
                    //if (string.IsNullOrEmpty(userLogin.log.Password))
                    //{
                    //    return RedirectToAction(nameof(Index));
                    //}
                    //else
                    //{
                    //    var login = loginRepository.List().SingleOrDefault(x=>x.Userid== userLogin.user.UserId);
                    //    login.Password = userLogin.log.Password;
                    //    loginRepository.Update(login.LoginId, login);
                    //}
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userLogin);
        }

        // GET: Users/Delete/5
        public IActionResult Delete(decimal id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var user = userRepository.find(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
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
            userRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
