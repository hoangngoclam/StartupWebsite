using startup_website_asp.net.Common;
using startup_website_asp.net.Models;
using startup_website_asp.net.Models.DAO;
using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace startup_website_asp.net.Controllers
{
	
	public class BaseController : Controller
    {
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var session = (CustomerLogin)Session[CommonSession.CUSTOMER_SESSION];
			if (session == null)
			{
				filterContext.Result = new RedirectToRouteResult(new
					System.Web.Routing.RouteValueDictionary(new { Controller = "CustomerLoginAndRegister", action = "Login"}));
			}
			base.OnActionExecuting(filterContext);
		}
		
		
	}
}