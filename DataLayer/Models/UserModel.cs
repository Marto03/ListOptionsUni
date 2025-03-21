using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; } 

        public string? Password { get; set; }

        // Role , или възможности .

        public UserTypeEnum Type { get; set; }  // Определя ролята на потребителя

        // Метод за хеширане на парола (SHA256)
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
