using startup_website_asp.net.Models.EF;
using startup_website_asp.net.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models.DAO
{    
    public class BaseDAO
    {
        public StartupWebsite db = null;
        public BaseDAO()
        {
            db = new StartupWebsite();
        }
        public ProductViewModel GetProductById(long productId, Guid? CustomerId = null)
        {
            List<ProductViewModel> listProductModel = new List<ProductViewModel>();
            ProductViewModel productModel = new ProductViewModel();
            if (CustomerId == null)
            {
                listProductModel = db.Database.SqlQuery<ProductViewModel>(
                        "EXECUTE dbo.[GetProductDetail] @ProductId, @CustomerId",
                        new SqlParameter("ProductId", productId),
                        new SqlParameter("CustomerId", DBNull.Value)).ToList();
            }
            else
            {
                listProductModel = db.Database.SqlQuery<ProductViewModel>(
                       "EXECUTE dbo.[GetProductDetail] @ProductId, @CustomerId",
                       new SqlParameter("ProductId", productId),
                       new SqlParameter("CustomerId", CustomerId)).ToList();
            }
            productModel = listProductModel[0];
            return productModel;
        }
    }
}