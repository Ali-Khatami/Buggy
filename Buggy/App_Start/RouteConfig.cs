﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Buggy
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "Site",
				url: "Site/{action}",
				defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "Login",
				url: "Login/{action}",
				defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "SiteDefault",
				url: "{action}",
				defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}