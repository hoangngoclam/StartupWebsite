using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using startup_website_asp.net.Models;
using startup_website_asp.net.Models.DAO;
using startup_website_asp.net.Models.EF;
using startup_website_asp.net.ViewModels;

namespace startup_website_asp.net.Areas.Startup.Controllers
{
    public class StartupOrderController : BaseController
    {
        private OrderDAO orderDAO = new OrderDAO();
        private OrderDetailDAO orderDetailDAO = new OrderDetailDAO();
        public enum StatusOrder
        {
            ChuaDuyet,
            DaDuyet,
            DaGiao,
            DaHuy
        };
        public ActionResult Index(string searchString = null)
        {
            ViewBag.SearchString = searchString;
            List<Order> orders = new List<Order>();
            if (searchString != null)
            {
                searchString = searchString.Trim();
                orders = db.Orders.Include(o => o.Customer)
                    .OrderByDescending(x => x.CreatedAt)
                    .Where(x => x.Status != "Đã xóa" 
                    && (x.Customer.Name.Contains(searchString) || x.Name.Contains(searchString) 
                    || x.PhoneNumber.Contains(searchString) ||x.Address.Contains(searchString))).ToList();
            }
            else
            {
                orders = db.Orders.Include(o => o.Customer).OrderByDescending(x => x.CreatedAt).Where(x => x.Status != "Đã xóa").ToList();
            }
            return View(orders);
        }

        public ActionResult OrderCreate()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderCreate([Bind(Include = "CustomerId,PhoneNumber,Address,Name,Email,IsTrial,TotalPrice,Note,ShipPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", order.CustomerId);
            return View(order);
        }

        public ActionResult OrderEdit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Include(x => x.Customer).SingleOrDefault(x=>x.OrderId == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", order.CustomerId);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderEdit([Bind(Include = "OrderId,CustomerId,PhoneNumber,Address,Name,Email,IsTrial,TotalPrice,Note,ShipPrice,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                order.UpdatedAt = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", order.CustomerId);
            return View(order);
        }

        // GET: Startup/StartupOrder/Delete/5
        public JsonResult DeleteOrder(long id)
        {
            try
            {
                bool result = orderDAO.DeleteById(id);
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
        public JsonResult DeleteOrderDetail(long id)
        {
            try
            {
                bool result = orderDetailDAO.DeleteById(id);
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

        [HttpGet]
        public ActionResult OrderDetails(long id)
        {
            var orderDetailIDbs = db.OrderDetails
                .Include(x=>x.Order).Include(x=>x.Product).Where(x => x.OrderId == id);
            ViewBag.OrderId = id;
            ViewBag.OrderInfo = db.Orders.Find(id);
            return View(orderDetailIDbs.ToList());
        }

        [HttpPost]
        public JsonResult UpdateOrderDetailStatus(int id, string status)
        {
            try
            {
                bool result = orderDetailDAO.UpdateStatus(id, status);
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

        [HttpPost]
        public JsonResult UpdateOrderStatus(int id, string status)
        {
            try
            {
                bool result = orderDAO.UpdateStatus(id, status);
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
        [HttpGet]
        public ActionResult OrderDetailCreate(long id)
        {
            var order = db.Orders.SingleOrDefault(o => o.OrderId == id);
            ViewBag.OrderId = order.OrderId;
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult OrderDetailCreate([Bind(Include = "CustomerId,ProductId,OrderId,Quality,TotalPrice,Status")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("OrderDetail",new { id=orderDetail.OrderId});
            }
            return View(orderDetail);
        }

        [HttpGet]
        public ActionResult OrderDetailEdit(long id)
        {
            var orderDetail = db.OrderDetails.Include(x=>x.Product).Include(x => x.Order).SingleOrDefault(o => o.OrderDetailId == id);
            if(orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        [HttpPost]
        public ActionResult OrderDetailEdit([Bind(Include = "OrderDetailId,Quality,TotalPrice,Status")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                OrderDetail orderDetailIDb = db.OrderDetails.SingleOrDefault(x => x.OrderDetailId == orderDetail.OrderDetailId);
                orderDetailIDb.Quality = orderDetail.Quality;
                orderDetailIDb.TotalPrice = orderDetail.TotalPrice;
                orderDetailIDb.Status = orderDetail.Status;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderDetail);
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
