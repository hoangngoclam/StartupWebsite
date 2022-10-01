using startup_website_asp.net.Common;
using startup_website_asp.net.Models.DAO;
using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace startup_website_asp.net.Areas.Startup.Controllers
{
    public class StartupCategoryController : BaseController
    {
        StartupCategoryDAO startupCategoryDao = new StartupCategoryDAO();
		
		// GET: Startup/StartupCategory
		public ActionResult ListStartupCategory()
        {

			List<StartupCategory> startupCategories = db.StartupCategories.Where(x=>x.StartupId == startupLogin.StartupId).ToList();
            return View(startupCategories);
        }
		public ActionResult DetailStartupCategory(int id)
		{
			StartupCategory startupCategory = startupCategoryDao.ViewDetail(id, startupLogin.UserID);
			return View(startupCategory);
		}
		public ActionResult EditStartupCategory(int id)
		{
			var category = startupCategoryDao.ViewDetail(id, startupLogin.UserID);
			if(category == null)
			{
				return RedirectToAction("Login", "LoginAndRegister");
			}
			return View(category);
		}

		[HttpPost]

		public ActionResult EditStartupCategory(StartupCategory startupCategory)
		{
			if (ModelState.IsValid)
			{
				bool result = startupCategoryDao.Update(startupCategory);
				if (result)
				{
					SetAlert("Sửa sản phẩm thành công", "success");
					return RedirectToAction("Index");
				}
				else
				{
					ModelState.AddModelError("", "Update is Fail!");
				}
			}
			return View("Index");
		}
		public ActionResult CreateStartupCategory()
		{			
			return View();
		}
		[HttpPost]
		public ActionResult CreateStartupCategory(StartupCategory startupCategory)
		{
			if (ModelState.IsValid)
			{
				long id = startupCategoryDao.Insert(startupCategory);
				return RedirectToAction("ListStartupCategory");
			}			
			return View();
		}
		[HttpPost]
		public JsonResult UpdateStatus(int id, string status)
		{
			try
			{
				bool result = startupCategoryDao.UpdateStatus(id, status);
				if (result)
				{
					return Json(new { Id = id, Status = status, Type="Success", Message = "Cập nhật thành công" });
				}
				else
				{
					return Json(new { Id = false, Status = false, Type = "Error", Message = "Cập nhật không thành công" });
				}
			}
			catch (Exception )
			{
				return Json(new { Id = false, Status = false, Type = "Error", Message = "Cập nhật không thành công" });
			}
		}

		[HttpPost]
		public JsonResult Delete(int id)
		{
			try
			{
				
				bool result = startupCategoryDao.Delete(id);
				if (result)
				{
					return Json(new { Result = true, Id = id, Type = "Success", Message = "Xóa thành công" });
				}
				else
				{
					return Json(new { Result = false, Id = -1, Type = "Error", Message = "Xóa không thành công", });
				}
			}
			catch (Exception)
			{
				return Json(new { Result = false, Id = -1, Type = "Error", Message = "Xóa không thành công", });
			}
		}
	}
}