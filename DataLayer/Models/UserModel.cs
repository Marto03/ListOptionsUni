using System.Security.Cryptography;
using System.Text;

namespace DataLayer.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; } 

        public string? Password { get; set; }

        public UserTypeEnum Type { get; set; }  // Определя ролята на потребителя

        // НОВО: Хотел към който принадлежи (само за администратор)
        public int? HotelId { get; set; }

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
