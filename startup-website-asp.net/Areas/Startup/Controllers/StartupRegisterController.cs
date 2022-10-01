using startup_website_asp.net.Areas.Startup.Models.DAO;
using startup_website_asp.net.Common;
using startup_website_asp.net.Models.DAO;
using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace startup_website_asp.net.Areas.Startup.Controllers
{
    public class StartupRegisterController : Controller
    {
        StartupWebsite db = new StartupWebsite();
        StartupDAO dao = new StartupDAO();
        StartupAccountDAO sAccountDao = new StartupAccountDAO();
        // GET: Startup/StartupRegister
        [HttpGet]
        public ActionResult Index()
        {
            var startupAccountSesstion = (StartupLogin)Session[CommonSession.STARTUP_SESSION];
            if (startupAccountSesstion == null)
            {
                return RedirectToAction("Login", "LoginAndRegister");
            }
            else if(startupAccountSesstion.StartupId == null)
            {
                ViewBag.StartupTypeId = new SelectList(db.StartupTypes, "StartupTypeId", "Name");
                return View();
            }
            return RedirectToAction("Login", "LoginAndRegister");
        }
        [HttpPost]
        public ActionResult Index( startup_website_asp.net.Models.EF.Startup startupR, HttpPostedFileBase logo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string logoUrl = ServerSavePath("/Assets/Images/Startup/Products/", logo);
                    startupR.LogoUrl = logoUrl;
                    long newStartupId = dao.RegisterStartup(startupR);
                    if (newStartupId != 0)
                    {
                        StartupLogin sAccountSesstion = (StartupLogin)Session[CommonSession.STARTUP_SESSION];
                        sAccountDao.UpdateStartupId(newStartupId,sAccountSesstion.UserID);
                        sAccountSesstion.StartupId = newStartupId;
                        return RedirectToAction("Index", "StartupHome");
                    }
                }
                ViewBag.StartupTypeId = new SelectList(db.StartupTypes, "StartupTypeId", "Name");
                ViewBag.Error("Không thể tạo startup vì 1 lý do nào đó");
                return View(startupR);
            }
            catch (Exception)
            {
                ViewBag.StartupTypeId = new SelectList(db.StartupTypes, "StartupTypeId", "Name");
                ViewBag.Error("Không thể tạo startup vì 1 lý do nào đó");
                return View(startupR);
            }
        }

        protected string ServerSavePath(string path, HttpPostedFileBase file)
        {
            var InputFileName = Path.GetFileName(DateTime.Now.ToFileTime() + file.FileName);
            string imagePath = "/Assets/Images/Startup/Products/" + InputFileName;
            var ServerSavePath = Path.Combine(Server.MapPath(path) + InputFileName);
            file.SaveAs(ServerSavePath);
            return imagePath;
        }
    }
}