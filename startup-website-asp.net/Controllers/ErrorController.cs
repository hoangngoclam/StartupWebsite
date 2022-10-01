using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace startup_website_asp.net.Controllers
{
    public class ErrorController : Controller
    {
		public ActionResult Oops(int id)
		{
			Response.StatusCode = id;

			return View();
		}
	}
}