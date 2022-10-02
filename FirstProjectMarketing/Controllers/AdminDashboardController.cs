using FirstProjectMarketing.Global;
using FirstProjectMarketing.Models;
using FirstProjectMarketing.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FirstProjectMarketing.Controllers
{
    
    public class AdminDashboardController : Controller
    {
        private readonly IMarketingRepository<Firstorderproduct> oderProductRepository;
        private readonly IMarketingRepository<Firstuser> userRepository;
        private readonly IWebHostEnvironment hosting;
        private readonly IMarketingRepository<Firsttestimonial> testRepositoy;
        private readonly IMarketingRepository<Firstcontactu> contactRepository;
        private List<Firstorderproduct> sales;
        public AdminDashboardController(IMarketingRepository<Firstorderproduct> oderProductRepository, IMarketingRepository<Firstuser> userRepository, IWebHostEnvironment hosting,
           IMarketingRepository<Firsttestimonial> testRepositoy , IMarketingRepository<Firstcontactu> contactRepository)
        {
            this.oderProductRepository = oderProductRepository;
            this.userRepository = userRepository;
            this.hosting = hosting;
            this.testRepositoy = testRepositoy;
            this.contactRepository = contactRepository;
            sales = oderProductRepository.List().ToList();
        }

        
        public IActionResult Dashboard()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null||roleid==null||roleid!=1)
            {
                
                return RedirectToAction("Privacy", "Home");
            }

            CalculateProfitTotalSales(this.sales.Where(x => x.Order.OrderPaydate.Value.Month == DateTime.UtcNow.Month).ToList());
            
            //----------Chart-------------
            List<decimal> chart1 = new List<decimal>();
            string [] labelechart1 = { "jan", "feb", "march", "april", "may", "jun", "jul", "agu", "sep", "oct", "nov", "dec" };
            //List<string> labelechart3 = sales.Select(c=>c.Order.OrderPaydate.Value.ToString("MMM",CultureInfo.InvariantCulture).ToLower()).Distinct().ToList();
            List<string> labelechart2 = new List<string>();

            foreach (var item in labelechart1)
            {
                decimal c = 0;
                foreach (var item2 in sales.Where(x=>x.Order.OrderPaydate.Value.ToString("MMM", CultureInfo.InvariantCulture).ToLower() ==item))
                {
                    c= c + (item2.Quantity * item2.Product.ProductPrice);
                    
                }
                chart1.Add(c);
                labelechart2.Add(item + " :" + c.ToString());
            }
            
            
            ViewBag.chart = JsonConvert.SerializeObject(chart1);
            ViewBag.lablechart1 = JsonConvert.SerializeObject(labelechart2);


            List<decimal?> total = new List<decimal?>();
            List<string> lab = new List<string>();
            List<string> lab2 = new List<string>();
            lab = sales.Select(x => x.Product.Catigory.Store.StoreName).Distinct().ToList();
            decimal? x = 0;
            foreach (var item in lab)
            {
                foreach (var item2 in sales.Where(x => x.Order.OrderPaydate.Value.Month == DateTime.UtcNow.Month && x.Product.Catigory.Store.StoreName==item))
                {
                    x= x+((item2.Quantity * item2.Product.ProductPrice) - (item2.Quantity * item2.Product.ProductWholesaleprice));
                }
                total.Add(x);
                lab2.Add(item +" :"+ x.ToString());
                x = 0;
            }
            
            
            ViewBag.lablePieChart = JsonConvert.SerializeObject(lab2);
            ViewBag.totalPieChart = JsonConvert.SerializeObject(total);
            //----------End Chart-------------------


            var numberOfUser = userRepository.List().Count();//.Where(x=>x.Firstlogins.Any(i=>i.RoleId==1)).Count();
            ViewBag.NumberOfUser = numberOfUser;

            var testimonial = testRepositoy.List();
            ViewBag.contact = contactRepository.List();
            ViewBag.countMessage = contactRepository.List().Count();
            DataConst.contactus = contactRepository.List().ToList();
            var tuble = Tuple.Create<IEnumerable<Firstorderproduct>, IEnumerable<Firsttestimonial>>(sales, testimonial);
            return View(tuble);
        }
        
        public IActionResult Sales()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            CalculateProfitTotalSales(sales.ToList());
            return View(sales);
        }
        [HttpPost]
        public IActionResult Sales(DateTime? startDate, DateTime? endDate)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            //var result =  oderProductRepository.List();

            if (startDate == null && endDate == null)
            {
                CalculateProfitTotalSales(sales);
                return View(sales);
            }
            else if (startDate != null && endDate == null)
            {
                var data = sales.Where(x => x.Order.OrderPaydate.Value.Date == startDate).ToList();
                CalculateProfitTotalSales(data);
                return View(data);
            }
            else if (startDate != null && endDate != null)
            {
                var data = sales.Where(x => x.Order.OrderPaydate.Value.Date >= startDate && x.Order.OrderPaydate.Value.Date <= endDate).ToList();
                CalculateProfitTotalSales(data);
                return View(data);
            }
            else
            {
                return View(sales);
            }
        }

        public IActionResult MonthlySales()
        {
            var data = oderProductRepository.List().Where(x=>x.Order.OrderPaydate.Value.Year== DateTime.UtcNow.Year && x.Order.OrderPaydate.Value.Month == DateTime.UtcNow.Month).ToList();
            CalculateProfitTotalSales(data);
            return View("Sales", data);
        }
        public IActionResult yearlySales()
        {
            var data = oderProductRepository.List().Where(x => x.Order.OrderPaydate.Value.Year == DateTime.UtcNow.Year).ToList();
            CalculateProfitTotalSales(data);
            return View("Sales", data);
        }
        public IActionResult dailySales()
        {
            var data = oderProductRepository.List().Where(x => x.Order.OrderPaydate.Value.Year == DateTime.UtcNow.Year &&
                                                          x.Order.OrderPaydate.Value.Month == DateTime.UtcNow.Month &&
                                                          x.Order.OrderPaydate.Value.Day == DateTime.UtcNow.Day).ToList();
            CalculateProfitTotalSales(data);
            return View("Sales", data);
        }



        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var user = userRepository.List().SingleOrDefault(u => u.UserId == userId);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(decimal id, [Bind("UserId,Firstname,Lastname,Username,Phone,Image,Country,City,Street,imgfile")] Firstuser user)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    UploadFile uploadFile = new UploadFile(hosting);
                    var image3 = uploadFile.imageFile(user.imgfile, user.Image, "/imgUsers");
                    user.Image = image3;
                    userRepository.Update(id, user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return View(user);
            }
            return View(user);
        }


        //--------- calculate profit and total sales
        private void CalculateProfitTotalSales(List<Firstorderproduct> result)
        {
            decimal profits = 0;
            decimal totalSales = 0;
            foreach (var item in result)
            {
                profits = profits + ((item.Quantity * item.Product.ProductPrice) - (item.Quantity * item.Product.ProductWholesaleprice));
                totalSales = totalSales + (item.Quantity * item.Product.ProductPrice);
            }
            ViewBag.profits = profits;
            ViewBag.totalSales = totalSales;
        }


        public IActionResult SendEmail()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            ViewBag.userEmail = new SelectList(userRepository.List(), "Username", "Username");
            return View();
        }
        [HttpPost]
        public IActionResult SendEmail(string Message, string uEmail)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var user= userRepository.List().Where(e=>e.Username==uEmail).FirstOrDefault();

            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("yazantayem997@outlook.com","120150622025y-");
            client.EnableSsl = true;
            client.Credentials = credentials;

            try
            {
                var mail = new MailMessage("yazantayem997@outlook.com", uEmail);
                mail.Subject = "this email send from SortPrice dev by yazan";
                mail.Body = Message;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return RedirectToAction("Dashboard", "AdminDashboard");
            
        }


        public IActionResult ProfitsEachStore()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            CalculateProfitTotalSales(sales);
            return View(sales);
        }
        [HttpPost]
        public IActionResult ProfitsEachStore(string StoreName)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleid = HttpContext.Session.GetInt32("RoleId");
            if (userId == null || roleid == null || roleid != 1)
            {

                return RedirectToAction("Privacy", "Home");
            }
            var profitDetails = sales.Where(s => s.Product.Catigory.Store.StoreName == StoreName).ToList();
            ViewBag.ProfitDetails = profitDetails;
            ViewBag.StoreName = StoreName;
            decimal? totalProfitToStore = 0;
            foreach (var item in profitDetails)
            {
                totalProfitToStore += ((item.Quantity * item.Product.ProductPrice) - (item.Quantity * item.Product.ProductWholesaleprice));
            }
            ViewBag.totalProfitToStore = totalProfitToStore;
            CalculateProfitTotalSales(sales);
            return View(sales);
        }
    }
}
