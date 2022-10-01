using startup_website_asp.net.Common;
using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace startup_website_asp.net.Areas.Admin.Controllers
{
	public class BaseController:Controller
	{
		protected StartupWebsite db = new StartupWebsite();
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
			//var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
			//if (session == null)
			//{
			//	filterContext.Result = new RedirectToRouteResult(new
			//		System.Web.Routing.RouteValueDictionary(new { Controller = "LoginAndRegister", action = "Login", Area = "Admin" }));
			//}
			//base.OnActionExecuting(filterContext);
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
			var ServerSavePath = Path.Combine(Server.MapPath(path) + InputFileName);
			file.SaveAs(ServerSavePath);
			return ServerSavePath;
		}
	}
}