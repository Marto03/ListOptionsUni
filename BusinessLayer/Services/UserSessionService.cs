using DataLayer.Models;

namespace BusinessLayer.Services
{
    public class UserSessionService
    {
        private static UserSessionService _instance;

        public UserSessionService() { }

        public bool IsLoggedIn => CurrentUser != null;
        public bool IsAdmin => CurrentUser?.Type == UserTypeEnum.Admin;
        public static UserSessionService Instance => _instance ??= new UserSessionService();
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
