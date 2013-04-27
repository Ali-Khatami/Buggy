using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Buggy.Models
{
	public class SiteDB : DbContext
	{
		public SiteDB() : base("DefaultConnection") { }

		public DbSet<Bug> Bugs { get; set; }
		public DbSet<User> Users { get; set; }
	}
}