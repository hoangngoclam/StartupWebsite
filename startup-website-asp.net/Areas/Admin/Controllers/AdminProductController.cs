using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using startup_website_asp.net.Models.DAO;
using startup_website_asp.net.Models.EF;

namespace startup_website_asp.net.Areas.Admin.Controllers
{
    public class AdminProductController : BaseController
    {
        private StartupWebsite db = new StartupWebsite();
		private ProductDAO dao = new ProductDAO();
        // GET: Admin/Products
        public ActionResult ListProduct()
        {
            var products = db.Products.Include(p => p.Attribute).Include(p => p.Attribute1).Include(p => p.ProductCategory).Include(p => p.Startup).Include(p => p.StartupCategory);
            return View("ListProduct",products.ToList());
        }

		[HttpPost]
		public JsonResult UpdateStatus(int id, string status)
		{
			try
			{
				bool result = dao.UpdateStatus(id, status);
				if (result)
				{
					return Json(new { Id = id, Status = status, Type = "Success", Message = "Cập nhật thành công" });
				}
				else
				{
					return Json(new { Id = false, Status = false, Type = "Error", Message = "Cập nhật không thành công" });
				}
			}
			catch (Exception)
			{
				return Json(new { Id = false, Status = false, Type = "Error", Message = "Cập nhật không thành công" });
			}
		}

	}
}