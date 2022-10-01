using System.Web.Mvc;
using MvcSiteMapProvider;
namespace startup_website_asp.net.Areas.Admin.Controllers
{
    public class AdminAccountController : BaseController
    {
        // GET: Admin/Account
        public ActionResult ListAccount()
        {
            return View();
        }

        public ActionResult CreateAccount()
        {
            return View();
        }

        public ActionResult EditAccount()
        {
            return View();
        }
        public ActionResult DetailAccount()
        {
            return View();
        }
        public ActionResult DeleteAccount()
        {
            return View();
        }


    }
}