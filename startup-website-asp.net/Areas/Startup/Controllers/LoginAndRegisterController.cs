using startup_website_asp.net.Areas.Startup.Models.DAO;
using startup_website_asp.net.Common;
using startup_website_asp.net.Models;
using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace startup_website_asp.net.Areas.Startup.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        // GET: Admin/LoginAndRegister
        //Login account admin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(StartupLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new StartupAccountDAO();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                switch (result)
                {
                    case 1:
                        ModelState.AddModelError("", "Đăng nhập thành công!");
                        StartupAccount startupAccount = dao.GetByUserName(model.UserName);
                        var startupSession = new Common.StartupLogin();

                        startupSession.UserName = startupAccount.UserName;
                        startupSession.UserID = startupAccount.StartupAccountId;
                        startupSession.FullName = startupAccount.Name;
                        startupSession.StartupId = startupAccount.StatupId;
                        Session.Add(Common.CommonSession.STARTUP_SESSION, startupSession);

                        return RedirectToAction("Index", "StartupHome");
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

        //Logout account startup
        [HttpGet]
        public ActionResult Logout()
        {
            Session[Common.CommonSession.STARTUP_SESSION] = null;
            return RedirectToAction("Login");
        }


        //Register account admin
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(StartupRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new StartupAccountDAO();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại!");
                }
                else
                {
                    var user = new StartupAccount();
                    
                    user.UserName = model.UserName;
                    user.Password = Encryptor.MD5Hash(model.Password);

                    //user.BirthDay = DateTime.Parse(model.Birthday);

                    user.Name = model.Name;

                    user.PhoneNumber = model.PhoneNumber;

                    user.Address = model.Address;

                    user.Gender = "Nam";
                    user.type = "Người viết bài";
                    user.Status = "Đang hoạt động";

                    var result = dao.Insert(user);

                    if (result > 0)
                    {
                        //ViewBag.Success = "Đăng ký thành công!";
                        //model = new CustomerRegisterViewModel();
                        return RedirectToAction("login");
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