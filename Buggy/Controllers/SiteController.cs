using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Buggy.Filters;
using Buggy.Models;
using Buggy.Models.Pages;
using Buggy.Utilities;
using Buggy.ViewModels.Partials;

namespace Buggy.Controllers
{
	[AuthenticationFilter(AdminOnly = false, SectionRequiresAuthentication = true)]
    public class SiteController : Controller
    {
		SiteDB db = new SiteDB();

		public ActionResult Index()
        {
			OverviewPageModel model = new OverviewPageModel()
			{
				BugsCreatedByMe = new BugOverviewTableViewModel()
				{
					FilterField = Bug.FilterableFields.Creator,
					FilterValue = UserUtils.CurrentUser.ID.ToString(),
					SortDirection = Interfaces.SortDirection.Descending
				},
				BugsToBeResolvedByMe = new BugOverviewTableViewModel()
				{
					FilterField = Bug.FilterableFields.Resolver,
					FilterValue = UserUtils.CurrentUser.ID.ToString(),
					SortDirection = Interfaces.SortDirection.Descending
				},
				BugsToBeTestedByMe = new BugOverviewTableViewModel()
				{
					FilterField = Bug.FilterableFields.Tester,
					FilterValue = UserUtils.CurrentUser.ID.ToString(),
					SortDirection = Interfaces.SortDirection.Descending
				}
			};

			return View("~/Views/Pages/Index.cshtml", model);
        }

		public ActionResult Detail(int bugID)
		{
			var bug = db.Bugs.Where(b => b.ID == bugID).FirstOrDefault();

			return View("~/Views/Pages/Bug/Detail.cshtml", bug);
		}
    }
}