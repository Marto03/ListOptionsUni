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

        public event PropertyChangedEventHandler? PropertyChanged;
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
