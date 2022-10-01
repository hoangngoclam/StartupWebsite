using startup_website_asp.net.Common;
using startup_website_asp.net.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace startup_website_asp.net.Controllers
{
    public class StartupLikesController : Controller
    {
        StartupLikeDAO startupLikeDAO = new StartupLikeDAO();

        [HttpPost]
        public JsonResult Subscribe(int id)
        {
            CustomerLogin customerLogin = (CustomerLogin)Session[CommonSession.CUSTOMER_SESSION];
         
            if (customerLogin != null)
            {
                bool resultSub = startupLikeDAO.Subscribe(id, customerLogin.UserID);
                if (resultSub)
                    return Json(1);
                else
                    return Json(0);
            }
            else
            {
                return Json(-1);
            }
            
        }
        [HttpPost]
        public JsonResult UnSubscribe(int id)
        {
            CustomerLogin customerLogin = (CustomerLogin)Session[CommonSession.CUSTOMER_SESSION];
            
            if (customerLogin != null)
            {
                bool resultUnSub = startupLikeDAO.UnSubscribe(id, customerLogin.UserID);
                if(resultUnSub)
                    return Json(1);
                else
                    return Json(0);
            }
            else
            {
                return Json(-1);
            }

        }
    }
}