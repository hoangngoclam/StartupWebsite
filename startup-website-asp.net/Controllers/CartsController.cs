using startup_website_asp.net.Models;
using startup_website_asp.net.Models.DAO;
using startup_website_asp.net.Models.EF;
using startup_website_asp.net.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace startup_website_asp.net.Controllers
{
    public class CartsController : Controller
    {
        private StartupWebsite db = new StartupWebsite();
        // GET: Carts
        public ActionResult ProductCart()
        {
            var cart = Session[Common.CommonSession.CART_SESSION];
            var list = new List<CartItemViewModel>();
            if (cart != null)
            {
                list = (List<CartItemViewModel>)cart;
            }
            ViewBag.ListProductInCart = list;
            long? SumAllPriceProduct = 0;
            long? PriceShip = 10000;
            foreach (var item in list)
            {
                SumAllPriceProduct += (item.Product.Price * item.Quantity);
            }
            ViewBag.SumAllPriceProduct = SumAllPriceProduct;
            ViewBag.PriceShip = PriceShip;
            ViewBag.SumAllPriceProductYouPay = SumAllPriceProduct + PriceShip;

            var listStartupId = new List<long>();

            foreach (var cartItem in list)
            {
                if (listStartupId.Contains(cartItem.Product.StartupId) == false)
                {
                    listStartupId.Add(cartItem.Product.StartupId);
                }
            }
            var listStartup = new List<Startup>();
            foreach (var startupId in listStartupId)
            {
                listStartup.Add(db.Startups.Find(startupId));
            }

            ViewBag.listStartup = listStartup;

            return View();
        }
        [HttpGet]
        public JsonResult Delete(string ID)
        {
            var array = ID.Split('-');

            long productID = Int64.Parse(array[0]);

            long startupId = Int64.Parse(array[1]);



            var HtmlCart = DeleteProductReturnHtml(productID);

            long SumAllPriceProduct = 0;
            long PriceShip = 10000;
            long SumAllPriceProductYouPay = 0;
            //long quantity = 0;
            var cart = Session[Common.CommonSession.CART_SESSION];
            var list = new List<CartItemViewModel>();
            if (cart != null)
            {
                list = (List<CartItemViewModel>)cart;
            }

            var listStartupId = new List<long>();

            foreach (var cartItem in list)
            {
                if (cartItem.Product.StartupId == startupId)
                {
                    SumAllPriceProduct += ((long)cartItem.Product.Price * (long)cartItem.Quantity);
                }
                //if (cartItem.Product.ProductId == productID)
                //{
                //    quantity = cartItem.Quantity;
                //}
            }

            SumAllPriceProductYouPay = SumAllPriceProduct + PriceShip;

            bool flas = false;
            foreach (var item in list)
            {
                if (item.Product.StartupId == startupId)
                {
                    flas = true;
                }
            }

            var result = new { HtmlCart = HtmlCart, SumAllPriceProduct = SumAllPriceProduct.ToString("N0"), PriceShip = PriceShip.ToString("N0"), SumAllPriceProductYouPay = SumAllPriceProductYouPay.ToString("N0"), StartupId = startupId, Flas = flas};
            return Json(result, JsonRequestBehavior.AllowGet);


        }

        public string DeleteProductReturnHtml(long productID)
        {
            var session = (startup_website_asp.net.Common.CustomerLogin)Session[startup_website_asp.net.Common.CommonSession.CUSTOMER_SESSION];
            if (session != null)
            {

                var listCartItem = new List<CartItemViewModel>();
                listCartItem = (List<CartItemViewModel>)Session[Common.CommonSession.CART_SESSION];
                foreach (var item in listCartItem)
                {
                    if (item.Product.ProductId == productID)
                    {
                        listCartItem.Remove(item);
                        db.Carts.Remove(db.Carts.SingleOrDefault(x => x.ProductId == productID && x.CustomerId == session.UserID));
                        db.SaveChanges();
                        break;
                    }
                }
                Session[Common.CommonSession.CART_SESSION] = listCartItem;

                string productStringHtml = "";
                long sumPriceAllProduct = 0;
                foreach (var item in listCartItem)
                {
                    productStringHtml += "<li id='product-cart-" + item.Product.ProductId + "'>"
                                 + "<a onclick='deleteProductCart(event)' class =" + "'remove btn-remove-product-cart' data-id='" + item.Product.ProductId + '-' + item.Product.StartupId + "'title=" + "'Xóa khỏi giỏ hàng'<i class='fa fa-remove'></i></a>"
                                 + "<a href =" + "'#' class = " + "'cart-img'> <img src='" + item.Product.MainImage + "'></a>"
                                 + "<h4><a href ='#'>" + item.Product.Name + "</a></h4>"
                                 + "<p class =" + "'quantity'>" + "<span id='id-quantity-" + item.Product.ProductId + "'>" + item.Quantity + "</span>" + " sp - " + "<span class = 'amount'>" + item.Product.Price + "</span></p>"
                            + "</li>";
                    sumPriceAllProduct += long.Parse(item.Product.Price.ToString()) * item.Quantity;
                }
                string result = "<a class =" + "'single-icon'>" + "<i class=" + "'ti-bag'></i>" + "<span class=" + "'total-count' id='countProduct'>" + listCartItem.Count + "</span></a>" +
                     "<div class =" + "'shopping-item'>" +
                        "<div class =" + "'dropdown-cart-header'>"
                            + "<span>" + listCartItem.Count + "</span>"
                            + "<span> sản phẩm</span>"
                            + "<a href =" + "'/Carts/ProductCart'> Xem giỏ hàng</a>"
                        + "</div>"
                        + "<ul class =" + "'shopping-list'>"
                            + productStringHtml
                        + "</ul>"
                         + "<div class ='bottom'>"
                            + "<div class ='total'>"
                                + "<span>Tổng cộng</span>"
                                + "<span class='total-amount' id='sum-price-all-product'>" + sumPriceAllProduct.ToString("N0") + "</span>"
                            + "</div>"
                         + "</div>"
                         + "<a href ='/Carts/ProductCart' class = 'btn animate w-100' style='color: white'> Thanh toán</a>"
                     + "</div>";
                return result;
            }
            else
            {
                var listCartItem = new List<CartItemViewModel>();
                listCartItem = (List<CartItemViewModel>)Session[Common.CommonSession.CART_SESSION];
                foreach (var item in listCartItem)
                {
                    if (item.Product.ProductId == productID)
                    {
                        listCartItem.Remove(item);
                        break;
                    }
                }
                Session[Common.CommonSession.CART_SESSION] = listCartItem;


                string productStringHtml = "";
                long sumPriceAllProduct = 0;
                foreach (var item in listCartItem)
                {
                    productStringHtml += "<li id='product-cart-" + item.Product.ProductId + "'>"
                                 + "<a onclick='deleteProductCart(event)' class =" + "'remove btn-remove-product-cart' data-id='" + item.Product.ProductId +'-'+ item.Product.StartupId+ "'title=" + "'Xóa khỏi giỏ hàng'<i class='fa fa-remove'></i></a>"
                                 + "<a href =" + "'#' class = " + "'cart-img'> <img src='" + item.Product.MainImage + "'></a>"
                                 + "<h4><a href ='#'>" + item.Product.Name + "</a></h4>"
                                 + "<p class =" + "'quantity'>" + "<span id='id-quantity-" + item.Product.ProductId + "'>" + item.Quantity + "</span>" + " sp - " + "<span class = 'amount'>" + item.Product.Price + "</span></p>"
                            + "</li>";
                    sumPriceAllProduct += long.Parse(item.Product.Price.ToString()) * item.Quantity;
                }
                string result = "<a class =" + "'single-icon'>" + "<i class=" + "'ti-bag'></i>" + "<span class=" + "'total-count' id='countProduct'>" + listCartItem.Count + "</span></a>" +
                     "<div class =" + "'shopping-item'>" +
                        "<div class =" + "'dropdown-cart-header'>"
                            + "<span>" + listCartItem.Count + "</span>"
                            + "<span> sản phẩm</span>"
                            + "<a href =" + "'/Carts/ProductCart'> Xem giỏ hàng</a>"
                        + "</div>"
                        + "<ul class =" + "'shopping-list'>"
                            + productStringHtml
                        + "</ul>"
                         + "<div class ='bottom'>"
                            + "<div class ='total'>"
                                + "<span>Tổng cộng</span>"
                                + "<span class='total-amount' id='sum-price-all-product'>" + sumPriceAllProduct.ToString("N0") + "</span>"
                            + "</div>"
                         + "</div>"
                         + "<a href ='/Carts/ProductCart' class = 'btn animate w-100' style='color: white'> Thanh toán</a>"
                     + "</div>";
                return result;
            }
        }


        [HttpPost]
        public JsonResult AddItem(long? productID)
        {
            var session = (startup_website_asp.net.Common.CustomerLogin)Session[startup_website_asp.net.Common.CommonSession.CUSTOMER_SESSION];
            //Nếu id sản phẩm truyền vào rỗng (Lúc tải lại trang)
            if (productID == null)
            {
                List<CartItemViewModel> listCartItem = new List<CartItemViewModel>();
                listCartItem = (List<CartItemViewModel>)Session[Common.CommonSession.CART_SESSION];

                if (listCartItem == null)
                {
                    if (session != null)
                    {
                        List<Cart> carts = new List<Cart>();
                        carts = db.Carts.Where(x => x.CustomerId == session.UserID).ToList();
                        var listCartItemxx = new List<CartItemViewModel>();
                        foreach (var item in carts)
                        {
                            var cartItem = new CartItemViewModel();
                            cartItem.Product = db.Products.Find(item.Product.ProductId);
                            cartItem.Quantity = item.Quality;

                            listCartItemxx.Add(cartItem);

                        }
                        Session[Common.CommonSession.CART_SESSION] = listCartItemxx;//Cập nhật lại session
                        listCartItem = listCartItemxx;
                    }
                    else
                    {
                        string result =
                         "<a class =" + "'single-icon'>" + "<i class=" + "'ti-bag'></i>" + "<span class=" + "'total-count' id='countProduct'>" + 0 + "</span></a>" +
                        "<div class =" + "'shopping-item'>" +
                           "<div class =" + "'dropdown-cart-header'>" +
                               "<span> 0 </span>" +
                               "<span> sản phẩm</span>" +
                               "<a href =" + "'/Carts/ProductCart'> Xem giỏ hàng"
                               + "</a>"
                           + "</div>"
                            + "<div class ='bottom'>"
                               + "<div class ='total'>"
                                   + "<span> Tổng cộng </span>"
                                    + "<span class='total-amount'>" + 0 + "</span>"
                               + "</div>"
                           + "</div>"
                            + "<a href ='/Carts/ProductCart' class = 'btn animate w-100' style='color: white'> Thanh toán</a>"
                        + "</div>";
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
                //Nếu giỏ hàng số lượng = 0
                if (listCartItem.Count == 0)
                {
                    if (session != null)
                    {
                        List<Cart> carts = new List<Cart>();
                        carts = db.Carts.Where(x => x.CustomerId == session.UserID).ToList();

                        foreach (var item in carts)
                        {
                            CartItemViewModel cartItem = new CartItemViewModel();

                            cartItem.Product = db.Products.SingleOrDefault(x => x.ProductId == item.Product.ProductId);
                            cartItem.Quantity = item.Quality;

                            listCartItem.Add(cartItem);
                        }
                        Session[Common.CommonSession.CART_SESSION] = listCartItem;//Cập nhật lại session
                    }

                    string result =
                      "<a class =" + "'single-icon'>" + "<i class=" + "'ti-bag'></i>" + "<span class=" + "'total-count' id='countProduct'>" + 0 + "</span></a>" +
                     "<div class =" + "'shopping-item'>" +
                        "<div class =" + "'dropdown-cart-header'>" +
                            "<span> 0 </span>" +
                            "<span> sản phẩm</span>" +
                            "<a href =" + "'/Carts/ProductCart'> Xem giỏ hàng"
                            + "</a>"
                        + "</div>"
                         + "<div class ='bottom'>"
                            + "<div class ='total'>"
                                + "<span> Tổng cộng </span>"
                                 + "<span class='total-amount'>" + 0 + "</span>"
                            + "</div>"
                        + "</div>"
                         + "<a href ='/Carts/ProductCart' class = 'btn animate w-100' style='color: white'> Thanh toán</a>"
                     + "</div>";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                //Nếu giỏ hàng có sản phẩm
                else
                {
                    if (session != null)
                    {
                        List<Cart> carts = new List<Cart>();
                        carts = db.Carts.Where(x => x.CustomerId == session.UserID).ToList();
                        if (carts != null)
                        {
                            foreach (var item in carts)
                            {
                                db.Carts.Remove(item);
                            }
                            db.SaveChanges();
                        }

                        foreach (var item in listCartItem)
                        {
                            Cart cartItem = new Cart();

                            cartItem.ProductId = item.Product.ProductId;
                            cartItem.Quality = item.Quantity;
                            cartItem.CustomerId = session.UserID;

                            db.Carts.Add(cartItem);
                        }
                        db.SaveChanges();
                    }


                    string productStringHtml = "";
                    long sumPriceAllProduct = 0;
                    foreach (var item in listCartItem)
                    {
                        productStringHtml += "<li id='product-cart-" + item.Product.ProductId + "'>"
                                     + "<a onclick='deleteProductCart(event)' class =" + "'remove btn-remove-product-cart' data-id='" + item.Product.ProductId + '-' + item.Product.StartupId + "'title=" + "'Xóa khỏi giỏ hàng'<i class='fa fa-remove'></i></a>"
                                     + "<a href =" + "'#' class = " + "'cart-img'> <img src='" + item.Product.MainImage + "'></a>"
                                       + "<h4><a href ='#'>" + item.Product.Name + "</a></h4>"
                                     + "<p class =" + "'quantity'>" + "<span id='id-quantity-" + item.Product.ProductId + "'>" + item.Quantity + "</span>" + " sp - " + "<span class = 'amount'>" + item.Product.Price + "</span></p>"
                                + "</li>";

                        sumPriceAllProduct += long.Parse(item.Product.Price.ToString()) * item.Quantity;
                    }
                    string result = "<a class =" + "'single-icon'>" + "<i class=" + "'ti-bag'></i>" + "<span class=" + "'total-count' id='countProduct'>" + listCartItem.Count + "</span></a>" +
                         "<div class =" + "'shopping-item'>" +
                            "<div class =" + "'dropdown-cart-header'>" +
                                "<span>" + listCartItem.Count + "</span>"
                                + "<span> sản phẩm</span>" +
                                "<a href =" + "'/Carts/ProductCart'> Xem giỏ hàng</a>"
                            + "</div>"
                            + "<ul class =" + "'shopping-list'>"

                                 + productStringHtml

                            + "</ul>"

                             + "<div class ='bottom'>"
                                + "<div class ='total'>"
                                    + "<span>Tổng cộng</span>"
                                     + "<span class='total-amount' id='sum-price-all-product'>" + sumPriceAllProduct + "</span>"
                                + "</div>"
                             + "</div>"
                             + "<a href ='/Carts/ProductCart' class = 'btn animate w-100' style='color: white'> Thanh toán</a>"
                         + "</div>";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            //Nếu id sản phẩm truyền vào có giá trị
            else
            {
                int quantity = 1;
                var product = new ProductDAO().GetProductById(productID);
                var cart = Session[Common.CommonSession.CART_SESSION];
                /*int count = 0; *///Đếm sản phẩm có trong giỏ hàng


                if (cart != null)//Nếu giỏ hàng có sản phẩm
                {
                    var listCartItem = (List<CartItemViewModel>)cart;

                    if (listCartItem.Exists(x => x.Product.ProductId == productID))//Nếu sản phẩm đó có trong giỏ hàng, sẽ tăng số lượng lên 1
                    {
                        foreach (var cartItem in listCartItem)
                        {
                            if (cartItem.Product.ProductId == productID)
                            {
                                cartItem.Quantity += quantity;
                            }
                        }
                    }
                    else //Nếu sản phẩm đó chưa có trong giỏ hàng, thêm 1 cardItem mới với sản phẩm +  số lượng là 1 
                    {
                        var cartItem = new CartItemViewModel();
                        cartItem.Product = product;
                        cartItem.Quantity = quantity;
                        listCartItem.Add(cartItem);
                    }

                    Session[Common.CommonSession.CART_SESSION] = listCartItem; //Cập nhật lại session
                    /*count = listCartItem.Count;*/ //Cập nhật lại đếm sản phẩm
                }
                //Nếu giỏ hàng chưa có sản phẩm, sẽ khai báo và thêm một cartItem mới vào session
                else
                {
                    var listCartItem = new List<CartItemViewModel>();

                    var cartItem = new CartItemViewModel();
                    cartItem.Product = product;
                    cartItem.Quantity = quantity;

                    listCartItem.Add(cartItem);

                    Session[Common.CommonSession.CART_SESSION] = listCartItem;//Cập nhật lại session

                    /* count = listCartItem.Count;*///Cập nhật lại đếm sản phẩm
                }

                //string countItem = count.ToString();
                var ResultListCartItem = new List<CartItemViewModel>();
                ResultListCartItem = (List<CartItemViewModel>)Session[Common.CommonSession.CART_SESSION];

                //Lưu cart vào dữ liệu
                if (session != null)
                {
                    List<Cart> carts = new List<Cart>();
                    carts = db.Carts.Where(x => x.CustomerId == session.UserID).ToList();
                    if (carts != null)
                    {
                        foreach (var item in carts)
                        {
                            db.Carts.Remove(item);
                        }
                        db.SaveChanges();
                    }
                    foreach (var item in ResultListCartItem)
                    {
                        Cart cartItem = new Cart();

                        cartItem.ProductId = item.Product.ProductId;
                        cartItem.Quality = item.Quantity;
                        cartItem.CustomerId = session.UserID;

                        db.Carts.Add(cartItem);
                    }
                    db.SaveChanges();
                }

                string productStringHtml = "";
                long sumPriceAllProduct = 0;
                foreach (var item in ResultListCartItem)
                {
                    productStringHtml += "<li id='product-cart-" + item.Product.ProductId + "'>"
                                 + "<a onclick='deleteProductCart(event)' class =" + "'remove btn-remove-product-cart' data-id='" + item.Product.ProductId + '-' + item.Product.StartupId + "'title=" + "'Xóa khỏi giỏ hàng'<i class='fa fa-remove'></i></a>"
                                 + "<a href =" + "'#' class = " + "'cart-img'> <img src='" + item.Product.MainImage + "'></a>"
                                 + "<h4><a href ='#'>" + item.Product.Name + "</a></h4>"
                                 + "<p class =" + "'quantity'>" + "<span id='id-quantity-" + item.Product.ProductId + "'>" + item.Quantity + " sp - " + "<span class = 'amount'>" + item.Product.Price + "</span></p>"
                            + "</li>";

                    sumPriceAllProduct += long.Parse(item.Product.Price.ToString()) * item.Quantity;
                }
                string result = "<a class =" + "'single-icon'>" + "<i class=" + "'ti-bag'></i>" + "<span class=" + "'total-count' id='countProduct'>" + ResultListCartItem.Count + "</span></a>" +
                     "<div class =" + "'shopping-item'>" +
                        "<div class =" + "'dropdown-cart-header'>" +
                            "<span>" + ResultListCartItem.Count + "</span>"
                            + "<span> sản phẩm</span>" +
                            "<a href =" + "'/Carts/ProductCart'> Xem giỏ hàng</a>"
                        + "</div>"
                        + "<ul class =" + "'shopping-list'>"
                             + productStringHtml
                        + "</ul>"
                           + "<div class ='bottom'>"
                                + "<div class ='total'>"
                                    + "<span >Tổng cộng</span>"
                                     + "<span  class='total-amount' id='sum-price-all-product'>" + sumPriceAllProduct + "</span>"
                                + "</div>"
                             + "</div>"
                             + "<a href ='/Carts/ProductCart' class = 'btn animate w-100' style='color: white'> Thanh toán</a>"
                     + "</div>";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Plus(string ID)
        {
            var array = ID.Split('-');

            long productID = Int64.Parse(array[0]);

            long startupId = Int64.Parse(array[1]);

            var listCartItem = new List<CartItemViewModel>();

            listCartItem = (List<CartItemViewModel>)Session[Common.CommonSession.CART_SESSION];

            long totalPriceProduct = 0;
            long totalAllPriceProduct = 0;
            int quantity = 0;
            long totalAllPriceProductCart = 0;

            foreach (var item in listCartItem)
            {
                if (item.Product.StartupId == startupId)
                {
                    if (item.Product.ProductId == productID)
                    {
                        if (item.Quantity <= 99 && item.Quantity >= 1)
                        {
                            item.Quantity++;
                            totalPriceProduct = item.Quantity * (long)item.Product.Price;
                            quantity = item.Quantity;
                            break;
                        }
                        else
                        {
                            totalPriceProduct = item.Quantity * (long)item.Product.Price;
                            quantity = item.Quantity;
                            break;
                        }
                    }
                }
            }

            foreach (var item in listCartItem)
            {
                totalAllPriceProductCart += item.Quantity * (long)item.Product.Price;
                if (item.Product.StartupId == startupId)
                {
                    totalAllPriceProduct += item.Quantity * (long)item.Product.Price;
                }
            }

            Session[Common.CommonSession.CART_SESSION] = listCartItem;

            var session = (startup_website_asp.net.Common.CustomerLogin)Session[startup_website_asp.net.Common.CommonSession.CUSTOMER_SESSION];

            if (session != null)
            {
                List<Cart> carts = new List<Cart>();
                carts = db.Carts.Where(x => x.CustomerId == session.UserID).ToList();
                if (carts != null)
                {
                    foreach (var item in carts)
                    {
                        db.Carts.Remove(item);
                    }
                    db.SaveChanges();
                }

                foreach (var item in listCartItem)
                {
                    Cart cartItem = new Cart();

                    cartItem.ProductId = item.Product.ProductId;
                    cartItem.Quality = item.Quantity;
                    cartItem.CustomerId = session.UserID;

                    db.Carts.Add(cartItem);
                }
                db.SaveChanges();
            }

            long priceShip = 10000;
            long totalAllPriceProductToPay = totalAllPriceProduct + priceShip;

            var result = new { Quantity = quantity, TotalAllPriceProductCart = totalAllPriceProductCart.ToString("N0"), TotalPriceProduct = totalPriceProduct.ToString("N0"), TotalAllPriceProduct = totalAllPriceProduct.ToString("N0"), TotalAllPriceProductToPay = totalAllPriceProductToPay.ToString("N0"), PriceShip = priceShip.ToString("N0"), StartupId = startupId, ProductId = productID };
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JsonResult Minus(string ID)
        {
            var array = ID.Split('-');

            long productID = Int64.Parse(array[0]);

            long startupId = Int64.Parse(array[1]);

            var listCartItem = new List<CartItemViewModel>();

            listCartItem = (List<CartItemViewModel>)Session[Common.CommonSession.CART_SESSION];

            long totalPriceProduct = 0;
            long totalAllPriceProduct = 0;
            int quantity = 0;
            long totalAllPriceProductCart = 0;

            foreach (var item in listCartItem)
            {
                if (item.Product.ProductId == productID)
                {
                    if (item.Quantity >= 2)
                    {
                        item.Quantity = item.Quantity - 1;
                        totalPriceProduct = item.Quantity * (long)item.Product.Price;
                        quantity = item.Quantity;
                        break;
                    }
                    else
                    {
                        item.Quantity = 1;
                        totalPriceProduct = item.Quantity * (long)item.Product.Price;
                        quantity = item.Quantity;
                        break;
                    }
                }
            }

            foreach (var item in listCartItem)
            {
                totalAllPriceProductCart += item.Quantity * (long)item.Product.Price;
                if (item.Product.StartupId == startupId)
                {
                    totalAllPriceProduct += item.Quantity * (long)item.Product.Price;
                }
            }

            Session[Common.CommonSession.CART_SESSION] = listCartItem;

            var session = (startup_website_asp.net.Common.CustomerLogin)Session[startup_website_asp.net.Common.CommonSession.CUSTOMER_SESSION];

            if (session != null)
            {
                List<Cart> carts = new List<Cart>();
                carts = db.Carts.Where(x => x.CustomerId == session.UserID).ToList();
                if (carts != null)
                {
                    foreach (var item in carts)
                    {
                        db.Carts.Remove(item);
                    }
                    db.SaveChanges();
                }

                foreach (var item in listCartItem)
                {
                    Cart cartItem = new Cart();

                    cartItem.ProductId = item.Product.ProductId;
                    cartItem.Quality = item.Quantity;
                    cartItem.CustomerId = session.UserID;

                    db.Carts.Add(cartItem);
                }
                db.SaveChanges();
            }

            long priceShip = 10000;
            long totalAllPriceProductToPay = totalAllPriceProduct + priceShip;

            var result = new { Quantity = quantity, TotalAllPriceProductCart = totalAllPriceProductCart.ToString("N0"), TotalPriceProduct = totalPriceProduct.ToString("N0"), TotalAllPriceProduct = totalAllPriceProduct.ToString("N0"), TotalAllPriceProductToPay = totalAllPriceProductToPay.ToString("N0"), PriceShip = priceShip.ToString("N0"), StartupId = startupId, ProductId = productID };
            return Json(result, JsonRequestBehavior.AllowGet);
        } 


       
    }
}
   