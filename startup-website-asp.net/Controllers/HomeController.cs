using startup_website_asp.net.Models;
using startup_website_asp.net.Models.EF;
using startup_website_asp.net.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using startup_website_asp.net.Common;
using startup_website_asp.net.ViewModels;

namespace startup_website_asp.net.Controllers
{
    public class HomeController : Controller
    {
        private ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
        private StartupWebsite db = new StartupWebsite();

        public ActionResult Index()
        {
            var session = (CustomerLogin)Session[CommonSession.CUSTOMER_SESSION];

            //Truyền danh sách sản phẩm vào trang chủ

            if (session!=null)
            {
                //Lấy ra danh sách PrductLiked của khách hàng
                List<ProductLiked> ListProductLiked = ListProductLikeds(session.UserID);

                //-----------Sản phẩm dùng thử--------------------------
                //Khai báo danh sách Product dùng thử của khách hàng đã liked
                List<Product> ListProductIstrialInListProductsLikedByCustomer = new List<Product>();
                //Lấy ra danh sách sản phẩm dùng thử
                List<Product> ListProductIstrial = db.Products.Where(x=>x.isTrial==true && x.Status == "Hiện").Take(8).ToList();
                //Xử lý và truyền danh sách sản phẩm dùng thử và danh sách sản phẩm dùng thử đã được khách hàng like
                foreach (var product in ListProductIstrial)
                {
                    if (FindProductInListProductLikeds(product.ProductId, ListProductLiked) ==true)
                    {
                        ListProductIstrialInListProductsLikedByCustomer.Add(product);
                    }
                }
                ViewBag.ListProductIstrial = ListProductIstrial;
                ViewBag.ListProductIstrialInListProductsLikedByCustomer = ListProductIstrialInListProductsLikedByCustomer;
                //-----------Sản phẩm giảm giá------------------------------
                //Khai báo danh sách Product dùng thử của khách hàng đã liked
                List<Product> ListProducHavePromotionInListProductsLikedByCustomer = new List<Product>();
                //Lấy ra danh sách sản phẩm dùng thử
                List<Product> ListProductHavePromotion = db.Products.Where(x => x.PromotionPrice != 0 && x.Status == "Hiện").Take(8).ToList();
                //Xử lý và truyền danh sách sản phẩm dùng thử và danh sách sản phẩm dùng thử đã được khách hàng like
                foreach (var product in ListProductHavePromotion)
                {
                    if (FindProductInListProductLikeds(product.ProductId, ListProductLiked) == true)
                    {
                        ListProducHavePromotionInListProductsLikedByCustomer.Add(product);
                    }
                }
                ViewBag.ListProductHavePromotion = ListProductHavePromotion;
                ViewBag.ListProducHavePromotionInListProductsLikedByCustomer = ListProducHavePromotionInListProductsLikedByCustomer;

                //-----------Sản phẩm có hàng tặng kèm------------------------------
                //Khai báo danh sách Product có sản phẩm dùng thử của khách hàng đã liked
                List<Product> ListProducHaveGiftInListProductsLikedByCustomer = new List<Product>();
                //Lấy ra danh sách sản phẩm có sản phẩm tặng kèm
                List<Product> ListProductHaveGift = FindListProductHaveGift().Take(8).ToList();

                //Xử lý và truyền danh sách sản phẩm có sản phẩm tặng kèm và danh sách sản phẩm có sản phẩm tặng kèm đã được khách hàng like
                foreach (var product in ListProductHaveGift)
                {
                    if (FindProductInListProductLikeds(product.ProductId, ListProductLiked) == true)
                    {
                        ListProducHaveGiftInListProductsLikedByCustomer.Add(product);
                    }
                }
                ViewBag.ListProductHaveGift = ListProductHaveGift;
                ViewBag.ListProducHaveGiftInListProductsLikedByCustomer = ListProducHaveGiftInListProductsLikedByCustomer;
                
            }
            else
            {
                ViewBag.ListProductIstrialSessionNull = db.Products.Where(x => x.isTrial == true && x.Status == "Hiện").Take(8).ToList();
                ViewBag.ListProductHavePromotionSessionNull = db.Products.Where(x => x.PromotionPrice != 0 && x.Status == "Hiện").Take(8).ToList();
                ViewBag.ListProductHaveGiftSessionNull = FindListProductHaveGift().Take(8).ToList();
                
            }
            ViewBag.Startups = db.Database.SqlQuery<StartupViewModel>(
                "EXECUTE dbo.[GetAllStartup]").ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Icon cart header 
        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            //var cart = Session[Common.CommonConstants.CART_SESSION];
            //var list = new List<CartItemViewModel>();
            //if (cart != null)
            //{
            //    list = (List<CartItemViewModel>)cart;
            //}
            return PartialView();
        }
        //Ham tra ve danh sach san pham da duoc yeu thich da duoc them vao cua khach hang cos CustomerId truyen vao
        public List<ProductLiked> ListProductLikeds(Guid CustomerId)
        {
            List<ProductLiked> listProductLikeds = new List<ProductLiked>();
            listProductLikeds = db.ProductLikeds.Where(x => x.CustomerId == CustomerId).ToList();
            return listProductLikeds;
        }
        //Tim san pham co id productId da co trong san pham yeu thich hay chua /Neu co tra ve true/Khong tra ve false
        public bool FindProductInListProductLikeds(long productID, List<ProductLiked> listProductLikeds)
        {
            foreach (var productLiked in listProductLikeds)
            {
                if (productLiked.ProductId == productID)
                {
                    return true;
                }
            }
            return false;
        }
        public bool FindProductHaveGift(long productId)
        {
            List<ProductGift> productGifts = db.ProductGifts.ToList();
            foreach (var item in productGifts)
            {
                if (item.ProductId == productId)
                {
                    return true;
                }
            }
            return false;
        }
        public List<Product> FindListProductHaveGift()
        {
            List<Product> ListProductHaveGift = new List<Product>();
            List<Product> products = db.Products.Where(x=>x.Status == "Hiện").ToList();
            foreach (var item in products)
            {
                if (FindProductHaveGift(item.ProductId)==true)
                {
                    ListProductHaveGift.Add(item);
                }
            }
            return ListProductHaveGift;
        }

        //Render ListCategory
        public string RenderProductCategory(List<ProductCategoryViewModel> listChild)
        {
            string result = "";
            foreach (ProductCategoryViewModel item in listChild)
            {
                if(item.listChild.Count() <= 0)
                {
                    result += string.Format("<li><a href='/product/ProductList?MetitleCategory={1}'>{0}</a></li>", item.Name,item.MetaTitle);
                }
                else
                {
                    result += string.Format("" +
                        "<li class='category'>" +
                        "<a href='/product/ProductList?MetitleCategory={1}'>{0}<i class='fa fa-angle-right' aria-hidden='true'></i></a>" +
                        "<ul class='sub-category'>{2}</ul></li>" +
                        "", item.Name, item.MetaTitle, RenderProductCategory(item.listChild));
                }
            }
            return result;
        }
        public PartialViewResult Header()
        {
            ViewBag.productCategory = this.RenderProductCategory(productCategoryDAO.GetlistProductCategory("", null, db.ProductCategories.ToList()));
            return PartialView("~/Views/Shared/_Header.cshtml", ViewBag.productCategory);
        }

    }
}