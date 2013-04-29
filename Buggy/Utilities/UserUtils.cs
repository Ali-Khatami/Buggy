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
		public static string USER_ID_COOKIE_NAME = "Buggy_User";

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
					string sUserId = SimpleCrypto.Decrypt(userCookie.Value);

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
		public static void CreateEncryptedUserCookie(int sUserID)
		{
			CreateEncryptedUserCookie(sUserID, false);
		}

		/// <summary>
		/// Creates an encrypted user cookie that we can read on subsequent requests.
		/// </summary>
		/// <param name="sUserID">The userID in plaintext.</param>
		/// <param name="remember">
		/// Whether or not to remember the user,ie keep the cookie around for at least a little while.
		/// If null cookie will default to session.
		/// </param>
		public static void CreateEncryptedUserCookie(int sUserID, bool? remember)
		{
			var cookie = new HttpCookie(
				USER_ID_COOKIE_NAME,
				SimpleCrypto.Encrypt(sUserID.ToString())
			);

			// if expirationdays has a value we will add that amount of days to today
			if (remember.GetValueOrDefault(false))
			{
				cookie.Expires = DateTime.Today.AddDays(30);
			}

			// set the cookie
			HttpContext.Current.Response.Cookies.Set(cookie);
		}

		/// <summary>
		/// Removes the current users encrypted user cookie.
		/// </summary>
		public static void DestroyEncryptedUserCookie()
		{
			HttpCookie userCookie = HttpContext.Current.Request.Cookies.Get(USER_ID_COOKIE_NAME);
			if (userCookie != null)
			{
				userCookie.Expires = DateTime.Today.AddDays(-1);
				HttpContext.Current.Response.Cookies.Set(userCookie);
			}
		}
	}
}