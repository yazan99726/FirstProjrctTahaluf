using FirstProjectMarketing.Models;
using FirstProjectMarketing.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Text;
using FirstProjectMarketing.Global;

namespace FirstProjectMarketing.Controllers
{
    public class CartController : Controller
    {
        private readonly IMarketingRepository<Firstcartproduct> cartProductRepository;
        private readonly IMarketingRepository<Firstcart> cartRepository;
        private readonly IMarketingRepository<Firstorderproduct> orderProductRepository;
        private readonly IMarketingRepository<Firstorder> orderRepository;
        private readonly IMarketingRepository<Firstbank> bankRepositry;
        private readonly IMarketingRepository<Firstpayment> paymentRepository;
        private readonly IMarketingRepository<Firstproduct> productRepository;

        public CartController(IMarketingRepository<Firstcartproduct> cartProductRepository, IMarketingRepository<Firstcart> cartRepository,
           IMarketingRepository<Firstorderproduct> orderProductRepository, IMarketingRepository<Firstorder> orderRepository,
           IMarketingRepository<Firstbank> bankRepositry, IMarketingRepository<Firstpayment> paymentRepository, IMarketingRepository<Firstproduct> productRepository)
        {
            this.cartProductRepository = cartProductRepository;
            this.cartRepository = cartRepository;
            this.orderProductRepository = orderProductRepository;
            this.orderRepository = orderRepository;
            this.bankRepositry = bankRepositry;
            this.paymentRepository = paymentRepository;
            this.productRepository = productRepository;
        }
        [HttpPost]
        public JsonResult AddToCart(int id, int qti=1)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            if (userId==0)
            {
                return Json(false);
            }
            var checkProduct = productRepository.find(id);
            if (checkProduct.ProductQuantity < qti)
            {
                return Json("checkQuantity");
            }
            var cartId = cartRepository.find(userId);
            var cp = cartProductRepository.List().Where(x=>x.Cart.UserId == userId && x.CartId==cartId.CartId);
            var product = cp.Where(x=>x.ProductId==id).FirstOrDefault();
            if (id == 0 || qti == 0)
            {
                return Json(false);
            }
            else if (product != null)
            {
                var cartProduct = cp.FirstOrDefault(c => c.ProductId == id);
                cartProduct.CartQuantity = cartProduct.CartQuantity + qti;
                cartProductRepository.Update(cartProduct.Id, cartProduct);
                return Json("success");
            }
            else
            {
                Firstcartproduct cartProduct = new Firstcartproduct()
                {
                    ProductId = id,
                    CartId = cartId.CartId,
                    CartQuantity = qti,
                };
                cartProductRepository.Add(cartProduct);
                DataConst.CartNumber += 1;
                return Json(true);
            }
        }

        
        public IActionResult MyCart()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["msgCart"] = "<script>alert('Please login first');</script>";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var myCart = cartProductRepository.List().Where(c => c.Cart.UserId == userId && c.Product.ProductQuantity > 0).ToList();
                ViewBag.SubTotal = subTotal(myCart);
                //ViewBag.totalProductPrice = ViewBag.SubTotal;
                return View(myCart);
            }
            
        }

        public JsonResult IncDec(int id , int qti)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            var myCart = cartProductRepository.List().SingleOrDefault(c => c.Cart.UserId == userId && c.ProductId==id);
            myCart.CartQuantity = qti;
            cartProductRepository.Update(myCart.Id, myCart);
            var myCart2 = cartProductRepository.List().Where(c => c.Cart.UserId == userId && c.Product.ProductQuantity > 0).ToList();
            var sTotal = subTotal(myCart2);
            var totalProductPrice = myCart.Product.ProductPrice * qti;
            List<decimal> amm = new List<decimal>();
            amm.Add(sTotal);
            amm.Add(totalProductPrice);
            return Json(amm);
        }

        public IActionResult DeleteItemFromCart(int id)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            var item = cartProductRepository.List().SingleOrDefault(c => c.Cart.UserId == userId && c.ProductId == id);
            cartProductRepository.Delete(item.Id);
            DataConst.CartNumber -= 1;
            return RedirectToAction("MyCart", "Cart");
        }

        public IActionResult Checkout()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["msgCheckout"] = "<script>alert('Please login first');</script>";
                return RedirectToAction("Index", "Home");
            }
            var orderDetails = cartProductRepository.List().Where(c => c.Cart.UserId == userId && c.Product.ProductQuantity > 0).ToList();
            ViewBag.SubTotal = subTotal(orderDetails);
            return View(orderDetails);
        }
        [HttpPost]
        public IActionResult Checkout(Firstpayment payment)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            var orderDetails = cartProductRepository.List().Where(c => c.Cart.UserId == userId && c.Product.ProductQuantity > 0).ToList();
            var totalPrice = subTotal(orderDetails);
            ViewBag.SubTotal = subTotal(orderDetails);
            var card = paymentRepository.find(int.Parse(payment.Cardnumber));
            if (card==null)
            {
                TempData["msg"] = "<script>alert('This card account is invalid');</script>";
                return View(orderDetails);
            }
            
            else if (card.Bank == null)
            {
                TempData["msg"] = "<script>alert('This bank account is invalid');</script>";
                return View(orderDetails);
                //return View(orderDetails);
            }
            
            else if (card.Expirydate <= DateTime.UtcNow || card.Expirydate != payment.Expirydate)
            {
                TempData["msg"] = "<script>alert('This card is Expiry Date');</script>";
                return View(orderDetails);
            }
            else if (card.Bank.Balance < totalPrice)
            {
                TempData["msg"] = "<script>alert('The amount is not enough to complete this process');</script>";
                return View(orderDetails);
            }
            else 
            {
                card.Bank.Balance = card.Bank.Balance - totalPrice;
                Firstorder order = new Firstorder()
                {
                    OrderPaydate = DateTime.UtcNow,
                    Totalprice = totalPrice,
                    UserId = userId,
                    PaymentId = card.Id
                };
                orderRepository.Add(order);
                Firstorderproduct orderproduct = new Firstorderproduct();
                foreach (var item in orderDetails)
                {
                    orderproduct.Id = 0;
                    orderproduct.OrderId = order.OrderId;
                    orderproduct.ProductId = item.ProductId;
                    orderproduct.Quantity = (decimal)item.CartQuantity;

                    orderProductRepository.Add(orderproduct);
                    cartProductRepository.Delete(item.Id);
                    item.Product.ProductQuantity -= item.CartQuantity;
                    productRepository.Update((decimal)item.ProductId, item.Product);
                }

                bankRepositry.Update(card.Bank.Id, card.Bank);

                SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                System.Net.NetworkCredential credentials =
                    new System.Net.NetworkCredential("ya*******97@outlook.com", "*******");
                client.EnableSsl = true;
                client.Credentials = credentials;

                try
                {

                    var mail = new MailMessage("yazantayem997@outlook.com", "yazantayem26@gmail.com");

                    StringBuilder sb = new StringBuilder();
                    //sb.Append("your order is <br/><br/>");
                    //foreach (var name in orderDetails)
                    //{
                    //    sb.AppendFormat("<br/><span>{0}</span>       {1}          {2}", name.Product.ProductName, name.CartQuantity ,name.CartQuantity*name.Product.ProductPrice);

                    //}

                    //sb.AppendFormat("<br/><br/><p> total Price = :{0}</p>", totalPrice);

                    sb.AppendFormat("<div class='col - lg - 5'>");
                    sb.AppendFormat("<div class='order_box'>");
                    sb.AppendFormat("<h2>Your Order</h2>");
                    sb.AppendFormat("<ul class='list'>");
                    sb.AppendFormat("<li><a> Product <span> Total </span></a></li>");
                    foreach (var item in orderDetails)
                    {
                        sb.AppendFormat("<li><a>");
                        sb.AppendFormat("<span class='first col - 6'>{0}</span>", item.Product.ProductName);
                        sb.AppendFormat("<span class='middle col - 3'>       x{0}</span>", item.CartQuantity);
                        sb.AppendFormat(" <span class='last col - 2'>        ${0}</span>", item.CartQuantity * item.Product.ProductPrice);
                        sb.AppendFormat("</a></li>");
                    }
                    sb.AppendFormat("</ul><ul class='list list_2'>");
                    sb.AppendFormat("<li> <a>Subtotal <span>${0}</span> </a></li>", ViewBag.SubTotal);
                    sb.AppendFormat("<li><a>Shipping<span> Flat rate: $0.00 </span></a></li>");
                    sb.AppendFormat(" <li><a>Total<span>${0} </span></a></li></ul>", ViewBag.SubTotal);
                    sb.AppendFormat("</div> </div>");
                    



                    mail.Subject = "yuor order";
                    mail.Body = sb.ToString(); 
                    mail.IsBodyHtml = true;
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }

                return RedirectToAction("MyCart");
            }
            
            
        }

        public IActionResult MyOrder()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["msgCheckout"] = "<script>alert('Please login first');</script>";
                return RedirectToAction("Index", "Home");
            }
            var order = orderProductRepository.List().Where(o=>o.Order.UserId==userId);
            return View(order);
        }
        
        private decimal subTotal(List<Firstcartproduct> cartProduct)
        {
            decimal totalPrice = 0;
            foreach (var item in cartProduct)
            {
                totalPrice = (decimal)(totalPrice + (item.CartQuantity * item.Product.ProductPrice));
            }
            return totalPrice;
        }

        
    }
}
