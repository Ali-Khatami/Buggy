using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Buggy.Models
{
	public class BugUpdate
	{
		public int ID { get; set; }
		public User Updater
		{
			get
			{
				SiteDB db = new SiteDB();

				return db.Users.Find(this.UpdaterID);
			}
		}
		public Bug Bug
		{
			get
			{
				SiteDB db = new SiteDB();

				return db.Bugs.Find(this.BugID);
			}
		}
		public int UpdaterID { get; set; }
		public int BugID { get; set; }
		public string Description { get; set; }
		public DateTime InsertDate { get; set; }
	}
}