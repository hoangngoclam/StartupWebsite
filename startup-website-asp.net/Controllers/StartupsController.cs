using PagedList;
using startup_website_asp.net.Common;
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
	public enum StartupFilterType
	{
		NumberOfSubscribe,
		NumberOfProduct,
	}
    public class StartupsController : Controller
    {
		StartupDAO startupDAO = new StartupDAO();
		ProductDAO ProductDAO = new ProductDAO();
		private int pageSize = 8;
		StartupCategoryDAO StartupCategoryDAO = new StartupCategoryDAO();
		// GET: Startups

		public ActionResult StartupList(string searchString, int? page, StartupFilterType startupFilterBy = StartupFilterType.NumberOfSubscribe, int startupTypeFilter = 0)
        {
			int pageNumber = (page ?? 1);
			List<StartupViewModel> startupviewModels = new List<StartupViewModel>();
			ViewBag.StartupFilterType = startupFilterBy;
			ViewBag.StartupTypeFilter = startupTypeFilter;
			ViewBag.StartupTypes = DbContextSt.Db.StartupTypes.ToList();
			ViewBag.SearchString = searchString;
			try
			{
				CustomerLogin customerLogin = (CustomerLogin)Session[CommonSession.CUSTOMER_SESSION];
				if (customerLogin != null)
				{
					startupviewModels = startupDAO.GetListWithCustomerId(customerLogin.UserID);
					if (!String.IsNullOrEmpty(searchString))
					{
						startupviewModels = startupviewModels.Where(s => s.Name.ToLower().Contains(searchString.ToLower())
											   || s.Description.ToLower().Contains(searchString.ToLower())).ToList();
					}
					OrderStartupList(ref startupviewModels, startupFilterBy);
					FilterbyStartupType(ref startupviewModels, startupTypeFilter);
					return View(startupviewModels.ToPagedList(pageNumber, pageSize));
				}
				else
				{
					startupviewModels = startupDAO.GetListWithCustomerId(null);
					if (!String.IsNullOrEmpty(searchString))
					{
						startupviewModels = startupviewModels.Where(s => s.Name.ToLower().Contains(searchString.ToLower())
											   || s.Description.ToLower().Contains(searchString.ToLower())).ToList();
					}
					OrderStartupList(ref startupviewModels, startupFilterBy);
					FilterbyStartupType(ref startupviewModels, startupTypeFilter);
					return View(startupviewModels.ToPagedList(pageNumber, pageSize));
				}
			}
			catch (Exception)
			{
				throw;
			}
        }
        public ActionResult StartupDetail(int id)
        {
			StartupViewModel startupviewModel = new StartupViewModel();
			try
			{
				CustomerLogin customerLogin = (CustomerLogin)Session[CommonSession.CUSTOMER_SESSION];
				if(customerLogin != null)
				{
					startupviewModel = startupDAO.GetDetailWithCustomerId(id, customerLogin.UserID);
				}
				else
				{
					startupviewModel = startupDAO.GetDetailWithCustomerId(id, null);
				}
			}
			catch (Exception)
			{
				throw;
			}
            return View(startupviewModel);
        }
		[HttpGet]
        public ActionResult StartupDetailProducts(int id,int? categoryId = null)
        {
			try
			{
				ViewBag.startup = startupDAO.GetDetail(id);
				ViewBag.categories= StartupCategoryDAO.GetCategoriesByStartupId(id);
				ViewBag.productList = ProductDAO.GetProductsByStartupCategory(id, categoryId);
			}
			catch (Exception)
			{
				throw;
			}
			return View();
		}
        public ActionResult StartupDetailBlogs(int id)
        {
			try
			{
				ViewBag.startup = startupDAO.GetDetail(id);
			}
			catch (Exception)
			{
				throw;
			}
			return View();
		}
		private void OrderStartupList(ref List<StartupViewModel> startups, StartupFilterType type)
		{
			switch (type)
			{
				case StartupFilterType.NumberOfSubscribe:
					startups.OrderByDescending(x => x.LikeNumber);
					break;
				case StartupFilterType.NumberOfProduct:
					startups.OrderByDescending(x => x.ProductNumber);
					break;
				default:
					break;
			}
		}
		private void FilterbyStartupType(ref List<StartupViewModel> startups, int startupType)
		{
			if(startupType == 0)
			{
				return;
			}
			startups = startups.Where(x => x.StartupTypeId == startupType).ToList();
		}
    }
}