using BusinessLayer.Services;
using ListsOptionsUI.Commands;
using ListsOptionsUI.ViewModels;
using System.Windows;
using System.Windows.Input;

public class UserViewModel : BaseViewModel
{
    #region fields
    private readonly UserService userService;
    private string username;
    private string password;
    #endregion
    #region Constructor
    public UserViewModel(UserService userService)
    {
        this.userService = userService;
        UserDetailsVM = new UserDetailsViewModel(userService);
        LoginCommand = new RelayCommand(async _ => await ExecuteLogin());
    }
    #endregion
    #region Properties
    public ICommand LoginCommand { get; }
    public ICommand OpenConfigCommand { get; }

    public UserDetailsViewModel UserDetailsVM { get; }

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

