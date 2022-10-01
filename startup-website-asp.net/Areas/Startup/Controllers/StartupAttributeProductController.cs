using startup_website_asp.net.Models.DAO.Startup;
using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Attribute = startup_website_asp.net.Models.EF.Attribute;

namespace startup_website_asp.net.Areas.Startup.Controllers
{
    public class StartupAttributeProductController : BaseController
    {
        // GET: Startup/AttributeProduct
        private AttributeDAO dao = new AttributeDAO();
        public ActionResult ListAttribute()
        {
            var attributes = db.Attributes.ToList();
            return View(attributes);
        }
        [HttpGet]
        public ActionResult ListSubAttribute(int id)
        {
            var subAttributes = db.SubAttributes.Where(x => x.AttributeId == id).ToList();
            ViewBag.AttributeId = id;
            return View(subAttributes);
        }

        [HttpGet]
        public ActionResult CreateAttribute()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateSubAttribute(int id)
        {
            ViewBag.AttributeId = id;
            return View();
        }

        [HttpGet]
        public ActionResult EditAttribute(int id)
        {
            var attibute = db.Attributes.Find(id);
            return View(attibute);
        }

        [HttpGet]
        public ActionResult EditSubAttribute(int id,int AttributeId)
        {
            var subAttibute = db.SubAttributes.Find(id);
            
            return View(subAttibute);
        }

        [HttpPost]
        public ActionResult CreateAttribute(Attribute attributeRequest)
        {
            try
            {
                db.Attributes.Add(attributeRequest);
                db.SaveChanges();
                return RedirectToAction("ListAttribute");
            }
            catch (Exception)
            {
                return View(attributeRequest);
            }
            
        }

        [HttpPost] 
        public ActionResult CreateSubAttribute(SubAttribute subAttributeRequest)
        {
            try
            {
                db.SubAttributes.Add(subAttributeRequest);
                db.SaveChanges();
                return RedirectToAction("ListSubAttribute", new { id = subAttributeRequest.AttributeId });
            }
            catch (Exception)
            {
                return View(subAttributeRequest);
            }
            
        }

        [HttpPost]
        public ActionResult EditAttribute(Attribute attributeRequest)
        {
            try
            {
                var attribute = db.Attributes.Find(attributeRequest.AttributeId);
                attribute.Name = attributeRequest.Name;
                db.SaveChanges();
                return RedirectToAction("ListAttribute");
            }
            catch (Exception)
            {
                return View(attributeRequest);
            }
            
        }

        [HttpPost]
        public ActionResult EditSubAttribute(SubAttribute subAttributeRequest)
        {
            try
            {
                var attribute = db.SubAttributes.Find(subAttributeRequest.SubAttributeId);
                attribute.Name = subAttributeRequest.Name;
                attribute.TextColor = subAttributeRequest.TextColor;
                attribute.BackgroundColor = subAttributeRequest.BackgroundColor;
                attribute.Index = subAttributeRequest.Index;
                db.SaveChanges();
                return RedirectToAction("ListSubAttribute", new { id = subAttributeRequest.AttributeId });
            }
            catch (Exception)
            {
                return View(subAttributeRequest);
            }
        }

        [HttpPost]
        public JsonResult DeleteAttribute(int id)
        {
            try
            {
                bool result = dao.DeleteAttribute(id);
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
        public JsonResult DeleteSubAttibute(int id)
        {
            try
            {
                bool result = dao.DeleteSubAttibute(id);
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