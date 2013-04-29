using System.Web;
using System.Web.Optimization;

namespace Buggy
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			#region Script Bundles

			// Common JS shared across all areas of the site. We do this for better caching.
			bundles.Add(
				new ScriptBundle("~/bundles/CommonJS").Include(
					"~/Scripts/jquery-{version}.js",
					"~/Scripts/bootstrap.js"
				)
			);

			// Authenticated User sections
			bundles.Add(
				new ScriptBundle("~/bundles/LoginJS").Include(
					"~/Scripts/Login/Login.js",
					"~/Scripts/Login/Register.js"
				)
			);

			// Authenticated User sections
			bundles.Add(
				new ScriptBundle("~/bundles/BuggyAuthenticatedJS").Include(
					"~/Scripts/Authenticated/Common.js",
					"~/Scripts/Authenticated/OverviewPage.js",
					"~/Scripts/Authenticated/AddBugModal.js",
					"~/Scripts/Authenticated/AddUpdateModal.js"
				)
			);

			// Admin User sections
			bundles.Add(
				new ScriptBundle("~/bundles/BuggyAdminJS").Include(

				)
			);

			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
						"~/Scripts/jquery-ui-{version}.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			#endregion

			#region StyleBundles

			// Common CSS shared across all areas of the site. We do this for better caching.
			bundles.Add(
				new StyleBundle("~/Content/CommonCSS").Include(
					"~/Content/bootstrap.css",
					"~/Content/bootstrap-overrides.css",
					"~/Content/bootstrap-responsive.css",
					"~/Content/Common.css"
				)
			);

			// Authenticated user
			bundles.Add(
				new StyleBundle("~/Content/LoginCSS").Include(
					"~/Content/Login/Login.css",
					"~/Content/Login/Register.css"
				)
			);

			// Authenticated user
			bundles.Add(
				new StyleBundle("~/Content/BuggyAuthenticatedCSS").Include(
					"~/Content/Authenticated/Common.css"
				)
			);

			// Admin side of buggy
			bundles.Add(
				new StyleBundle("~/Content/BuggyAdminCSS").Include(
					"~/Content/Admin/Common.css"
				)
			);

			// jQuery UI
			bundles.Add(
				new StyleBundle("~/Content/themes/base/css").Include(
					"~/Content/themes/base/jquery.ui.core.css",
					"~/Content/themes/base/jquery.ui.resizable.css",
					"~/Content/themes/base/jquery.ui.selectable.css",
					"~/Content/themes/base/jquery.ui.accordion.css",
					"~/Content/themes/base/jquery.ui.autocomplete.css",
					"~/Content/themes/base/jquery.ui.button.css",
					"~/Content/themes/base/jquery.ui.dialog.css",
					"~/Content/themes/base/jquery.ui.slider.css",
					"~/Content/themes/base/jquery.ui.tabs.css",
					"~/Content/themes/base/jquery.ui.datepicker.css",
					"~/Content/themes/base/jquery.ui.progressbar.css",
					"~/Content/themes/base/jquery.ui.theme.css"
				)
			);

			#endregion
		}
	}
}