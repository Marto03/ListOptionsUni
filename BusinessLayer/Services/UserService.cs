using Common.CustomExceptions;
using DataLayer.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class UserService : BaseService
    {
        public UserService(HotelDbContextModel context) : base(context) { }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task ManageUserAsync(UserModel userModel, bool isConfiguringExistingUser)
        {
            if (await _context.Users.AnyAsync(u => u.Id == userModel.Id) && isConfiguringExistingUser)
                await UpdateUserAsync(userModel); // Избран потребител за редакция
            else
                await RegisterUserAsync(userModel);
        }

        public async Task<UserModel?> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null || UserModel.HashPassword(password) != user.Password)
                return null; // Невалидно име или парола

            UserSessionService.Instance.SetCurrentUser(user); // Запазване на активния потребител
            return user;
        }

        public async Task<bool> DeleteUserAsync(UserModel user)
        {
            if (user == null || !_context.Users.Any(u => u.Id == user.Id))
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task RegisterUserAsync(UserModel userModel)
        {
            if (_context.Users.Any(r => r.UserName == userModel.UserName))
                throw new UserAlreadyExistsException(userModel.UserName);

            string hashedPassword = UserModel.HashPassword(userModel.Password);
            var user = new UserModel
            {
                UserName = userModel.UserName,
                Password = hashedPassword,
                Type = userModel.Type
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

        }
        private async Task UpdateUserAsync(UserModel updatedUser)
        {
            if (updatedUser == null)
                return;

            var existingUser = await _context.Users.FindAsync(updatedUser.Id);
            if (existingUser == null)
                return;

            // Актуализиране на полетата
            existingUser.UserName = updatedUser.UserName;
            existingUser.Type = updatedUser.Type;

            // Проверка дали паролата е променена
            if (!string.IsNullOrWhiteSpace(updatedUser.Password) && UserModel.HashPassword(updatedUser.Password) != existingUser.Password)
                existingUser.Password = UserModel.HashPassword(updatedUser.Password);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
            }
        }
        public async Task UpdateUserHotelAsync(UserModel userModel)
        {
            if (userModel == null)
                throw new ArgumentNullException(nameof(userModel));

            var existingUser = await _context.Users.FindAsync(userModel.Id);
            if (existingUser == null)
                throw new Exception("Потребителят не беше намерен в базата данни.");

            // Актуализиране на ID на хотела
            existingUser.HotelId = userModel.HotelId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Може да се обработи или хвърли ново изключение
                throw new Exception("Неуспешно актуализиране на потребителския хотел.", ex);
            }
        }

    }
}
