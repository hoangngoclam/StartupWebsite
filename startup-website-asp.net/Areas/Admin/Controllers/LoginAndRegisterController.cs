using startup_website_asp.net.Common;
using startup_website_asp.net.Models;
using startup_website_asp.net.Models.DAO;
using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace startup_website_asp.net.Areas.Admin.Controllers
{
    public class LoginAndRegisterController : Controller
    {

        private SystemAccountDAO dao = new SystemAccountDAO();
        // GET: Admin/LoginAndRegister
        //Login account admin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var resultLogin = dao.Login(model.UserName, model.Password); //int:Item1, string:Item2, AdminLogin:Item3

				if (resultLogin.Item1 == 1)
                {
                    Session.Add(Common.CommonSession.ADMIN_SESSION, resultLogin.Item3);
                    return RedirectToAction("ListAccount", "AdminAccount");
                }
                else
                {
                    ModelState.AddModelError("", resultLogin.Item2);
                    return View("Login");
                }
            }
            return View("Login");
        }

        //Logout account admin
        [HttpGet]
        public ActionResult Logout()
        {
            Session[Common.CommonSession.ADMIN_SESSION] = null;
            return RedirectToAction("Login");
        }


        ////Register account admin
        //public ActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Register(AdminRegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var dao = new SystemAccountDAO();
        //        if (dao.CheckUserName(model.UserName))
        //        {
        //            ModelState.AddModelError("", "Tên đăng nhập đã tồn tại!");
        //        }
        //        else
        //        {
        //            var user = new SystemAccount();

        //            user.UserName = model.UserName;
        //            user.Password = Encryptor.MD5Hash(model.Password);

        //            user.BirthDay = DateTime.Parse(model.Birthday);

        //            user.Name = model.Name;
                   
        //            user.PhoneNumber = model.PhoneNumber;

        //            user.Address = model.Address;

        //            //user.Gender = model.Gender;
               
        //            user.CreatedAt = DateTime.Now;

        //            var result = dao.Insert(user);

        //            if (result > 0)
        //            {
        //                //ViewBag.Success = "Đăng ký thành công!";
        //                //model = new CustomerRegisterViewModel();

        //                return View("/");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Đăng ký thât bại!");
        //            }
        //        }
        //    }
        //    return View(model);
        //}
    }
}