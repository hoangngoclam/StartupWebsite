using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace startup_website_asp.net.Areas.Startup.Controllers
{
    public class StartupHomeController : BaseController
    {
        // GET: Startup/StartupHome
        public ActionResult Index()
        {
            return View();
        }
    }
}