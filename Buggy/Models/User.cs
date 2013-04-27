using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Buggy.Models
{

	/// <summary>
	/// A user of the product who has registered with the site.
	/// </summary>
	public class User
	{
		/// <summary>
		/// Unique user identifier.
		/// </summary>
		public int ID { get; set; }
		/// <summary>
		/// User's password
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		/// Person's first name.
		/// </summary>
		public string FirstName { get; set; }
		/// <summary>
		/// Person's last name.
		/// </summary>
		public string LastName { get; set; }
		/// <summary>
		/// Person's full name.
		/// </summary>
		public string Name
		{
			get
			{
				return string.Format("{0} {1}", FirstName, LastName);
			}
		}
		/// <summary>
		/// Person's email address.
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// Person's phone number.
		/// </summary>
		public string PhoneNumber { get; set; }
		/// <summary>
		/// Person's current session identifier.
		/// </summary>
		public string SessionID { get; set; }
		/// <summary>
		/// The group the user belongs to that determines their privelages.
		/// </summary>
		public Group GroupTier { get; set; }

		public enum Group
		{
			Regular,
			Admin
		}
	}
}