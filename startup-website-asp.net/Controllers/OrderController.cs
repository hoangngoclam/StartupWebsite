using startup_website_asp.net.Models.EF;
using startup_website_asp.net.ViewModels;
using startup_website_asp.net.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using startup_website_asp.net.Common;

namespace startup_website_asp.net.Controllers
{
    public class OrderController:BaseController
    {
        // GET: Order
        private OrderDAO orderDAO = new OrderDAO();
        private StartupWebsite db = new StartupWebsite();
        [HttpGet]
        public ActionResult Payment(long startupId, long sumProduct)
        {
            try
            {
                var customerSession = (CustomerLogin)Session[CommonSession.CUSTOMER_SESSION];
                var listCartSession = (List<CartItemViewModel>)Session[CommonSession.CART_SESSION];
                Customer customerInfo = db.Customers.Find(customerSession.UserID);
                if (customerSession != null)
                {
                    PaymentViewModel model = new PaymentViewModel(
                            customerInfo.Name, customerInfo.PhoneNumber,
                            customerInfo.Address, customerInfo.Email);
                    if (listCartSession.Count() <= 0)
                    {
                        return RedirectToAction("ProductCart", "Carts");
                    }
                    //ViewBag.TotalPrice = listCartSession.Sum(x => {
                    //    if (x.Product.PromotionPrice == 0 || x.Product.PromotionPrice == null)
                    //    { 
                    //        return x.Product.Price * x.Quantity; 
                    //    }
                    //    else 
                    //        return x.Product.PromotionPrice * x.Quantity; 
                    //    }
                    //);
                    ViewBag.TotalPrice = sumProduct;

                    ViewBag.startupId = startupId;
                    return View(model);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult Payment(PaymentViewModel paymentViewModel, long startupId)
        {
            try
            {
                var session = (CustomerLogin)Session[CommonSession.CUSTOMER_SESSION];
                var listCartSession = (List<CartItemViewModel>)Session[CommonSession.CART_SESSION];
                paymentViewModel.CustomerId = session.UserID;
                long orderId = orderDAO.CreateOrderByStartupId(paymentViewModel,startupId,listCartSession);
                if (orderId>0)
                {
                    Session[CommonSession.CART_SESSION] = null;
                    Session[CommonSession.ORDERID_SESSION] = orderId;
                    return RedirectToAction("SuccessfulOrder","Order");
                }
                else
                {
                    return View(paymentViewModel);
                }
            }
            catch (Exception)
            {
                return View(paymentViewModel);
            }
        }
        [HttpGet]
        public ActionResult TrialSubscription(long id)
        {
            try
            {
                var session = (CustomerLogin)Session[CommonSession.CUSTOMER_SESSION];
                Customer customerInfo = db.Customers.Find(session.UserID);
                Product productModel = db.Products.Find(id);
                
                if (session != null && productModel.isTrial == true) 
                {
                    TrialSubscriptionViewModel model = new TrialSubscriptionViewModel(
                            customerInfo.Name, customerInfo.PhoneNumber,
                            customerInfo.Address, customerInfo.Email, id,productModel.StartupId
                            );

                    return View(model);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
           
        }
        [HttpPost]
        public ActionResult TrialSubscription(TrialSubscriptionViewModel trialSubscription)
        {
            try
            {
                var session = (CustomerLogin)Session[CommonSession.CUSTOMER_SESSION];
                trialSubscription.CustomerId = session.UserID;
                if (orderDAO.CreateOrderbyTrialSubscription(trialSubscription))
                {
                    return RedirectToAction("SuccessfulOrder", "Order");
                }
                else
                {
                    return View(trialSubscription);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("TrialSubscription",new {id= trialSubscription.ProductId });
            }
        }

        public ActionResult SuccessfulOrder()
        {
            var sessionOrderId = Session[CommonSession.ORDERID_SESSION];
            if (sessionOrderId != null)
            {
                ViewBag.OrderId = sessionOrderId.ToString();
                Session[CommonSession.ORDERID_SESSION] = null;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}