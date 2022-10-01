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
    public class AdminStartupTypesController : BaseController
    {
        StartupTypeDAO dao = new StartupTypeDAO();
        // GET: Admin/AdminStartupTypes
        public ActionResult Index()
        {
            return View(db.StartupTypes.ToList());
        }

        // GET: Admin/AdminStartupTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StartupType startupType = db.StartupTypes.Find(id);
            if (startupType == null)
            {
                return HttpNotFound();
            }
            return View(startupType);
        }

        // GET: Admin/AdminStartupTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminStartupTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StartupTypeId,Name,MetaTitle,ImageUrl,CreatedAt,UpdatedAt")] StartupType startupType)
        {
            if (ModelState.IsValid)
            {
                db.StartupTypes.Add(startupType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(startupType);
        }

        // GET: Admin/AdminStartupTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StartupType startupType = db.StartupTypes.Find(id);
            if (startupType == null)
            {
                return HttpNotFound();
            }
            return View(startupType);
        }

        // POST: Admin/AdminStartupTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StartupTypeId,Name,MetaTitle,ImageUrl,CreatedAt,UpdatedAt")] StartupType startupType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(startupType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(startupType);
        }

        [HttpPost]
        public JsonResult Delete(int id)
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
