using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Buggy.Filters;
using Buggy.Models;
using Buggy.Utilities;
using Buggy.ViewModels.Partials;

namespace Buggy.Controllers
{
    public class APIController : Controller
    {
		SiteDB db = new SiteDB();

		[AJAXAuthenticationFilter(SectionRequiresAuthentication = true, AdminOnly = false)]
		public JsonResult AddBug(string name, string description, Bug.BugType type, Bug.PriorityLevel priority, int resolver, int tester)
        {
			db.Bugs.Add(new Bug()
			{
				CreatorID = UserUtils.CurrentUser.ID,
				Name = name,
				Description = description,
				Type = type,
				Priority = priority,
				CurrentState = Bug.State.Created,
				InsertDate = DateTime.Now,
				ResolverID = resolver,
				TesterID = tester,
				Updates = new List<Bug.Update>() { }
			});

			return Json(new { success = db.SaveChanges() == 1 });
        }

		[AJAXAuthenticationFilter(SectionRequiresAuthentication = true, AdminOnly = false)]
		public JsonResult AddUpdate(int bugID, string description)
		{
			var bug = db.Bugs.Find(bugID);
			bool success = false;
			string sMessage = "";

			if (bug != null && bug.ID == bugID && !string.IsNullOrEmpty(description))
			{
				bug.Updates = bug.Updates ?? new List<Bug.Update>();

				bug.Updates.Add(
					new Bug.Update()
					{
						InsertDate = DateTime.Now,
						UpdaterID = UserUtils.CurrentUser.ID,
						Description = description
					}
				);

				success = (db.SaveChanges() > 0);

				if (!success) { sMessage = "Unable to save updates."; }
			}
			else if (string.IsNullOrEmpty(description))
			{
				sMessage = "Description is null or empty.";
			}
			else
			{
				sMessage = string.Format("Unable to find bug {0}", bugID);
			}

			return Json(
				new
				{
					success = success,
					message = sMessage
				}
			);
		}

		[AJAXAuthenticationFilter(SectionRequiresAuthentication = true, AdminOnly = false)]
		public JsonResult EditBug(int bugID, string name, string description, Bug.BugType type, Bug.PriorityLevel priority, int resolver, int tester, Bug.State currentState)
		{
			var bug = db.Bugs.Find(bugID);
			bool success = false;

			if (bug != null && bug.ID == bugID)
			{
				bug.Name = name;
				bug.Description = description;
				bug.Type = type;
				bug.Priority = priority;
				bug.ResolverID = resolver;
				bug.TesterID = tester;
				bug.CurrentState = currentState;

				success = (db.SaveChanges() == 1);
			}

			return Json(new { });
		}

		[AJAXAuthenticationFilter(SectionRequiresAuthentication = true, AdminOnly = false)]
		public JsonResult DeleteBug(int bugID)
		{
			var bug = db.Bugs.Find(bugID);
			bool success = false;

			if (bug != null && bug.ID == bugID)
			{
				db.Bugs.Remove(bug);
				success = (db.SaveChanges() == 1);
			}

			return Json(new { success = success });
		}

		#region User APIs

		[AJAXAuthenticationFilter(SectionRequiresAuthentication = false, AdminOnly = false)]
		public JsonResult Login(string loginID, string password, bool? remember)
		{
			loginID = loginID.ToLower();

			var user = db.Users.FirstOrDefault(us => (us.LoginID.ToLower() == loginID));

			bool bSuccess = false;
			string sMessage = "";

			if (user != null && user.Password == password)
			{
				bSuccess = true;
				UserUtils.CreateEncryptedUserCookie(user.ID, remember);
			}

			return Json(
				new
				{
					success = bSuccess,
					message = sMessage
				},
				JsonRequestBehavior.DenyGet
			);
		}

		[AJAXAuthenticationFilter(SectionRequiresAuthentication = false, AdminOnly = false)]
		public JsonResult FindUser(string term)
		{
			if (string.IsNullOrEmpty(term))
			{
				return Json(new List<string>(), JsonRequestBehavior.AllowGet);
			}

			term = term.ToLower();

			return Json(
				db.Users
				.Where(user => (user.FirstName.ToLower().Contains(term) || user.LastName.ToLower().Contains(term)))
				.Take(5)
				.OrderBy(user => user.FirstName)
				.ToList(),
				JsonRequestBehavior.AllowGet
			);
		}

		[AJAXAuthenticationFilter(SectionRequiresAuthentication = false, AdminOnly = false)]
		public JsonResult RegisterUser(User user)
		{
			bool bSuccess = false;

			if (ModelState.IsValid)
			{
				db.Users.Add(user);
				if (db.SaveChanges() == 1)
				{
					User justAddedUser = db.Users.Where(u => u.LoginID == user.LoginID).FirstOrDefault();

					if (justAddedUser != null)
					{
						bSuccess = true;

						UserUtils.CreateEncryptedUserCookie(
							justAddedUser.ID,
							false
						);
					}
					else
					{
						bSuccess = false;
					}
				}
			}

			return Json(new { success =  bSuccess });
		}

		[AJAXAuthenticationFilter(SectionRequiresAuthentication = true, AdminOnly = false)]
		public JsonResult UpdateUser(User user)
		{
			var foundUser = db.Users.Find(user.ID);
			bool bSuccess = false;

			if (user != null)
			{
				db.Users.Remove(user);
				bSuccess = (db.SaveChanges() == 1);
			}

			return Json(new { success = bSuccess });
		}

		[AJAXAuthenticationFilter(SectionRequiresAuthentication = true, AdminOnly = true)]
		public JsonResult DeleteUser(int userID)
		{
			var user = db.Users.Find(userID);
			bool bSuccess = false;

			if (user != null && user.ID == userID)
			{
				db.Users.Remove(user);
				bSuccess = (db.SaveChanges() == 1);
			}

			return Json(new { success = bSuccess });
		}

		#endregion

		#region Overview page APIs

		[AJAXAuthenticationFilter(SectionRequiresAuthentication = true, AdminOnly = false)]
		public JsonResult GetOverviewTable(BugOverviewTableViewModel model)
		{			
			return Json(new { html = this.RenderPartialViewToString("~/Views/Partials/BugOverviewTable.cshtml", model.Bugs)});
		}

		#endregion
	}
}
