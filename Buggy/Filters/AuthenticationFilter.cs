using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Buggy.Utilities;

namespace Buggy.Filters
{
	/// <summary>
	/// Filter that checks to see if the person viewing a specific section is allowed to see this section
	/// and if not where they are suppose to go instead.
	/// </summary>
	public class AuthenticationFilter : ActionFilterAttribute
	{
		/// <summary>
		/// Specifies whether a section requires authentication or not.
		/// </summary>
		public bool SectionRequiresAuthentication = false;
		/// <summary>
		/// Specifies whether the section or action requires admin credentials.
		/// </summary>
		public bool AdminOnly = false;
		
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (SectionRequiresAuthentication && UserUtils.CurrentUser == null)
			{
				HttpContext.Current.Response.Redirect("~/Login/", true);
			}
			else if (AdminOnly && UserUtils.CurrentUser.GroupTier != Models.User.Group.Admin)
			{
				HttpContext.Current.Response.Redirect("~/", true);
			}
			else if (!SectionRequiresAuthentication && UserUtils.CurrentUser != null)
			{
				HttpContext.Current.Response.Redirect("~/Site/Index", true);
			}
		}
	}

	/// <summary>
	/// Filter that checks to see if the person hitting a specific API is allowed to, if not we will return an error result.
	/// </summary>
	public class AJAXAuthenticationFilter : ActionFilterAttribute
	{
		/// <summary>
		/// Specifies whether a section requires authentication or not.
		/// </summary>
		public bool SectionRequiresAuthentication = false;
		/// <summary>
		/// Specifies whether the section or action requires admin credentials.
		/// </summary>
		public bool AdminOnly = false;

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			bool bUserAllowed = true;

			if (SectionRequiresAuthentication && UserUtils.CurrentUser == null)
			{
				bUserAllowed = false;
			}
			else if (AdminOnly && UserUtils.CurrentUser.GroupTier != Models.User.Group.Admin)
			{
				bUserAllowed = false;
			}

			if (!bUserAllowed)
			{
				throw new Exception("Invalid Credentials");
			}
		}
	}
}