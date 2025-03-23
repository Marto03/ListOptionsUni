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

        public UserDetailsViewModel(UserService userService)
        {
            _userService = userService;
            UserTypes = new ObservableCollection<UserTypeEnum>((UserTypeEnum[])Enum.GetValues(typeof(UserTypeEnum)));

            AddUserCommand = new RelayCommand((o) => OpenExpander());
            SaveNewUserCommand = new RelayCommand(async _ => await SaveUserAsync(), _ => CanSaveUser);
            DeleteUserCommand = new RelayCommand(async user => await DeleteUserAsync(user), user => user is UserModel && SelectedUser != null && CurrentUser?.Type == UserTypeEnum.Admin);
            ConfigureUserCommand = new RelayCommand(async user => await ConfigureUser(user), user => user is UserModel && SelectedUser != null && CurrentUser?.Type == UserTypeEnum.Admin);

            NewUser = new UserModel();
            LoadUsersAsync();
        }

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

        private async Task LoadUsersAsync()
        {
            Users.Clear();
            var users = await _userService.GetAllUsersAsync();
            foreach (var user in users)
                Users.Add(user);
        }

        private void OpenExpander()
        {
            NewUser.UserName = null;
            NewUser.Password = null;
            NewUser.Type = UserTypeEnum.Admin;
            OnPropertyChanged(nameof(NewUser));
            IsExpanderOpen = true;
            //OnPropertyChanged(nameof(IsExpanderOpen));
        }

        private bool CanSaveUser => !string.IsNullOrWhiteSpace(NewUser.UserName) &&
                                    !string.IsNullOrWhiteSpace(NewUser.Password) && CurrentUser?.Type == UserTypeEnum.Admin;

        private async Task SaveUserAsync()
        {
            if (!CanSaveUser) return;

            bool success = await _userService.RegisterUserAsync(NewUser);
            if (success)
            {
                //NewUser = new UserModel();
                await LoadUsersAsync();
                IsExpanderOpen = false;
                NewUser = new UserModel();
            }
            else
            {
                // TODO: Покажи съобщение за грешка
            }
        }

        private async Task DeleteUserAsync(object parameter)
        {
            //if (SelectedUser == null) return;
            if (parameter is not UserModel user) return;

            // Задаваме избрания потребител от параметъра
            SelectedUser = user;

            bool success = await _userService.DeleteUserAsync(SelectedUser);
            if (success)
                await LoadUsersAsync();
        }

        //private async Task ConfigureUser()
        //{
        //    if(!IsExpanderOpen) IsExpanderOpen = true;
        //    bool success = await _userService.UpdateUserAsync(SelectedUser);
        //    if (success)
        //        await LoadUsersAsync();

        //    // TODO: Логика за отваряне на конфигурацията на избран потребител
        //}

        private async Task ConfigureUser(object parameter)
        {
            //if (SelectedUser == null) return;

            if (parameter is not UserModel user) return;

            // Задаваме избрания потребител от параметъра
            SelectedUser = user;

            // Копираме данните на избрания потребител в NewUser, за да може да ги редактираме
            NewUser = new UserModel
            {
                Id = SelectedUser.Id,
                UserName = SelectedUser.UserName,
                Type = SelectedUser.Type
            };

            // Отваряме Expander-а за редакция
            IsExpanderOpen = true;
        }
    }

}
