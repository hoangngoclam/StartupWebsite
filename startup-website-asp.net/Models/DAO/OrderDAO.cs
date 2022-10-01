using startup_website_asp.net.BusinessLayer;
using startup_website_asp.net.Common;
using startup_website_asp.net.Controllers;
using startup_website_asp.net.Models.EF;
using startup_website_asp.net.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static startup_website_asp.net.Areas.Startup.Controllers.StartupOrderController;

namespace startup_website_asp.net.Models.DAO
{
    public class OrderDAO : BaseDAO
    {
        private CartDAO cartDAO = new CartDAO();
        private BLOrder BLOrder = new BLOrder();
        public bool CreateOrderbyTrialSubscription(TrialSubscriptionViewModel trialViewModel)
        {
            this.AddCustomerInfo(trialViewModel.CustomerId, trialViewModel.PhoneNumber, trialViewModel.Address, trialViewModel.Email);
            Order donDungThu = new Order();
            OrderDetail orderDetail = new OrderDetail();
            donDungThu.CustomerId = trialViewModel.CustomerId;
            donDungThu.Name = trialViewModel.Name;
            donDungThu.Address = trialViewModel.Address;
            donDungThu.IsTrial = true;
            donDungThu.StartupId = (long)trialViewModel.StartupId;
            donDungThu.PhoneNumber = trialViewModel.PhoneNumber;
            donDungThu.Note = trialViewModel.Note;
            donDungThu.Email = trialViewModel.Email;
            donDungThu.Status = "ChuaDuyet";
            donDungThu.CreatedAt = DateTime.Now;
            db.Orders.Add(donDungThu);
            db.SaveChanges();
            orderDetail.ProductId = trialViewModel.ProductId;
            orderDetail.OrderId = donDungThu.OrderId;
            orderDetail.Quality = 1;
            orderDetail.CreatedAt = DateTime.Now;
            db.OrderDetails.Add(orderDetail);
            db.SaveChanges();
            return true;            
        }

        //Add info for customer if didn't have
        private void AddCustomerInfo(Guid customerId, string phoneNumber, string address, string email)
        {
            Customer customer = db.Customers.Find(customerId);
            if (customer.PhoneNumber == null || customer.PhoneNumber == "")
            {
                customer.PhoneNumber = phoneNumber;
            }
            if (customer.Address == null || customer.Address == "")
            {
                customer.Address = address;
            }
            if (customer.Email == null || customer.Email == "")
            {
                customer.Email = email;
            }
            db.SaveChanges();
        }
        public long CreateOrderByPayment(PaymentViewModel paymentModel, List<CartItemViewModel> cartViewModels)
        {
            this.AddCustomerInfo(paymentModel.CustomerId, paymentModel.PhoneNumber, paymentModel.Address, paymentModel.Email);
            Order donHang = new Order();
            OrderDetail orderDetail = new OrderDetail();
            donHang.CustomerId = paymentModel.CustomerId;
            donHang.Name = paymentModel.Name;
            donHang.Address = paymentModel.Address;
            donHang.IsTrial = false;
            donHang.PhoneNumber = paymentModel.PhoneNumber;
            donHang.Note = paymentModel.Note;
            donHang.Status = StatusOrder.ChuaDuyet.ToString();
            donHang.Email = paymentModel.Email;
            donHang.CreatedAt = DateTime.Now;
            db.Orders.Add(donHang);
            db.SaveChanges();
            AddMulOrderDetailByCartVM(cartViewModels, donHang.OrderId);
            cartDAO.DeleteCartByCustomerId(paymentModel.CustomerId);
            return donHang.OrderId;
        }
        public List<Cart> GetListCartItemByStartupId(long startupId, Guid customerId)
        {
            List<Cart> cartItemViewModels = cartDAO.getCartByStartupId(startupId, customerId);
            return cartItemViewModels;
        }

        public bool CreateOrderByStartupId(PaymentViewModel paymentModel, long startupId)
        {
            long totalPrice = 0;
            this.AddCustomerInfo(paymentModel.CustomerId, paymentModel.PhoneNumber, paymentModel.Address, paymentModel.Email);
            List<Cart> cartModels = GetListCartItemByStartupId(startupId, paymentModel.CustomerId);
            Order donHang = new Order();
            donHang.CustomerId = paymentModel.CustomerId;
            donHang.Name = paymentModel.Name;
            donHang.Address = paymentModel.Address;
            donHang.IsTrial = false;
            donHang.PhoneNumber = paymentModel.PhoneNumber;
            donHang.Note = paymentModel.Note;
            donHang.Status = StatusOrder.ChuaDuyet.ToString();
            donHang.Email = paymentModel.Email;
            donHang.CreatedAt = DateTime.Now;
            db.Orders.Add(donHang);
            db.SaveChanges();
            totalPrice =  AddMulOrderDetailByCartM(cartModels, donHang.OrderId);
            UpdateTotalPriceforOrder(totalPrice,donHang.OrderId);
            cartDAO.DeleteCartByCustomerId(paymentModel.CustomerId,startupId);
            return true;
        }

        public long CreateOrderByStartupId(PaymentViewModel paymentModel, long startupId, List<CartItemViewModel> cartViewModels)
        {
            long totalPrice = 0;
            this.AddCustomerInfo(paymentModel.CustomerId, paymentModel.PhoneNumber, paymentModel.Address, paymentModel.Email);
            List<CartItemViewModel> cartVMsByStartupId = cartViewModels.Where(x=>x.Product.StartupId == startupId).ToList();
            Order donHang = new Order();
            donHang.CustomerId = paymentModel.CustomerId;
            donHang.Name = paymentModel.Name;
            donHang.Address = paymentModel.Address;
            donHang.IsTrial = false;
            donHang.PhoneNumber = paymentModel.PhoneNumber;
            donHang.Note = paymentModel.Note;
            donHang.Status = StatusOrder.ChuaDuyet.ToString();
            donHang.Email = paymentModel.Email;
            donHang.CreatedAt = DateTime.Now;
            donHang.StartupId = startupId;
            db.Orders.Add(donHang);
            db.SaveChanges();
            totalPrice = AddMulOrderDetailByCartVM(cartVMsByStartupId, donHang.OrderId);
            UpdateTotalPriceforOrder(totalPrice, donHang.OrderId);
            cartDAO.DeleteCartByCustomerId(paymentModel.CustomerId);
            return donHang.OrderId;
        }

        public long AddMulOrderDetailByCartM(List<Cart> cartViewModels, long orderId)
        {
            long totalPrice = 0;
            DateTime timeNow = DateTime.Now;
            foreach (Cart item in cartViewModels)
            {
                OrderDetail newOrderDetailItem = new OrderDetail();
                newOrderDetailItem.ProductId = item.Product.ProductId;
                newOrderDetailItem.OrderId = orderId;
                newOrderDetailItem.Quality = item.Quality;
                newOrderDetailItem.TotalPrice =
                    (item.Product.PromotionPrice == 0 || item.Product.PromotionPrice == null) ?
                    item.Product.Price * item.Quality : item.Product.PromotionPrice * item.Quality;
                newOrderDetailItem.CreatedAt = timeNow;
                db.OrderDetails.Add(newOrderDetailItem);
                totalPrice += (long)newOrderDetailItem.TotalPrice;
            }
            db.SaveChanges();
            return totalPrice;
        }
        public long AddMulOrderDetailByCartVM(List<CartItemViewModel> cartViewModels, long orderId)
        {
            long totalPrice = 0;
            DateTime timeNow = DateTime.Now;
            foreach (CartItemViewModel item in cartViewModels)
            {
                OrderDetail newOrderDetailItem = new OrderDetail();
                newOrderDetailItem.ProductId = item.Product.ProductId;
                newOrderDetailItem.OrderId = orderId;
                newOrderDetailItem.Quality = item.Quantity;
                newOrderDetailItem.TotalPrice =
                    (item.Product.PromotionPrice == 0 || item.Product.PromotionPrice == null) ?
                    item.Product.Price * item.Quantity : item.Product.PromotionPrice * item.Quantity;
                newOrderDetailItem.CreatedAt = timeNow;
                db.OrderDetails.Add(newOrderDetailItem);
                totalPrice += (long)newOrderDetailItem.TotalPrice;
            }
            db.SaveChanges();
            return totalPrice;
        }
        public void UpdateTotalPriceforOrder(long totalPrice, long orderId)
        {
            Order orderIdb = db.Orders.Find(orderId);
            orderIdb.TotalPrice = totalPrice;
            db.SaveChanges();
        }
        
        public bool DeleteById(long orderId)
        {
            var orderIDb = db.Orders.SingleOrDefault(x => x.OrderId == orderId);
            if (orderIDb == null)
            {
                return false;
            }
            orderIDb.Status = "Đã xóa";
            db.SaveChanges();
            return true;
        }
        public bool UpdateStatus(long orderId, string status)
        {
            var orderIDb = db.Orders.Find(orderId);
            if (orderIDb == null)
            {
                return false;
            }
            orderIDb.Status = status;
            db.SaveChanges();
            return true;
        }
        public List<OrderViewModel> GetListByCustomerId(Guid customerId)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            ICollection<Order> ordersIdb = db.Orders.Where(x=>x.CustomerId == customerId).ToList();
            result = BLOrder.GetListOrderViewModel(ordersIdb);
            return result;
        }
    }
}