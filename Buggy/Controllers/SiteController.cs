using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Buggy.Models;

namespace Buggy.Controllers
{
    public class SiteController : Controller
    {
		SiteDB db = new SiteDB();

		public ActionResult Index()
        {
			db.Bugs.Add(
				new Bug()
				{
					InsertDate = DateTime.Now,
					Name = string.Format("Bug {0}", System.Guid.NewGuid().ToString()),
					Description = "A sweet bug in the system.",
					Type = Bug.BugType.Bug,
					CurrentState = Bug.State.Created,
					Priority = Bug.PriorityLevel.Meh
				}
			);
			db.SaveChanges();
			List<Bug> bugs = (db.Bugs.Count() > 0) ? db.Bugs.Take(10).ToList() : new List<Bug>();
			return View("~/Views/Pages/Index.cshtml", new { });
        }

		public ActionResult Detail(int bugID)
		{
			var bug = db.Bugs.Where(b => b.ID == bugID).FirstOrDefault();

			return View();
		}
    }
}
