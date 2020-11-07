using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models.DAO
{
    public class StartupLikeDAO:BaseDAO
    {
        public bool Subscribe(int startupId, Guid customerId)
        {
            try
            {
                StartupLiked newStartupLike = new StartupLiked();
                newStartupLike.StartupId = startupId;
                newStartupLike.CustomerId = customerId;
                db.StartupLikeds.Add(newStartupLike);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
        public bool UnSubscribe(int startupId, Guid customerId)
        {
            try
            {
                StartupLiked startupLiked = db.StartupLikeds.Single(sl => sl.StartupId == startupId && sl.CustomerId == customerId);
                db.StartupLikeds.Remove(startupLiked);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}