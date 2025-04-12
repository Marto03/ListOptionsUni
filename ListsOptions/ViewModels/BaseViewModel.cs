using HotelApp.BusinessLayer.Services;
using HotelApp.Core.Interfaces;
using HotelApp.Core.Models;
using System.ComponentModel;

namespace ListsOptionsUI.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private readonly IUserSessionService _userSessionService;

        // Инжектиране на зависимостта чрез конструктор
        public BaseViewModel(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
            _userSessionService.CurrentUserChanged += OnCurrentUserChanged;
        }

        public UserModel? CurrentUser => _userSessionService.CurrentUser;
        public int? CurrentHotel => _userSessionService.CurrentUser?.HotelId;

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
