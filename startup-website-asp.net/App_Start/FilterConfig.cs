using System.Web;
using System.Web.Mvc;

namespace startup_website_asp.net
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
			filters.Add(new HandleErrorAttribute());
		}
    }
}
