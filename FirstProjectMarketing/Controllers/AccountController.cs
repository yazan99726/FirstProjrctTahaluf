using FirstProjectMarketing.Global;
using FirstProjectMarketing.Models;
using FirstProjectMarketing.Models.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FirstProjectMarketing.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMarketingRepository<Firstuser> userRepository;
        private readonly IMarketingRepository<Firstlogin> loginRepository;
        private readonly IMarketingRepository<Firstcart> cartRepository;
        private readonly IWebHostEnvironment hosting;

        public AccountController(IMarketingRepository<Firstuser> userRepository, IMarketingRepository<Firstlogin> loginRepository, IMarketingRepository<Firstcart> cartRepository,
            IWebHostEnvironment hosting)
        {
            this.userRepository = userRepository;
            this.loginRepository = loginRepository;
            this.cartRepository = cartRepository;
            this.hosting = hosting;
        }
        public IActionResult Index()
        {
            return View();
        }


        
        [HttpPost]
        public IActionResult Login(string userName, string password, string ReturnUrl)
        {
            if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                TempData["msg"] = "<script>alert('Please enter userName and Password');</script>";
                return RedirectToAction("Index", "Home");
            }
            var user = loginRepository.List().SingleOrDefault(u => u.Username == userName && u.Password == password);
            if (user != null)
            {

                switch (user.RoleId)
                {
                    case 1:
                        {
                            HttpContext.Session.SetInt32("UserId", Convert.ToInt32(user.Userid));
                            HttpContext.Session.SetInt32("RoleId", Convert.ToInt32(user.RoleId));
                            HttpContext.Session.SetString("UserName", (string)user.User.Firstname + " " + user.User.Lastname);
                            HttpContext.Session.SetString("UserImage", (string)user.User.Image);
                            return RedirectToAction("Dashboard", "AdminDashboard");
                        }
                    case 21:
                        {
                            HttpContext.Session.SetInt32("UserId", Convert.ToInt32(user.Userid));
                            HttpContext.Session.SetString("UserName", (string)user.User.Firstname + " " + user.User.Lastname);
                            if (user.User.Image == null)
                            {
                                HttpContext.Session.SetString("UserImage", "userimageprofilenotfound.jpg");
                            }
                            else
                            {
                                HttpContext.Session.SetString("UserImage", (string)user.User.Image);
                            }

                            return RedirectToAction("Index", "Home");
                        }
                }
            }
            else
            {
                TempData["msglogin"] = "<script>alert('the userName or Password is not correct');</script>";
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");

          

        }


            public IActionResult Register()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserLogin userLogin)
        {
            var users = userRepository.List().Where(u => u.Username == userLogin.user.Username).ToList();
            if (userLogin==null)
            {
                return View();
            }
            else if (users.Count>0)
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



                return RedirectToAction("Index", "Home");
            }
            
        }
        
        
        public IActionResult ProfileCustomer()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = userRepository.find(Convert.ToDecimal(userId));
            
            return View(user);
        }
        [HttpPost]
        public IActionResult ProfileCustomer(decimal id ,Firstuser profile)
        {
            if (id != profile.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (profile == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    UploadFile uploadFile = new UploadFile(hosting);
                    profile.Image = uploadFile.imageFile(profile.imgfile, "/imgUsers");
                    userRepository.Update(id, profile);
                    HttpContext.Session.SetString("UserName", (string)profile.Firstname +" "+ profile.Lastname);
                    if (profile.Image == null)
                    {
                        HttpContext.Session.SetString("UserImage", "userimageprofilenotfound.jpg");
                    }
                    else
                    {
                        HttpContext.Session.SetString("UserImage", (string)profile.Image);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return View(profile);
            }
            return View(profile);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            DataConst.CartNumber = 0;
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public IActionResult ChangePassword(string oldPass, string newPass)
        {
            var login = loginRepository.List().SingleOrDefault(x=>x.Userid== Convert.ToDecimal(HttpContext.Session.GetInt32("UserId")));
            if (newPass == null || login==null ||oldPass==null)
            {
                TempData["msg"] = "<script>alert('old Password and new password cannot  be empty');</script>";
                return RedirectToAction("Index", "Home");
            }
            else if (login.Password != oldPass)
            {
                TempData["msg"] = "<script>alert('old password is not correct');</script>";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                login.Password = newPass;
                loginRepository.Update(login.LoginId, login);
                TempData["msg"] = "<script>alert('Password changed successfully');</script>";
                return RedirectToAction("Index", "Home");
            }
            

        }
    }
}
//[HttpPost]
//public IActionResult Login(string userName, string password, string ReturnUrl)
//{
//    var result = loginRepository.List().Where(x=>x.Username==userName && x.Password==password).SingleOrDefault();
//    if (result == null)
//        return null;
//    else
//    {
//        //key and algorthem and data
//        //generate token
//        var handler = new JwtSecurityTokenHandler();
//        //algorthem enchode(key in singniger)
//        var tokenKey = Encoding.ASCII.GetBytes("hello oday ,hello roqaya algorthem enchode(key in singniger)");
//        //contines key and algorthem and data 
//        var tokendescriptor = new SecurityTokenDescriptor()
//        {
//            //stored data in claim
//            Subject = new ClaimsIdentity(new Claim[]
//            {
//                new Claim(ClaimTypes.Name, result.Username),
//                new Claim(ClaimTypes.Role,result.RoleId.ToString()),
//                new Claim(ClaimTypes.PrimarySid,result.Userid.ToString()),
//            }),
//            Expires = DateTime.UtcNow.AddHours(1),
//            //الية التشفير
//            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
//        };
//        //create and generate token
//        var token = handler.CreateToken(tokendescriptor);
//        var x= handler.WriteToken(token);
//        HttpContext.Session.SetString("token", (string)x);
//        return RedirectToAction(x);
//    }
//}
//public IActionResult Login()
//{
//    return View();
//}

//ClaimsIdentity identity = null;
//bool isAuthenticated = false;
//if (user != null && user.RoleId == 21)
//{
//    identity = new ClaimsIdentity(new[] {
//    new Claim(ClaimTypes.Name,userName),
//    new Claim(ClaimTypes.Role,"customar")
//    }, CookieAuthenticationDefaults.AuthenticationScheme
//    );
//    isAuthenticated = true;
//    HttpContext.Session.SetInt32("UserId", Convert.ToInt32(user.Userid));
//    HttpContext.Session.SetString("UserName", (string)user.User.Firstname + " " + user.User.Lastname);
//    if (user.User.Image == null)
//    {
//        HttpContext.Session.SetString("UserImage", "userimageprofilenotfound.jpg");
//    }
//    else
//    {
//        HttpContext.Session.SetString("UserImage", (string)user.User.Image);
//    }
//}
//if (user != null && user.RoleId == 1)
//{
//    identity = new ClaimsIdentity(new[] {
//    new Claim(ClaimTypes.Name,userName),
//    new Claim(ClaimTypes.Role, user.Role.RoleName)
//    }, CookieAuthenticationDefaults.AuthenticationScheme
//    );
//    isAuthenticated = true;
//    HttpContext.Session.SetInt32("UserId", Convert.ToInt32(user.Userid));
//    HttpContext.Session.SetString("UserName", (string)user.User.Firstname + " " + user.User.Lastname);
//    HttpContext.Session.SetString("UserImage", (string)user.User.Image);
//    return RedirectToAction("Dashboard", "AdminDashboard");

//}
//if (isAuthenticated)
//{
//    var Principale = new ClaimsPrincipal(identity);
//    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Principale);
//    if (user.RoleId == 1)
//    {
//        return RedirectToAction("Dashboard", "AdminDashboard");
//    }
//    return RedirectToAction("Index", "Home");
//}