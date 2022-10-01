using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using startup_website_asp.net.Classes;
using startup_website_asp.net.Models.EF;

namespace startup_website_asp.net.Areas.Admin.Controllers
{
    public class AdminStartupController : BaseController
	{
        private StartupWebsite db = new StartupWebsite();

        // GET: Admin/Startups
        public ActionResult ListAdminStartup()
        {
            var startups = db.Startups;
            ViewBag.SearchValue = "";
            ViewBag.Status = "";
            return View(startups.ToList());
        }

        // GET: Admin/Startups/Details/5
        public ActionResult DetailAdminStartup(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            startup_website_asp.net.Models.EF.Startup startup = db.Startups.Find(id);
            if (startup == null)
            {
                return HttpNotFound();
            }
            return View(startup);
        }

        // GET: Admin/Startups/Create
        public ActionResult CreateAdminStartup()
        {
            ViewBag.StartupTypeId = new SelectList(db.StartupTypes, "StartupTypeId", "Name");
            ViewBag.Status = new SelectList(this.GetListStartupStatus(),"Name");
            return View();
        }

        // POST: Admin/Startups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdminStartup([Bind(Include = "StartupId,WardId,StartupTypeId,Name,LogoUrl,CoverUrl,Description,Slogan,Address,PhoneNumber,FanpageLink,YoutubeLink,InstagramLink,Status,CreatedAt,UpdatedAt")] startup_website_asp.net.Models.EF.Startup startup)
        {
            if (ModelState.IsValid)
            {
                db.Startups.Add(startup);
                db.SaveChanges();
                return RedirectToAction("ListAdminStartup");
            }

            return View("ListAdminStartup");
        }

        // GET: Admin/Startups/Edit/5
        public ActionResult StartupEdit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            startup_website_asp.net.Models.EF.Startup startup = db.Startups.Find(id);
            if (startup == null)
            {
                return HttpNotFound();
            }
            ViewBag.Status = new SelectList(this.GetListStartupStatus(), "Name",startup.Status);
            ViewBag.StartupTypeId = new SelectList(db.StartupTypes, "StartupTypeId", "Name");
            return View(startup);
        }

        [HttpPost]
        public JsonResult AjaxEditAdminStartup(int id, string status)
        {
            try
            {
                startup_website_asp.net.Models.EF.Startup startupUpdate = db.Startups.Find(id);
                startupUpdate.Status = status;
                db.SaveChanges();
                return Json(new { Id = id, Status = status, Type = "Success", Message = "Cập nhật thành công" });
            }
            catch (Exception)
            {
                return Json(new { Id = false, Status = false, Type = "Error", Message = "Cập nhật không thành công" });
            }         
        }

        [HttpPost]
        public JsonResult AjaxDeleteAdminStartup(long id)
        {
            try
            {
                startup_website_asp.net.Models.EF.Startup startup = db.Startups.Find(id);
                db.Startups.Remove(startup);
                db.SaveChanges();
                return Json(new { Result = true, Id = id, Type = "Success", Message = "Xóa thành công" });
            }
            catch (Exception)
            {
                return Json(new { Result = false, Id = -1, Type = "Error", Message = "Xóa không thành công", });
            }
            
        }
        [HttpGet]
        public ActionResult SearchAndFilterAdminStartup(string searchValue, String status)
        {
            var SearchResult = db.Startups.Where(x=>x.Name != "");
            if (searchValue != "")
            {
                SearchResult = db.Startups.Where(x => x.Name.Contains(searchValue));
            }
            if(status != "" && status != null)
            {
                SearchResult = SearchResult.Where(x => x.Status == status.Trim());
            }
            ViewBag.SearchValue = searchValue;
            ViewBag.Status = status;
            return View("ListAdminStartup", SearchResult.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private List<StartupStatus> GetListStartupStatus()
        {
            //{ "Chưa duyệt", "Bị ẩn", "Đang hoạt động", "Ngừng Hoạt động", "Bị Khóa" }
            List<StartupStatus> listStatus = new List<StartupStatus>();
            listStatus.Add(new StartupStatus("Chưa duyệt"));
            listStatus.Add(new StartupStatus("Bị ẩn"));
            listStatus.Add(new StartupStatus("Đang hoạt độn"));
            listStatus.Add(new StartupStatus("Ngừng Hoạt động"));
            listStatus.Add(new StartupStatus("Bị Khóa"));
            return listStatus;
        }

    }
}
