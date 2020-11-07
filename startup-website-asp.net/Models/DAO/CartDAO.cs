using startup_website_asp.net.Models.EF;
using startup_website_asp.net.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models.DAO
{
    public class CartDAO : BaseDAO
    {
        public CartDAO()
        {

        }
        public bool DeleteCartByCustomerId(Guid customerId)
        {
            try
            {
                List<Cart> listCartDelete = new List<Cart>();
                listCartDelete = db.Carts.Where(x => x.CustomerId == customerId).ToList();
                db.Carts.RemoveRange(listCartDelete);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteCartByCustomerId(Guid customerId, long startupId)
        {
            try
            {
                List<Cart> listCartDelete = new List<Cart>();
                listCartDelete = db.Carts.Include(x=>x.Product).Where(x => x.CustomerId == customerId && x.Product.StartupId == startupId).ToList();
                db.Carts.RemoveRange(listCartDelete);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCartByProductId(long productId, Guid customerId)
        {
            try
            {
                List<Cart> listCartDelete = new List<Cart>();
                listCartDelete = db.Carts.Where(x => x.CustomerId == customerId && x.ProductId == productId).ToList();
                db.Carts.RemoveRange(listCartDelete);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateCartByProductId(long productId, Guid customerId)
        {
            return true;
        }

        public List<Cart> getCartByCustomerId(Guid customerId)
        {
            List<Cart> carts = db.Carts.Include(x => x.Product).Where(x => x.CustomerId == customerId).ToList();

            return carts;
        }

        public List<Cart> getCartByStartupId(long startupId, Guid customerId)
        {
            List<Cart> carts = db.Carts.Include(x => x.Product).Where(x => x.CustomerId == customerId && x.Product.StartupId == startupId).ToList();
            return carts;
        }

    }
}