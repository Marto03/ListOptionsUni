using BusinessLayer.Services;
using DataLayer.Models;
using System.ComponentModel;

namespace ListsOptionsUI.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public UserModel CurrentUser => UserSessionService.Instance.CurrentUser;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
