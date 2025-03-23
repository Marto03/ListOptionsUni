//using DataLayer.Models;
//using Microsoft.EntityFrameworkCore;

//namespace BusinessLayer.Services
//{
//    public class UserService : BaseService
//    {
//        public UserService(HotelDbContextModel context) : base(context) { }

//        public List<UserModel> GetAllUsers()
//        {
//            return _context.Users.ToList();  // Извлича всички удобства от базата
//        }
//        public async Task<bool> RegisterUserAsync(UserModel userModel)
//        {
//            if (await _context.Users.AnyAsync(u => u.UserName == userModel.UserName))
//                return false; // Вече съществува

//            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userModel.Password);

//            var user = new UserModel
//            {
//                UserName = userModel.UserName,
//                Password = hashedPassword,
//                Type = userModel.Type
//            };

//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();
//            return true;
//        }
//        public async Task<bool> DeleteUserAsync(UserModel user)
//        {
//            var existingUser = await _context.Users.FindAsync(user.Id);
//            if (existingUser == null)
//                return false; // Не е намерен

//            _context.Users.Remove(existingUser);
//            await _context.SaveChangesAsync();
//            return true;
//        }
//        public async Task<UserModel?> AuthenticateAsync(string username, string password)
//        {
//            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
//            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
//                return null; // Невалидно име или парола

//            UserSessionService.Instance.SetCurrentUser(user); // ✅ Запазваме логнатия потребител
//            return user;
//        }


//        public async Task<bool> CanUserAddAsync(string username)
//        {
//            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
//            return user?.Type == UserTypeEnum.Admin; // Само админите могат да добавят
//        }
//    }
//}

using DataLayer.Models;
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

        public async Task<bool> RegisterUserAsync(UserModel userModel)
        {
            if (await _context.Users.AnyAsync(u => u.Id == userModel.Id))
                return await UpdateUserAsync(userModel); // Вече съществува

            string hashedPassword = UserModel.HashPassword(userModel.Password);

            var user = new UserModel
            {
                UserName = userModel.UserName,
                Password = hashedPassword,
                Type = userModel.Type
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateUserAsync(UserModel updatedUser)
        {
            if (updatedUser == null)
                return false;

            var existingUser = await _context.Users.FindAsync(updatedUser.Id);
            if (existingUser == null)
                return false;

            // Актуализиране на полетата
            existingUser.UserName = updatedUser.UserName;
            existingUser.Type = updatedUser.Type;

            // Проверка дали паролата е променена
            if (!string.IsNullOrWhiteSpace(updatedUser.Password) && UserModel.HashPassword(updatedUser.Password) != existingUser.Password)
            {
                existingUser.Password = UserModel.HashPassword(updatedUser.Password);
            }

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<UserModel?> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null || UserModel.HashPassword(password) != user.Password)
                return null; // Невалидно име или парола

            UserSessionService.Instance.SetCurrentUser(user); // ✅ Запазване на активния потребител
            return user;
        }

        public async Task<bool> DeleteUserAsync(UserModel user)
        {
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool CanUserAdd()
        {
            return UserSessionService.Instance.IsAdmin; // ✅ Само администратор може да добавя потребители
        }
    }
}
