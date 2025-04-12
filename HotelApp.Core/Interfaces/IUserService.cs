using HotelApp.Core.Models;

namespace HotelApp.Core.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsersAsync();
        Task ManageUserAsync(UserModel userModel, bool isConfiguringExistingUser);

        Task<UserModel?> AuthenticateAsync(string username, string password);

        Task<bool> DeleteUserAsync(UserModel user);


        Task RegisterUserAsync(UserModel userModel);

        Task UpdateUserAsync(UserModel updatedUser);

        Task UpdateUserHotelAsync(UserModel userModel);
        
    }
}
