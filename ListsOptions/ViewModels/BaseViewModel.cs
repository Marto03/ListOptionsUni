using BusinessLayer.Services;
using DataLayer.Models;
using System.ComponentModel;

namespace ListsOptionsUI.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            UserSessionService.Instance.CurrentUserChanged += OnCurrentUserChanged;
        }

        public UserModel? CurrentUser => UserSessionService.Instance.CurrentUser;
        public int? CurrentHotel => UserSessionService.Instance.CurrentUser.HotelId;

        public event PropertyChangedEventHandler? PropertyChanged;

        internal bool CanSave => CurrentUser?.Type == UserTypeEnum.Admin;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnCurrentUserChanged(UserModel? user)
        {
            OnPropertyChanged(nameof(CurrentUser));
        }
    }
}
