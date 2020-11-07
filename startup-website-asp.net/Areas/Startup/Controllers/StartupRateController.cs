using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using startup_website_asp.net.Models.EF;

namespace startup_website_asp.net.Areas.Startup.Controllers
{
    public class StartupRateController : BaseController
    {
        private StartupWebsite db = new StartupWebsite();

        // GET: Startup/StartupRate
        public ActionResult Index()
        {
            var rates = db.Rates.Include(r => r.OrderDetail).Include(r => r.Product).Where(x=>x.StartupId == startupLogin.StartupId);
            return View(rates.ToList());
        }

        // GET: Startup/StartupRate/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            return View(rate);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
