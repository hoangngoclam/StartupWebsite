using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Newtonsoft.Json;
using startup_website_asp.net.Models;
using startup_website_asp.net.Models.DAO;
using startup_website_asp.net.Models.DAO.Startup;
using startup_website_asp.net.Models.EF;
using startup_website_asp.net.ViewModels;

namespace startup_website_asp.net.Areas.Startup.Controllers
{
    public class StartupProductController : BaseController
    {
		private StartupWebsite db = new StartupWebsite();
		private ProductDAO dao = new ProductDAO();
		private AttributeDAO attributeDAO = new AttributeDAO();
		private ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
		[HttpGet]
		//[ActionName("Index")]
		public ActionResult ListProduct()
        {
			var products = db.Products.Include(p => p.Attribute).Include(p => p.Attribute1)
				.Include(p => p.ProductCategory).Include(p => p.Startup)
				.Include(p => p.StartupCategory).Where(x=>x.StartupId == startupLogin.StartupId).ToList();
			return View(products);
		}

		[HttpGet]
		public JsonResult ApiGetSubAttribute(long ProductId)
		{
			var SubAttributeData = db.Database.SqlQuery<SubAttributeViewModel>("EXECUTE dbo.GetSubAttributeByProdId @ProductId", new SqlParameter("ProductId", ProductId)).ToList();
			var json = JsonConvert.SerializeObject(SubAttributeData);
			return Json(json, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		//[ActionName("Edit")]
		public ActionResult EditProduct(long? id)
		{
			Product product = db.Products.Find(id);
			if (product != null)
			{
				ProductViewModel productModel = new ProductViewModel(product);
				List<SubAttribute> listSubAttribute = new List<SubAttribute>();
				ViewBag.AttributeId1 = new SelectList(db.Attributes, "AttributeId", "Name");
				ViewBag.Attribute = db.Attributes.Find(product.AttributeId1);
				if (product.SubImages != "")
				{
					ViewBag.SubImagesList = product.SubImages.Split(',');
				}
				ViewBag.StartupCategoryId = new SelectList(db.StartupCategories, "StartupCategoryId", "Name", product.StartupCategoryId);
			
				//ViewBag.GiftId = new SelectList(listProductGift, "GiftId", "Name");
				ViewBag.ProductGift = db.ProductGifts.Where(x => x.ProductId == id);
				List<ProductCategoryViewModel> listProductCategoryModel = productCategoryDAO.GetListProductCategoryRender();
				List<Gift> listProductGift = db.Gifts.ToList();
				ViewBag.GiftId = new SelectList(listProductGift, "GiftId", "Name");
				ViewBag.ProductCategoryId = new SelectList(listProductCategoryModel.OrderBy(x => x.ProductCategoryId), "ProductCategoryId", "RoundName", product == null ? 1 : product.ProductCategoryId);
				return View(productModel);
			}
			return RedirectToAction("ListProduct");
		}

		[HttpGet]
		//[ActionName("Create")]
		public ActionResult CreateProduct()
		{
			//ViewBag.AttributeId1 = new SelectList(db.Attributes, "AttributeId", "Name");
			//ViewBag.AttributeId2 = new SelectList(db.Attributes, "AttributeId", "Name");

			//ViewBag.ProductCategoryId = db.ProductCategories.ToList(); 

			ViewBag.StartupId = startupLogin.StartupId;

			ViewBag.StartupCategoryId = new SelectList(db.StartupCategories.Where(x=>x.StartupId == startupLogin.StartupId), "StartupCategoryId", "Name");

			List<ProductCategory> listProductCategory = db.ProductCategories.Where(x => x.ParentCategoryId == null).ToList();

			List<ProductCategoryViewModel> listProductCategoryModel = productCategoryDAO.GetListProductCategoryRender();
			ViewBag.ProductCategoryId = new SelectList(listProductCategoryModel.OrderBy(x => x.ProductCategoryId), "ProductCategoryId", "RoundName");

			List<Gift> listProductGift = db.Gifts.ToList();
			ViewBag.GiftId = new SelectList(listProductGift, "GiftId", "Name");

			return View();
		}
		string ServerSavePath(string path, HttpPostedFileBase file)
		{
			var InputFileName = Path.GetFileName(DateTime.Now.ToFileTime() + file.FileName);
			string imagePath = "/Assets/Images/Startup/Products/" + InputFileName;
			var ServerSavePath = Path.Combine(Server.MapPath(path) + InputFileName);
			file.SaveAs(ServerSavePath);
			return imagePath;
		}
		[HttpPost]
		//[ActionName("Create")]
		public ActionResult CreateProduct(ProductViewModel product, HttpPostedFileBase mainNewImage, HttpPostedFileBase[] subNewImages, int[] GiftIdPush, string ListAttribute, long[] SubItemPrice)
		{
			string mainImagePath;
			List<string> subImagePath = new List<string>();
			if (mainNewImage != null)
			{
				mainImagePath = ServerSavePath("/Assets/Images/Startup/Products/", mainNewImage);
				product.MainImage = mainImagePath;
				foreach (HttpPostedFileBase subImage in subNewImages)
				{
					//Checking file is available to save.  
					if (subImage != null)
					{
						subImagePath.Add(ServerSavePath("/Assets/Images/Startup/Products/", subImage));
					}
					else
					{
						subImagePath.Add("''");
					}
				}
				product.SubImages=string.Join(",", subImagePath);
			}
			long newProductId = dao.CreateProduct(product);
			//if(SubItemPrice != null)
			//{
			//	attributeDAO.CreateManualAttribute(newProductId, product.AttributeId1, ListAttribute, SubItemPrice);
			//}
			//if (GiftIdPush != null)
			//{
			//	ProductGift newProductGift = new ProductGift();
			//	foreach (var giftId in GiftIdPush)
			//	{
			//		newProductGift.ProductId = newProductId;
			//		newProductGift.GiftId = giftId;
			//		db.ProductGifts.Add(newProductGift);
			//		db.SaveChanges();
			//	}
			//}

			if (newProductId == 0)
			{
				return RedirectToAction("CreateProduct");
			};

			return RedirectToAction("ListProduct");
		}
		[HttpGet]
		//[ActionName("GetProductGift")]
		public JsonResult GetProductGift(String idGift)
		{
			long id = long.Parse(idGift);


			List<Gift> listGift = db.Gifts.Where(x => x.GiftId == id).ToList();

			string listStringHtml = "";

			foreach (var gift in listGift)
			{

				listStringHtml += "<div class=" + "'col-6 col-md-2 p-2 pr-0' id ='gift-item-" + gift.GiftId + "'>" +
									"<div class=" + "'card w-100 mb-0' style =" + "'position: relative'" + ">" +
										"<a onclick='deleteGiftItem(event)' type='button' data-id='" + gift.GiftId + "' class=" + "'btn btn-danger btn-gift-delete' style =" + "'width: 25px; height:25px;line-height: 25px; padding:0;margin:0; text-align:center; border-radius:50%; position:absolute; top:5px;right:5px; cursor: pointer;'" + ">" + "X" + "</a>" +
										"<img class=" + "'card-img-top' src =" + gift.ImageUrl + ">" +
										"<div class=" + "'card-body'" + ">" +
											 "<p class=" + "'card-text'" + ">" + gift.Name + "</p>"
										 + "</div>"
									+ "</div>"
								 + "</div>";
			}

			return Json(listStringHtml, JsonRequestBehavior.AllowGet);
		}


		[HttpGet]
		//[ActionName("GetListChildCategory")]
		public JsonResult GetListChildCategory(String idParentCategory)
		{
			int id = int.Parse(idParentCategory);
			List<ProductCategory> listChildCategory = db.ProductCategories.Where(x => x.ParentCategoryId == id).ToList();
			string listOption = "";
			foreach (var childCategory in listChildCategory)
			{
				listOption += "<option value=" + childCategory.ProductCategoryId + ">" + childCategory.Name + "</option>";
			}
			return Json(listOption, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		//[ActionName("GetListGrandChildCategory")]
		public JsonResult GetListGrandChildCategory(String idChildCategory)
		{
			if (idChildCategory != "")
			{
				int id = int.Parse(idChildCategory);
				List<ProductCategory> listGrandChildCategory = db.ProductCategories.Where(x => x.ParentCategoryId == id).ToList();
				string listOption = "";
				foreach (var grandChildCategory in listGrandChildCategory)
				{
					listOption += "<option value=" + grandChildCategory.ProductCategoryId + ">" + grandChildCategory.Name + "</option>";
				}
				return Json(listOption, JsonRequestBehavior.AllowGet);
			}
			return Json(null, JsonRequestBehavior.AllowGet);

		}

		[HttpGet]
		//[ActionName("GetListSubAttribute")]
		public JsonResult GetListSubAttribute(int id)
		{
			var listSubAttribute = db.SubAttributes.Where(x => x.AttributeId == id).ToList();
			List<string> arraySubAttribute = new List<string>();
			foreach (var subAttributeItem in listSubAttribute)
			{
				arraySubAttribute.Add(subAttributeItem.SubAttributeId + "-" + subAttributeItem.Name + "-" + subAttributeItem.BackgroundColor+"-"+subAttributeItem.TextColor);
			}
			return Json(string.Join(",", arraySubAttribute.ToArray()), JsonRequestBehavior.AllowGet);
		}


		[HttpPost]
		//[ActionName("UpdateStatus")]
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
		//[ActionName("Delete")]
		public JsonResult DeleteProduct(int id)
		{
			try
			{
				bool result = dao.Delete(id);
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

      
		

		[HttpPost]
		//[ActionName("Edit")]
		public ActionResult EditProduct(ProductViewModel product, HttpPostedFileBase mainNewImage, HttpPostedFileBase[] subNewImages, int[] GiftIdPush, string ListAttribute, long[] SubItemPrice)
		{
			try
			{
				//if (SubItemPrice != null && product.AttributeId1 != null)
				//{
				//	attributeDAO.removeAttrRelProductId(product.ProductId);
				//	attributeDAO.CreateManualAttribute(product.ProductId, product.AttributeId1, ListAttribute, SubItemPrice);
				//}
				dao.Update(product, mainNewImage, subNewImages);
				return RedirectToAction("ListProduct");
			}
			catch (Exception)
			{
				return RedirectToAction("EditProduct", product.ProductId);
				throw;
			}
			
		}

	}
}