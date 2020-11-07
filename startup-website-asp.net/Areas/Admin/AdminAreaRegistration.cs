using System.Web.Mvc;

namespace startup_website_asp.net.Areas.Admin
{
	public class AdminAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Admin";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
			   "AdminHome",
			   "admin/trang-chu",
			   new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
		   );
			context.MapRoute(
			   "AdminLogin",
			   "admin/dang-nhap",
			   new { controller = "LoginAndRegister", action = "Login", id = UrlParameter.Optional }
		   );
			context.MapRoute(
			   "AdminRegister",
			   "admin/dang-ky",
			   new { controller = "LoginAndRegister", action = "Register", id = UrlParameter.Optional }
		   );
			context.MapRoute(
				"Admin_default",
				"Admin/{controller}/{action}/{id}",
				new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}