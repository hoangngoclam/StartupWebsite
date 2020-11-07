using startup_website_asp.net.Common;
using startup_website_asp.net.Models;
using startup_website_asp.net.Models.DAO;
using startup_website_asp.net.Models.EF;
using startup_website_asp.net.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace startup_website_asp.net.Controllers
{
    public class CustomerLoginAndRegisterController : Controller
    {
        // GET: LoginAndRegister
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(CustomerLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new CustomerDAO();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                switch (result)
                {
                    case 1:
                        ModelState.AddModelError("", "Đăng nhập thành công!");
                        var customer = dao.GetByUserName(model.UserName);
                        var customerSession = new Common.CustomerLogin();
                        customerSession.UserName = customer.UserName;
                        customerSession.UserID = customer.CustomerId;
                        customerSession.FullName = customer.Name;
                        Session.Add(Common.CommonSession.CUSTOMER_SESSION, customerSession);
                        return RedirectToAction("Index", "Home");
                    case 0:
                        ModelState.AddModelError("", "Tài khoản không tồn tại!");
                        break;
                    case -1:
                        ModelState.AddModelError("", "Sai mật khẩu! ");
                        break;
                    default:
                        ModelState.AddModelError("", "Đăng nhập không thành công!");
                        break;
                }
            }
            return View("Login");
        }
        //Logout account customer
        [HttpGet]
        public ActionResult Logout()
        {
            Session[Common.CommonSession.CUSTOMER_SESSION] = null;
            Session[Common.CommonSession.CART_SESSION] = null;
            return Redirect("/");
        }
        //Register account customer
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(CustomerRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new CustomerDAO();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại!");
                }
                else
                {
                    var user = new Customer();
                    user.CustomerId = Guid.NewGuid();
                    user.UserName = model.UserName;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    //user.BirthDay = DateTime.Parse(model.Birthday);
                    user.Name = model.Name;
                    user.PhoneNumber = model.PhoneNumber;
                    //user.Address = model.Address;
                    user.Gender = model.Gender;
                    user.CreatedAt = DateTime.Now;
                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        //ViewBag.Success = "Đăng ký thành công!";
                        //model = new CustomerRegisterViewModel();

                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký thât bại!");
                    }
                }
            }
            return View(model);
        }
    }
}