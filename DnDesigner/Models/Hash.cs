using System;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// This is the class that is used to hash userIds to use in verification tokens.
/// </summary>
public static class Hash
{
	public static string CalculateSHA256Hash(string input)
	{
		using (SHA256 sha256 = SHA256.Create())
		{
			// Convert the input string to a byte array and compute the hash.
			byte[] inputBytes = Encoding.UTF8.GetBytes(input);
			byte[] hashBytes = sha256.ComputeHash(inputBytes);

			// Convert the byte array to a hexadecimal string.
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < hashBytes.Length; i++)
			{
				builder.Append(hashBytes[i].ToString("x2"));
			}
			return builder.ToString();
		}
	}
}
