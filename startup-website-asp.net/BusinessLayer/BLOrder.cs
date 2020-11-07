using startup_website_asp.net.Models.EF;
using startup_website_asp.net.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.BusinessLayer
{
    public class BLOrder
    {
        public OrderViewModel GetOrderViewModel(Order order)
        {
            OrderViewModel result = new OrderViewModel(order);
            return result;
        }
        public List<OrderViewModel> GetListOrderViewModel(ICollection<Order> orders)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (Order orderItem in orders)
            {
                OrderViewModel orderVMItem = new OrderViewModel(orderItem);
                orderVMItem.OrderDetails = orderItem.OrderDetails;
                result.Add(orderVMItem);
            }
            return result;
        }
    }
}