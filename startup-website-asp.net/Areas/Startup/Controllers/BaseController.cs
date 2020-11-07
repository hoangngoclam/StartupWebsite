using startup_website_asp.net.Common;
using startup_website_asp.net.Models;
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
	public class BaseController:Controller
	{
		protected StartupWebsite db ;
		protected StartupLogin startupLogin;
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var startupAccountSesstion = (StartupLogin)Session[CommonSession.STARTUP_SESSION];
			if (startupAccountSesstion == null)
			{
				filterContext.Result = new RedirectToRouteResult(new
					System.Web.Routing.RouteValueDictionary(new { Controller = "LoginAndRegister", action = "Login", Area = "Startup" }));
				base.OnActionExecuting(filterContext);
			}
			else if (startupAccountSesstion.StartupId == null)
			{
				filterContext.Result = new RedirectToRouteResult(new
					System.Web.Routing.RouteValueDictionary(new { Controller = "StartupRegister", action = "Index", Area = "Startup" }));
			}
			else
			{
				startupLogin = startupAccountSesstion;
			}
			
			base.OnActionExecuting(filterContext);
		}

		public BaseController()
		{
			db = new StartupWebsite();
		}
		//Alert
		protected void SetAlert(string message, string type)
		{
			TempData["AlertMessage"] = message;
			if (type == "success")
			{
				TempData["AlertType"] = "success";
			}
			else if (type == "warning")
			{
				TempData["AlertType"] = "warning";
			}
			else if (type == "error")
			{
				TempData["AlertType"] = "danger";
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