using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models.DAO
{
    public class OrderDetailDAO:BaseDAO
    {
        public bool DeleteById(long orderDetailId)
        {
            var orderDetailIDb = db.OrderDetails.SingleOrDefault(x => x.OrderDetailId == orderDetailId);
            if (orderDetailIDb == null)
            {
                return false;
            }
            orderDetailIDb.Status = "Đã xóa";
            db.SaveChanges();
            return true;
        }
        public bool UpdateStatus(long orderDetailId, string status)
        {
            var orderDetailIDb = db.OrderDetails.SingleOrDefault(x => x.OrderDetailId == orderDetailId);
            if (orderDetailIDb == null)
            {
                return false;
            }
            orderDetailIDb.Status = status;
            db.SaveChanges();
            return true;
            return false;
        }
    }
}