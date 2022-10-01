using startup_website_asp.net.Models;
using startup_website_asp.net.Models.DAO;
using startup_website_asp.net.Models.EF;
using startup_website_asp.net.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace startup_website_asp.net.Controllers
{
    public class ProductController : Controller
    {
        private ProductCategoryDAO productCategoryDAO = new ProductCategoryDAO();
        private ProductDAO productDAO = new ProductDAO();
        private RateDAO rateDAO = new RateDAO();
        private StartupWebsite db = new StartupWebsite();
        // GET:
        public ActionResult ProductDetail(long id)
        {
            
            ProductViewModel productModel = new ProductViewModel();
            productModel = productDAO.GetProductDetail(id);
            List<ProductCategoryViewModel> listProductCategoryModel = productCategoryDAO.GetListProductCategoryRender();
            productModel.ProductCategoryRender = listProductCategoryModel.Find(x => x.ProductCategoryId == productModel.ProductCategoryId).RoundName;
            productModel.ListSubImage = productModel.SubImages.Split(',').ToList();
            ViewBag.ListRate = db.Rates.Where(x => x.ProductId == id).ToList();
            ViewBag.RateNumber = rateDAO.GetAverageRate(id);
            ViewBag.Product = db.Products.Find(id);
            ViewBag.Attributes = db.AttributeRelationships.Where(x => x.ProductId == id ).ToList();
            ViewBag.Attribute1 = db.Attributes.Find(productModel.AttributeId1);
            ViewBag.RelatedProducts = db.Products.Where(x => x.ProductCategoryId == productModel.ProductCategoryId && x.ProductId!=productModel.ProductId).ToList();
            return View(productModel);
        }
       
        public ActionResult ProductList(string MetitleCategory = "", string search = "")
        {
            List<ProductViewModel> listProductModel = new List<ProductViewModel>();
            ViewBag.ProductList_Category = RenderProductCategory(productCategoryDAO.GetlistProductCategory("",null,db.ProductCategories.ToList()));
            listProductModel = db.Database.SqlQuery<ProductViewModel>(
                "EXECUTE dbo.[GetProductByCustomerAndCategory] @MetaTitleCategory, @CustomerId", 
                new SqlParameter("MetaTitleCategory", MetitleCategory),
                new SqlParameter("CustomerId", new Guid())).ToList();
            return View(listProductModel);
        }
        [HttpGet]
        public ActionResult SearchProduct(string search = "")
        {
            List<ProductViewModel> listProductModel = new List<ProductViewModel>();
            ViewBag.ProductList_Category = RenderProductCategory(productCategoryDAO.GetlistProductCategory("", null, db.ProductCategories.ToList()));
            listProductModel = db.Database.SqlQuery<ProductViewModel>(
                "EXECUTE dbo.[GetSearchProduct] @Search, @CustomerId",
                new SqlParameter("Search", search.Trim()),
                new SqlParameter("CustomerId", new Guid())).ToList();
            return View("ProductList",listProductModel);
        }
        public string RenderProductCategory(List<ProductCategoryViewModel> listChild)
        {
            string result = "";
            foreach (ProductCategoryViewModel item in listChild)
            {
                if (item.listChild.Count() <= 0)
                {
                    result += string.Format("<li><a href='/product/ProductList?MetitleCategory={1}' >{0}</a></li>", item.Name, item.MetaTitle);
                }
                else
                {
                    result += string.Format("" +
                        "<li>" +
                        "<a href='javascript: void(0)' >{0}<i class='fa fa-angle-right'></i></a>" +
                        "<ul class='sub-category-2'>{2}</ul></li>" +
                        "", item.Name, item.MetaTitle, RenderProductCategory(item.listChild));
                }
            }
            return result;
        }
    }
}