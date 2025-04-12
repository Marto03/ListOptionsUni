using HotelApp.Common.CustomExceptions;
using HotelApp.Core.Interfaces;
using HotelApp.Core.Models;
using HotelApp.Core.Repositories;

namespace HotelApp.BusinessLayer.Services
{
    internal class UserService : IUserService
    {
        private readonly IGenericRepository<UserModel> _userRepository;
        private readonly IUserSessionService userSessionService;

        public UserService(IGenericRepository<UserModel> userRepository, IUserSessionService userSessionService)
        {
            _userRepository = userRepository;
            this.userSessionService = userSessionService;
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task ManageUserAsync(UserModel userModel, bool isConfiguringExistingUser)
        {
            if (await _userRepository.ExistsAsync(userModel.Id) && isConfiguringExistingUser)
                await UpdateUserAsync(userModel);
            else
                await RegisterUserAsync(userModel);
        }

        public async Task<UserModel?> AuthenticateAsync(string username, string password)
        {
            var user = (await _userRepository.FindAsync(u => u.UserName == username)).FirstOrDefault();
            if (user == null || UserModel.HashPassword(password) != user.Password)
                return null;

            userSessionService.SetCurrentUser(user);
            return user;
        }

        public async Task<bool> DeleteUserAsync(UserModel user)
        {
            if (user == null || !await _userRepository.ExistsAsync(user.Id))
                return false;

            await _userRepository.DeleteAsync(user);
            return true;
        }

        public async Task RegisterUserAsync(UserModel userModel)
        {
            if ((await _userRepository.FindAsync(r => r.UserName == userModel.UserName)).Any())
                throw new UserAlreadyExistsException(userModel.UserName);

            string hashedPassword = UserModel.HashPassword(userModel.Password);
            var user = new UserModel
            {
                UserName = userModel.UserName,
                Password = hashedPassword,
                Type = userModel.Type
            };

            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(UserModel updatedUser)
        {
            if (updatedUser == null) return;

            var existingUser = await _userRepository.GetByIdAsync(updatedUser.Id);
            if (existingUser == null) return;

            existingUser.UserName = updatedUser.UserName;
            existingUser.Type = updatedUser.Type;

            if (!string.IsNullOrWhiteSpace(updatedUser.Password) &&
                UserModel.HashPassword(updatedUser.Password) != existingUser.Password)
            {
                existingUser.Password = UserModel.HashPassword(updatedUser.Password);
            }

            await _userRepository.UpdateAsync(existingUser);
        }

        public async Task UpdateUserHotelAsync(UserModel userModel)
        {
            if (userModel == null)
                throw new ArgumentNullException(nameof(userModel));

            var existingUser = await _userRepository.GetByIdAsync(userModel.Id);
            if (existingUser == null)
                throw new Exception("Потребителят не беше намерен в базата данни.");

            existingUser.HotelId = userModel.HotelId;

            await _userRepository.UpdateAsync(existingUser);
        }
    }

}
