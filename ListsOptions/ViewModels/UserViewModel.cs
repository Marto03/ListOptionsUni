using HotelApp.BusinessLayer.Services;
using HotelApp.Core.Interfaces;
using ListsOptionsUI.Commands;
using ListsOptionsUI.ViewModels;
using System.Windows;
using System.Windows.Input;

public class UserViewModel : BaseViewModel
{
    #region fields
    private readonly IUserService userService;
    private string username;
    private string password;
    #endregion
    #region Constructor
    public UserViewModel(IUserService userService, IUserSessionService userSessionService)
        : base (userSessionService)
    {
        this.userService = userService;
        LoginCommand = new RelayCommand(async _ => await ExecuteLogin());
    }
    #endregion
    #region Properties
    public ICommand LoginCommand { get; }

    public string Username
    {
        get => username;
        set { username = value; OnPropertyChanged(nameof(Username)); }
    }

    public string Password
    {
        get => password;
        set { password = value; OnPropertyChanged(nameof(Password)); }
    }
    #endregion
    #region Methods
    private async Task ExecuteLogin()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            return;

        var user = await userService.AuthenticateAsync(Username, Password);
        if (user != null)
        {
            OnSuccessLogion();
            OnPropertyChanged(nameof(CurrentUser));
        }
        else
            MessageBox.Show("Грешно име или парола!", "Невалиден потребител", MessageBoxButton.OK);
    }

    private void OnSuccessLogion()
    {
        username = null;
        password = null;
        OnPropertyChanged(nameof(Username));
        OnPropertyChanged(nameof(Password));
    }
    #endregion
}

