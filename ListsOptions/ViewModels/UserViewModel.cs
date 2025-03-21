using BusinessLayer.Services;
using ListsOptionsUI.Commands;
using ListsOptionsUI.ViewModels;
using System.Windows;
using System.Windows.Input;

public class UserViewModel : BaseViewModel
{
    private readonly UserService _userService;
    private string _username;
    private string _password;
    private bool _isConfigViewVisible;

    public ICommand LoginCommand { get; }
    public ICommand OpenConfigCommand { get; }

    public UserViewModel(UserService userService)
    {
        _userService = userService;
        LoginCommand = new RelayCommand(async _ => await ExecuteLogin());
        OpenConfigCommand = new RelayCommand(OpenConfig);

        UserDetailsVM = new UserDetailsViewModel(userService);
    }

    public string? CurrentUser => UserSessionService.Instance.CurrentUser?.UserName; // Да бъде името !
    public UserDetailsViewModel UserDetailsVM { get; }

    public string Username
    {
        get => _username;
        set { _username = value; OnPropertyChanged(nameof(Username)); }
    }

    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(nameof(Password)); }
    }

    public bool IsConfigViewVisible
    {
        get => _isConfigViewVisible;
        set { _isConfigViewVisible = value; OnPropertyChanged(nameof(IsConfigViewVisible)); }
    }

    private async Task ExecuteLogin()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            return;

        var user = await _userService.AuthenticateAsync(Username, Password);
        if (user != null)
        {
            OnPropertyChanged(nameof(CurrentUser));

        }
        else
        {
            MessageBox.Show("Грешно име или парола!", "Невалиден потребител", MessageBoxButton.OK);
            // TODO: Покажи грешка
        }
    }

    private void OpenConfig(object o)
    {
        IsConfigViewVisible = true;
    }
}

