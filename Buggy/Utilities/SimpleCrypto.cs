using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Buggy.Utilities
{
	public class SimpleCrypto
	{
		/// <summary>
		/// The encryption key.
		/// </summary>
		private static byte[] AESKey = ASCIIEncoding.UTF8.GetBytes("AESKEY");
		/// <summary>
		/// The encryption vector.
		/// </summary>
		private static byte[] AESVector = ASCIIEncoding.UTF8.GetBytes("AESVECTOR");

		/// <summary>
		/// Used for serializing and encrypting data.
		/// </summary>
		/// <param name="data">Any data type you like. Will be serialized as JSON so be sure to allow for this.</param>
		/// <returns>Returns an encrypted string using AES.</returns>
		public static string EncryptData(object data)
		{
			JavaScriptSerializer ser = new JavaScriptSerializer();

			return EncryptData(ser.Serialize(data));
		}

		/// <summary>
		/// Used for serializing and encrypting plain text
		/// </summary>
		/// <param name="data">A string of information in plain text</param>
		/// <returns>Returns an encrypted string using AES.</returns>
		public static string EncryptData(string data)
		{
			RijndaelManaged aesAlg = new RijndaelManaged();
			aesAlg.Key = AESKey;
			aesAlg.IV = AESVector;
			ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

			MemoryStream msEncrypt = new MemoryStream();

			using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
			{
				using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
				{
					swEncrypt.Write(data);
				}
			}

			byte[] encrypedBytes = msEncrypt.ToArray();

			return ASCIIEncoding.UTF8.GetString(encrypedBytes);
		}

		/// <summary>
		/// Used for decrypting information back into plain text.
		/// </summary>
		/// <param name="encryptedData">A string of encrypted data.</param>
		/// <returns>Returns decrypted information in plain text.</returns>
		public static string DecryptData(string encryptedData)
		{
			string sDecryptedData;

			RijndaelManaged aesAlg = new RijndaelManaged();
			aesAlg.Key = AESKey;
			aesAlg.IV = AESVector;

			// Create a decrytor to perform the stream transform.
			ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

			// Create the streams used for decryption. 
			using (MemoryStream msDecrypt = new MemoryStream(ASCIIEncoding.UTF8.GetBytes(encryptedData)))
			{
				using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
				{
					using (StreamReader srDecrypt = new StreamReader(csDecrypt))
					{

						// Read the decrypted bytes from the decrypting stream 
						// and place them in a string.
						sDecryptedData = srDecrypt.ReadToEnd();
					}
				}
			}

			return sDecryptedData;
		}
	}
}