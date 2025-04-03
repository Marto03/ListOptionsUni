using DataLayer.Models;

namespace BusinessLayer.Services
{
    public class UserSessionService
    {
        private static UserSessionService _instance;

        private UserSessionService() { }

        public bool IsLoggedIn => CurrentUser != null;
        public bool IsAdmin => CurrentUser?.Type == UserTypeEnum.Admin;
        public static UserSessionService Instance => _instance ??= new UserSessionService();
        public UserModel? CurrentUser { get; private set; }


        public void SetCurrentUser(UserModel user)
        {
            CurrentUser = user;
        }

        //public void Logout()
        //{
        //    CurrentUser = null;
        //}

    }
}
