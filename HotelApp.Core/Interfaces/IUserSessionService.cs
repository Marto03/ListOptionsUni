using HotelApp.Core.Models;

namespace HotelApp.Core.Interfaces
{
    public interface IUserSessionService
    {
        bool IsLoggedIn { get; }
        bool IsAdmin { get; }
        UserModel? CurrentUser { get; }
        event Action<UserModel?> CurrentUserChanged;
        void SetCurrentUser(UserModel user);
    }
}
