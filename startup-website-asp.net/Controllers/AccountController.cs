using startup_website_asp.net.Models;
using startup_website_asp.net.Common;
using startup_website_asp.net.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using startup_website_asp.net.Models.EF;
using startup_website_asp.net.ViewModels;

namespace startup_website_asp.net.Controllers
{
    public class AccountController : Controller
    {
        private StartupWebsite db = new StartupWebsite();
        private OrderDAO orderDAO = new OrderDAO();
        //[HttpGet]
        //public ActionResult ControllerTest()
        //{
        //    List<StartupType> startupTypes = db.StartupTypes.ToList();
        //    Return View(startupTypes);
        //}
        // GET: Account
        [HttpGet]
        public ActionResult EditProfileAccount()
        {
            var session = (startup_website_asp.net.Common.CustomerLogin)Session[startup_website_asp.net.Common.CommonSession.CUSTOMER_SESSION];
            if (session != null)
            {
                Customer customer = db.Customers.SingleOrDefault(x => x.CustomerId == session.UserID);
                ViewBag.Customer = customer;

                ViewBag.BirthDay = customer.BirthDay.ToString().Split('/');

                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "CustomerLoginAndRegister");
            }
        }

        [HttpPost]
        public ActionResult EditProfileAccount(string name, string email, string address, string phonenumber, string gender, string day, string month, string year)
        {
            var session = (startup_website_asp.net.Common.CustomerLogin)Session[startup_website_asp.net.Common.CommonSession.CUSTOMER_SESSION];
                Customer customer = db.Customers.SingleOrDefault(x => x.CustomerId == session.UserID);
                customer.Name = name;
                customer.Email = email;
                customer.Address = address;
                customer.PhoneNumber = phonenumber;
                customer.Gender = gender;
                customer.BirthDay = DateTime.Parse(month+ "/"+day+"/"+year);
                db.SaveChanges();
                ViewBag.Customer = customer;
                ViewBag.BirthDay = customer.BirthDay.ToString().Split('/');

                var customerSession = new Common.CustomerLogin();
                customerSession.UserName = customer.UserName;
                customerSession.UserID = customer.CustomerId;
                customerSession.FullName = customer.Name;
                Session.Add(Common.CommonSession.CUSTOMER_SESSION, customerSession);

            return View(customer);
        }

        public ActionResult ProfileAccountOrders()
        {
            CustomerLogin session = (CustomerLogin)Session[CommonSession.CUSTOMER_SESSION];
            List<OrderViewModel> orderViewModels = orderDAO.GetListByCustomerId(session.UserID);
            return View(orderViewModels);
        }

        public ActionResult ProfileAccountFavoriteProducts()
        {
            var session = (startup_website_asp.net.Common.CustomerLogin)Session[startup_website_asp.net.Common.CommonSession.CUSTOMER_SESSION];
            if (session != null)
            {
                List<ProductLiked> listProductLikeds = new List<ProductLiked>();

                listProductLikeds = ListProductLikeds(session.UserID);

                List<Product> result = new List<Product>();
                var products = db.Products;

                foreach (var product in products)
                {
                    foreach (var productLiked in listProductLikeds)
                    {
                        if (product.ProductId == productLiked.ProductId)
                        {
                            result.Add(product);
                            break;
                        }
                    }
                }

                ViewBag.ListProducts = result;
                ViewBag.listProductLikeds = listProductLikeds;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "CustomerLoginAndRegister");
            }

        }
        //Logic productliked

        //Ham tra ve danh sach san pham da duoc yeu thich da duoc them vao cua khach hang cos CustomerId truyen vao
        public List<ProductLiked> ListProductLikeds(Guid CustomerId)
        {
            List<ProductLiked> listProductLikeds = new List<ProductLiked>();
            listProductLikeds = db.ProductLikeds.Where(x => x.CustomerId == CustomerId).ToList();
            return listProductLikeds;
        }

        //Tim san pham co id productId da co trong san pham yeu thich hay chua /Neu co tra ve true/Khong tra ve false
        public bool FindProductInListProductLikeds(long productID, List<ProductLiked> listProductLikeds)
        {
            foreach (var productLiked in listProductLikeds)
            {
                if (productLiked.ProductId==productID)
                {
                    return true;
                }
            }
            return false;
        }
        
        [HttpGet]
        public JsonResult ChangeProductToListProductLiked(long productID)
        {
            //Khai bao de lay idCustomer tu session
            var session = (startup_website_asp.net.Common.CustomerLogin)Session[startup_website_asp.net.Common.CommonSession.CUSTOMER_SESSION];
            //Khai bao va tim danh sach da thich voi idCustomer
            if (session !=null)
            {
                List<ProductLiked> listProductLikeds = new List<ProductLiked>();

                listProductLikeds = ListProductLikeds(session.UserID);

                long? viewCountProduct = 0;

                if (FindProductInListProductLikeds(productID, listProductLikeds)==false)//san phan khong nam trong danh sach da thich truoc do
                {
                    ProductLiked productLiked = new ProductLiked();
                    productLiked.CustomerId = session.UserID;
                    productLiked.ProductId = productID;
                    //Them san pham yeu thich vao du lieu
                    db.ProductLikeds.Add(productLiked);
                    //Them so luong thich cua san pham do tang len 1 don vi
                    db.Products.Find(productID).ViewCount += 1;
                    db.SaveChanges();
                    viewCountProduct = db.Products.Find(productID).ViewCount;
                    return Json(new { viewCount = viewCountProduct.ToString(), status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ProductLiked productLiked = new ProductLiked();
                
                    productLiked = db.ProductLikeds.SingleOrDefault(x=>x.ProductId==productID && x.CustomerId==session.UserID);
                    //Xoa san pham yeu thich khoi du lieu
                    db.ProductLikeds.Remove(productLiked);
                    //Giam so luong thich cua san pham do xuong 1 don vi
                    db.Products.Find(productID).ViewCount -= 1;
                    db.SaveChanges();
                    viewCountProduct = db.Products.Find(productID).ViewCount;
                    return Json(new { viewCount = viewCountProduct.ToString(), status = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { viewCount = "sessionNull" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ProfileAccountOrderDetail()
        {

            return View();
        }
    }
}