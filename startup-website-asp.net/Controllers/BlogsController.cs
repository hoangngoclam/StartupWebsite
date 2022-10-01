using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace startup_website_asp.net.Controllers
{
    public class BlogsController : Controller
    {
        // GET: Blogs
        public ActionResult BlogList()
        {
            return View();
        }
        public ActionResult BlogDetail()
        {
            return View();
        }
    }
}