using BusinessLayer.Services;
using DataLayer.Models;
using ListsOptionsUI.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{
    public class UserDetailsViewModel : BaseViewModel
    {
        private readonly UserService _userService;
        private UserModel _selectedUser;
        private UserModel _newUser;
        private bool _isExpanderOpen;

        public ObservableCollection<UserModel> Users { get; set; } = new();
        public ObservableCollection<UserTypeEnum> UserTypes { get; }

        public UserModel SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public UserModel NewUser
        {
            get => _newUser;
            set
            {
                _newUser = value;
                OnPropertyChanged(nameof(NewUser));
            }
        }

        public bool IsExpanderOpen
        {
            get => _isExpanderOpen;
            set
            {
                _isExpanderOpen = value;
                OnPropertyChanged(nameof(IsExpanderOpen));
            }
        }

        public ICommand AddUserCommand { get; }
        public ICommand SaveNewUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand ConfigureUserCommand { get; }

        public UserDetailsViewModel(UserService userService)
        {
            _userService = userService;
            UserTypes = new ObservableCollection<UserTypeEnum>((UserTypeEnum[])Enum.GetValues(typeof(UserTypeEnum)));

            AddUserCommand = new RelayCommand((o) => OpenExpander());
            SaveNewUserCommand = new RelayCommand(async _ => await SaveUserAsync(), _ => CanSaveUser);
            DeleteUserCommand = new RelayCommand(async _ => await DeleteUserAsync(), _ => SelectedUser != null);
            ConfigureUserCommand = new RelayCommand(async _ => await ConfigureUser());

            NewUser = new UserModel();
            LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            Users.Clear();
            var users = await _userService.GetAllUsersAsync();
            foreach (var user in users)
                Users.Add(user);
        }

        private void OpenExpander()
        {
            IsExpanderOpen = !IsExpanderOpen;
        }

        private bool CanSaveUser => !string.IsNullOrWhiteSpace(NewUser.UserName) &&
                                    !string.IsNullOrWhiteSpace(NewUser.Password);

        private async Task SaveUserAsync()
        {
            if (!CanSaveUser) return;

            bool success = await _userService.RegisterUserAsync(NewUser);
            if (success)
            {
                //NewUser = new UserModel();
                await LoadUsersAsync();
                IsExpanderOpen = false;
            }
            else
            {
                // TODO: Покажи съобщение за грешка
            }
        }

        private async Task DeleteUserAsync()
        {
            if (SelectedUser == null) return;

            bool success = await _userService.DeleteUserAsync(SelectedUser);
            if (success)
                await LoadUsersAsync();
        }

        private async Task ConfigureUser()
        {
            if(!IsExpanderOpen) IsExpanderOpen = true;
            bool success = await _userService.UpdateUserAsync(SelectedUser);
            if (success)
                await LoadUsersAsync();

            // TODO: Логика за отваряне на конфигурацията на избран потребител
        }
    }

}
