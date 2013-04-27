using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Buggy.Models;

namespace Buggy.Utilities
{
	public class UserUtils
	{
		public const string USER_ID_COOKIE_NAME = "Buggy_User_ID";

		/// <summary>
		/// Grabs the current user in context using the encrypted cookie.
		/// </summary>
		public static User CurrentUser
		{
			get
			{
				SiteDB db = new SiteDB();

				HttpCookie userCookie = HttpContext.Current.Request.Cookies.Get(USER_ID_COOKIE_NAME);
				User user = null;

				if (userCookie != null && !string.IsNullOrEmpty(userCookie.Value))
				{
					string sUserId = SimpleCrypto.DecryptData(userCookie.Value);

					int iUserID = -1;

					if (int.TryParse(sUserId, out iUserID))
					{
						user = db.Users.Where(u => u.ID == iUserID).FirstOrDefault();
					}
				}

				return user;
			}
		}

		/// <summary>
		/// Creates an encrypted user cookie that we can read on subsequent requests.
		/// </summary>
		/// <param name="sUserID">The userID in plaintext.</param>
		public static void CreateEncrypteUserCookie(string sUserID)
		{
			CreateEncrypteUserCookie(sUserID, null);
		}

		/// <summary>
		/// Creates an encrypted user cookie that we can read on subsequent requests.
		/// </summary>
		/// <param name="sUserID">The userID in plaintext.</param>
		/// <param name="expirationDays">The number of days to add for expiration. If null cookie will default to session.</param>
		public static void CreateEncrypteUserCookie(string sUserID, int? expirationDays)
		{
			var cookie = new HttpCookie(
				USER_ID_COOKIE_NAME,
				SimpleCrypto.EncryptData(sUserID)
			);

			// if expirationdays has a value we will add that amount of days to today
			if (expirationDays.HasValue)
			{
				cookie.Expires = DateTime.Today.AddDays(expirationDays.Value);
			}

			// set the cookie
			HttpContext.Current.Response.Cookies.Set(cookie);
		}
	}
}