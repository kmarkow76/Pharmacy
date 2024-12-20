using System.Security.Cryptography;
using System.Text;

namespace Pharmacy.Domain.Helpers;

public class HashPasswordHelper
{
    public static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashesBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hash = BitConverter.ToString(hashesBytes).Replace("-", "").ToLower();

            return hash;
        }
    }
}