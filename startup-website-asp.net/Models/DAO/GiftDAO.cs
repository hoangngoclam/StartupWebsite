using startup_website_asp.net.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models.DAO.Startup
{
    public class GiftDAO:BaseDAO
    {
        public GiftDAO()
        {
            db = new StartupWebsite();
        }
		public bool Delete(int id)
		{
			try
			{
				var gift = db.Gifts.Find(id);
				db.Gifts.Remove(gift);
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