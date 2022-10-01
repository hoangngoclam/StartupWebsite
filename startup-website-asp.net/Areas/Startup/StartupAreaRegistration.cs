using System.Web.Mvc;

namespace startup_website_asp.net.Areas.Startup
{
	public class StartupAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Startup";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
			   "StartupHome",
			   "startup/trang-chu",
			   new { controller = "StartupHome", action = "Index", id = UrlParameter.Optional }
		   );
			context.MapRoute(
				"Startup_default",
				"Startup/{controller}/{action}/{id}",
				new { controller = "StartupHome", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}