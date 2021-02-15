using System.Security.Cryptography;
using System.Text;

namespace Battleships.Data.Services
{
    public static class PasswordHashingService
    {
        public static string Hash(string pass)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(pass));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString());
                }

                return builder.ToString();
            }
        }
    }
}
