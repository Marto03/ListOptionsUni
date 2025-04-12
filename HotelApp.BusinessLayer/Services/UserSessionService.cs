using HotelApp.Core.Interfaces;
using HotelApp.Core.Models;

namespace HotelApp.BusinessLayer.Services
{
    internal class UserSessionService : IUserSessionService
    {
        public UserSessionService() { }

        public bool IsLoggedIn => CurrentUser != null;
        public bool IsAdmin => CurrentUser?.Type == UserTypeEnum.Admin;
        public UserModel? CurrentUser { get; private set; }

        public event Action<UserModel?> CurrentUserChanged;

        public void SetCurrentUser(UserModel user)
        {
            CurrentUser = user;
            CurrentUserChanged?.Invoke(CurrentUser);
        }

        //public void Logout()
        //{
        //    CurrentUser = null;
        //    CurrentUserChanged?.Invoke(CurrentUser);
        //}
    }
}
