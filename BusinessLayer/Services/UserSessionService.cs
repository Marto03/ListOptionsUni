using DataLayer.Models;

namespace BusinessLayer.Services
{
    public class UserSessionService
    {
        private static UserSessionService _instance;
        public static UserSessionService Instance => _instance ??= new UserSessionService();

        public UserModel? CurrentUser { get; private set; }

        private UserSessionService() { }

        public void SetCurrentUser(UserModel user)
        {
            CurrentUser = user;
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public bool IsLoggedIn => CurrentUser != null;
        public bool IsAdmin => CurrentUser?.Type == UserTypeEnum.Admin;
    }
}
