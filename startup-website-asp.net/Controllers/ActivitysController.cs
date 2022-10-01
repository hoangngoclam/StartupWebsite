using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace startup_website_asp.net.Controllers
{
    public class ActivitysController : Controller
    {
        // GET: Activity
        public ActionResult ActivityListScreen()
        {
            return View();
        }
        public ActionResult ActivityDetailScreen()
        {
            return View();
        }
    }
}