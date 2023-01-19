using System;
using System.Text;

namespace SW.Helpers
{
    internal static class StringExtensions
    {
        internal static string HashTo256(this string s)
        {
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textBytes = Encoding.UTF8.GetBytes(s + String.Empty);
                byte[] hashBytes = sha.ComputeHash(textBytes);
                return BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }
        }
    }
}