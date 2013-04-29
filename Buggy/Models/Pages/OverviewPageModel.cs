using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Buggy.ViewModels.Partials;

namespace Buggy.Models.Pages
{
	public class OverviewPageModel
	{
		public BugOverviewTableViewModel BugsCreatedByMe { get; set; }
		public BugOverviewTableViewModel BugsToBeResolvedByMe { get; set; }
		public BugOverviewTableViewModel BugsToBeTestedByMe { get; set; }
	}
}