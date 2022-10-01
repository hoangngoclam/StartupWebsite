using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace startup_website_asp.net
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute(
			  name: "EditProfileAccount",
			  url: "tai-khoan/{id}",
			  defaults: new { controller = "Account", action = "EditProfileAccount", id = UrlParameter.Optional },
			  namespaces: new[] { "startup-website-asp.net.Controllers" }
			  );
			routes.MapRoute(
			  name: "ProfileAccountFavoriteProducts",
			  url: "san-pham-thich/{id}",
			  defaults: new { controller = "Account", action = "ProfileAccountFavoriteProducts", id = UrlParameter.Optional },
			  namespaces: new[] { "startup-website-asp.net.Controllers" }
			  );
			routes.MapRoute(
			  name: "ProfileAccountOrders",
			  url: "don-hang/{id}",
			  defaults: new { controller = "Account", action = "ProfileAccountOrders", id = UrlParameter.Optional },
			  namespaces: new[] { "startup-website-asp.net.Controllers" }
			  );
			routes.MapRoute(
			  name: "BlogList",
			  url: "danh-sach-blog",
			  defaults: new { controller = "Blogs", action = "BlogList", id = UrlParameter.Optional },
			  namespaces: new[] { "startup-website-asp.net.Controllers" }
			  );
			routes.MapRoute(
			  name: "BlogDetail",
			  url: "chi-tiet-blog/{id}",
			  defaults: new { controller = "Blogs", action = "BlogDetail", id = UrlParameter.Optional },
			  namespaces: new[] { "startup-website-asp.net.Controllers" }
			  );
			routes.MapRoute(
			  name: "Cart",
			  url: "gio-hang/{id}",
			  defaults: new { controller = "Cart", action = "ProductCart", id = UrlParameter.Optional },
			  namespaces: new[] { "startup-website-asp.net.Controllers" }
			  );
			routes.MapRoute(
			  name: "StartupList",
			  url: "startup-danh-sach",
			  defaults: new { controller = "Startups", action = "StartupList", id = UrlParameter.Optional },
			  namespaces: new[] { "startup-website-asp.net.Controllers" }
			  );
			routes.MapRoute(
			  name: "Startup-detail",
			  url: "startup-chi-tiet/{id}",
			  defaults: new { controller = "Startups", action = "StartupDetail", id = UrlParameter.Optional },
			  namespaces: new[] { "startup-website-asp.net.Controllers" }
			  );
			routes.MapRoute(
			  name: "StartupDetailProducts",
			  url: "startup-chi-tiet-san-pham/{id}",
			  defaults: new { controller = "Startups", action = "StartupDetailProducts", id = UrlParameter.Optional },
			  namespaces: new[] { "startup-website-asp.net.Controllers" }
			  );
			routes.MapRoute(
			  name: "StartupDetailBlogs",
			  url: "startup-chi-tiet-blog/{id}",
			  defaults: new { controller = "Startups", action = "StartupDetailBlogs", id = UrlParameter.Optional },
			  namespaces: new[] { "startup-website-asp.net.Controllers" }
			  );
			routes.MapRoute(
			  name: "ProductDetail",
			  url: "chi-tiet-san-pham/{id}",
			  defaults: new { controller = "Product", action = "ProductDetail", id = UrlParameter.Optional },
			  namespaces: new[] { "startup-website-asp.net.Controllers" }
			  );
			routes.MapRoute(
			  name: "Product",
			  url: "cua-hang",
			  defaults: new { controller = "Product", action = "ProductList", id = UrlParameter.Optional },
			  namespaces: new[] { "startup-website-asp.net.Controllers" }
			  );
			routes.MapRoute(
			  name: "Index",
			  url: "trang-chu",
			  defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
			  namespaces: new[] { "startup-website-asp.net.Controllers" }
			  );
			routes.MapRoute(
			  name: "Login",
			  url: "dang-nhap",
			  defaults: new { controller = "CustomerLoginAndRegister", action = "Login", id = UrlParameter.Optional },
			  namespaces: new[] { "startup-website-asp.net.Controllers" }
			  );
            routes.MapRoute(
              name: "Register",
              url: "dang-ky",
              defaults: new { controller = "CustomerLoginAndRegister", action = "Register", id = UrlParameter.Optional },
              namespaces: new[] { "startup-website-asp.net.Controllers" }
              );
			routes.MapRoute(
			  name: "About",
			  url: "gioi-thieu",
			  defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional },
			  namespaces: new[] { "startup-website-asp.net.Controllers" }
			  );
			routes.MapRoute(
				name: "Payment",
				url: "mua-hang/{id}",
				defaults: new { controller = "Order", action = "Payment", id = UrlParameter.Optional },
				namespaces: new[] { "startup-website-asp.net.Controllers" }
			);
			routes.MapRoute(
				name: "TrialSubscription",
				url: "dung-thu/{id}",
				defaults: new { controller = "Order", action = "TrialSubscription", id = UrlParameter.Optional },
				namespaces: new[] { "startup-website-asp.net.Controllers" }
			);
			routes.MapRoute(
			  name: "Contact",
			  url: "lien-he",
			  defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional },
			  namespaces: new[] { "startup-website-asp.net.Controllers" }
			  );
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				namespaces: new[] { "startup-website-asp.net.Controllers" }
			);
		}
	}
}
