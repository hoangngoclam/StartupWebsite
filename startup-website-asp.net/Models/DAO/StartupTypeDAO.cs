using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup_website_asp.net.Models.DAO
{
    public class StartupTypeDAO:BaseDAO
    {
		public bool Delete(int id)
		{
			try
			{
				var startupType = db.StartupTypes.Find(id);
				db.StartupTypes.Remove(startupType);
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