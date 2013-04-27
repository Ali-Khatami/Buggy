using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Buggy.Models;

namespace Buggy.Controllers
{
    public class APIController : Controller
    {
		SiteDB db = new SiteDB();

		public JsonResult AddBug(Bug bug)
        {
			db.Bugs.Add(bug);

			return Json(new { success = db.SaveChanges() == 1 });
        }

		public JsonResult AddUpdate(int bugID, Bug.Update update)
		{
			var bug = db.Bugs.Where(b => b.ID == bugID).FirstOrDefault();
			bool success = false;

			if (bug != null && bug.ID == bugID)
			{
				if (bug.Updates == null) { bug.Updates = new List<Bug.Update>(); };

				update.Updater = db.Users.Where(user => user.ID == bugID).FirstOrDefault();

				bug.Updates.Add(update);

				success = (db.SaveChanges() == 1);
			}

			return Json(new { success = success });
		}

		public JsonResult DeleteBug(int bugID)
		{
			var bug = db.Bugs.Where(b => b.ID == bugID).FirstOrDefault();
			bool success = false;

			if (bug != null && bug.ID == bugID)
			{
				db.Bugs.Remove(bug);
				success = (db.SaveChanges() == 1);
			}

			return Json(new { success = success });
		}

		public JsonResult RegisterUser(User user)
		{
			db.Users.Add(user);

			return Json(new { success = db.SaveChanges() == 1 });
		}

		public JsonResult DeleteUser(int userID)
		{
			var user = db.Users.Where(b => b.ID == userID).FirstOrDefault();
			bool success = false;

			if (user != null && user.ID == userID)
			{
				db.Users.Remove(user);
				success = (db.SaveChanges() == 1);
			}

			return Json(new { success = success });
		}
    }
}
