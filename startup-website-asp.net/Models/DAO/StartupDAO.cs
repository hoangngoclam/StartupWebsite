using System.Collections.Generic;
using System.Linq;
using startup_website_asp.net.Models.EF;
using System.Data.Entity;
using System;
using startup_website_asp.net.ViewModels;
using System.Data.SqlClient;

namespace startup_website_asp.net.Models.DAO
{
	public class StartupDAO:BaseDAO
	{
		public StartupDAO()
		{
			db = new StartupWebsite();
		}
		//Danh sách danh mục
		public IEnumerable<startup_website_asp.net.Models.EF.Startup> ListAll()
		{
			return db.Startups.ToList();
		}
		public StartupViewModel GetDetailWithCustomerId(int id,Guid? customerId)
		{
			List<StartupViewModel> result;
			if (customerId != null)
			{
				result = db.Database.SqlQuery<StartupViewModel>(
			   "EXECUTE dbo.[StartupDetailWithCustomer] @StartupId, @CustomerId",
			   new SqlParameter("StartupId", id),
			   new SqlParameter("CustomerId", customerId)).ToList();
			}
			else
			{
				result = db.Database.SqlQuery<StartupViewModel>(
			   "EXECUTE dbo.[StartupDetailWithCustomer] @StartupId, @CustomerId",
			   new SqlParameter("StartupId", id),
			   new SqlParameter("CustomerId", DBNull.Value)).ToList();
			}
			
			return result[0];
		}
		public List<StartupViewModel> GetListWithCustomerId( Guid? customerId)
		{
			List<StartupViewModel> result;
			if (customerId != null)
			{
				result = db.Database.SqlQuery<StartupViewModel>(
			   "EXECUTE dbo.[GetListStartupWithCustomerId] @CustomerId",
			   new SqlParameter("CustomerId", customerId)).ToList();
			}
			else
			{
				result = db.Database.SqlQuery<StartupViewModel>(
			   "EXECUTE dbo.[GetListStartupWithCustomerId] @CustomerId",
			   new SqlParameter("CustomerId", DBNull.Value)).ToList();
			}

			return result;
		}

		public long RegisterStartup(startup_website_asp.net.Models.EF.Startup startupI)
		{
			try
			{
				startup_website_asp.net.Models.EF.Startup newStartup = new startup_website_asp.net.Models.EF.Startup();
				newStartup.LogoUrl = startupI.LogoUrl;
				newStartup.Name = startupI.Name;
				newStartup.Address = startupI.Address;
				newStartup.PhoneNumber = startupI.PhoneNumber;
				newStartup.StartupTypeId = startupI.StartupTypeId;
				newStartup.Description = startupI.Description;
				db.Startups.Add(newStartup);
				db.SaveChanges();
				return newStartup.StartupId;
			}
			catch (Exception)
			{
				return 0;
			}
		}
		public startup_website_asp.net.Models.EF.Startup GetDetail(int id)
		{
			return db.Startups.Find(id);
		}
	}
}