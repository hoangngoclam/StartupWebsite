using startup_website_asp.net.Models.DAO.Startup;
using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace startup_website_asp.net.Areas.Startup.Controllers
{
    public class StartupGiftController : BaseController
    {
		// GET: Startup/StartupGift
		private GiftDAO dao = new GiftDAO();
        public ActionResult ListGift()
        {
			var gifts = db.Gifts.ToList();
            return View(gifts);
        }
		public ActionResult CreateGift()
		{
			return View();
		}

		[HttpPost]
		public ActionResult CreateGift(Gift giftRequest, HttpPostedFileBase mainImage)
		{
			try
			{
				if (mainImage != null)
				{
					string mainImagePath = ServerSavePath("/Assets/Images/Startup/Products/", mainImage);
					giftRequest.ImageUrl = mainImagePath;
					db.Gifts.Add(giftRequest);
					db.SaveChanges();
					return RedirectToAction("ListGift");
				}
				return View("CreateGift");
			}
			catch (Exception)
			{
				return View("CreateGift");
			}
			
		}
		[HttpGet]
		public ActionResult EditGift(int id)
		{
			var gift = db.Gifts.Find(id);
			return View(gift);
		}
		[HttpPost]
		public ActionResult EditGift(Gift giftRequest, HttpPostedFileBase mainImage)
		{
			try
			{
				
				if (ModelState.IsValid)
				{
					Gift gift = db.Gifts.Find(giftRequest.GiftId);
					if (mainImage != null)
					{
						string mainImagePath = ServerSavePath("/Assets/Images/Startup/Products/", mainImage);
						gift.ImageUrl = mainImagePath;
					}
					gift.Name = giftRequest.Name;
					gift.price = giftRequest.price;
					gift.Quality = giftRequest.Quality;
					db.SaveChanges();
					return RedirectToAction("ListGift");
				}
				return View(giftRequest);
			}
			catch (Exception)
			{
				return View(giftRequest);
			}
		
		}

		[HttpPost]
		public JsonResult DeleteGift(int id)
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


	}
}