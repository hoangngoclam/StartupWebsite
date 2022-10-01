using startup_website_asp.net.Models.DAO;
using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace startup_website_asp.net.Areas.Admin.Controllers
{
	public class AdminProductCategoryController : BaseController
	{
		StartupWebsite db = new StartupWebsite();
		ProductCategoryDAO dao = new ProductCategoryDAO();
		public ActionResult ListProductCategory()
		{
			IEnumerable<ProductCategory> categories = dao.ListAll();
			return View("ListProductCategory", categories);
		}
		
		public ActionResult CreateProductCategory()
		{
			ViewBag.ParentCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "Name");
			return View();
		}
		[HttpPost]
		public ActionResult CreateProductCategory([Bind(Include = "SeoTitle,MetaTitle,DisplayOrder,ParentCategoryId,Name,Status,CreatedAt,UpdatedAt")] ProductCategory productCategory)
		{
			if (ModelState.IsValid)
			{
				long id = dao.Insert(productCategory);
				return RedirectToAction("ListProductCategory");
			}
			ViewBag.ParentCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "Name", productCategory.ParentCategoryId);
			return View(productCategory);
		}
		public ActionResult EditProductCategory(int id)
		{
			ViewBag.ParentCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "Name");
			var category = new ProductCategoryDAO().ViewDetail(id);
			ViewBag.ParentCategoryName = new SelectList(db.ProductCategories, "ParentCategoryId", "Name");
			return View(category);
		}
		[HttpPost]
		public ActionResult EditProductCategory(ProductCategory productCategory)
		{
			if (ModelState.IsValid)
			{				
				bool result = dao.Update(productCategory);
				if (result)
				{
					SetAlert("Sửa sản phẩm thành công", "success");
					return RedirectToAction("ListProductCategory", "Category");
				}
				else
				{
					ModelState.AddModelError("", "Update is Fail!");
				}
			}
			return View("ListProductCategory");
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
				
				bool result=dao.Delete(id);
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