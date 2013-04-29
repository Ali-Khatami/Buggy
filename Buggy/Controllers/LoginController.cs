using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Buggy.Filters;
using Buggy.Utilities;

namespace Buggy.Controllers
{
	[AuthenticationFilter(AdminOnly = false, SectionRequiresAuthentication = false)]
    public class LoginController : Controller
    {
		public ActionResult Index()
		{
			return View("~/Views/Pages/User/Login.cshtml", new { });
		}

		public ActionResult Register()
		{
			return View("~/Views/Pages/User/Register.cshtml", new { });
		}
    }
}
