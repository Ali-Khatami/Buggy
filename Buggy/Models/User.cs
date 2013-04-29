using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
		/// This is what a user logs into the site with.
		/// </summary>
		[DisplayName("Username")]
		[Required(AllowEmptyStrings=false, ErrorMessage="Username cannot be null or empty.")]
		public string LoginID { get; set; }
		/// <summary>
		/// User's password
		/// </summary>
		[DisplayName("Password")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be null or empty.")]
		public string Password { get; set; }
		/// <summary>
		/// Person's first name.
		/// </summary>
		[DisplayName("First Name")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "First Name cannot be null or empty.")]
		public string FirstName { get; set; }
		/// <summary>
		/// Person's last name.
		/// </summary>
		[DisplayName("Last Name")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Last Name cannot be null or empty.")]
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
		[DisplayName("Email")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Email cannot be null or empty.")]
		[EmailAddress(ErrorMessage = "Invalid Email address.")]
		public string Email { get; set; }
		/// <summary>
		/// Person's phone number.
		/// </summary>
		[DisplayName("Phone")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Phone cannot be null or empty.")]
		[Phone(ErrorMessage = "Invalid Phone number.")]
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