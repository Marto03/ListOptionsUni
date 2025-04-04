using BusinessLayer.Services;
using Common;
using Common.CustomExceptions;
using DataLayer.Models;
using ListsOptions;
using ListsOptionsUI.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{
    public class UserDetailsViewModel : BaseViewModel
    {
        #region fields
        private readonly UserService userService;
        private UserModel selectedUser;
        private UserModel newUser;
        private bool isExpanderOpen;
        private bool isConfiguringExistingUser;
        private bool CanSaveUser => !string.IsNullOrWhiteSpace(NewUser.UserName) &&
                                    !string.IsNullOrWhiteSpace(NewUser.Password) &&
                                    CurrentUser?.Type == UserTypeEnum.Admin;
        #endregion
        #region Constructor
        public UserDetailsViewModel(UserService userService)
        {
            this.userService = userService;
            UserTypes = new ObservableCollection<UserTypeEnum>((UserTypeEnum[])Enum.GetValues(typeof(UserTypeEnum)));

            AddUserCommand = new RelayCommand((o) => AddNewUser());
            SaveNewUserCommand = new RelayCommand(async _ => await SaveUserAsync(), _ => CanSaveUser);
            DeleteUserCommand = new RelayCommand(async user => await DeleteUserAsync(user), user => user is UserModel selectedUser && selectedUser.Id != 1 && selectedUser != CurrentUser && CurrentUser?.Type == UserTypeEnum.Admin);

            ConfigureUserCommand = new RelayCommand(async user => await ConfigureUser(user), user => user is UserModel selectedUser && CurrentUser?.Type == UserTypeEnum.Admin);
            NewUser = new UserModel();
            LoadUsersAsync();

            //UserSessionService.Instance.CurrentUserChanged += OnCurrentUserChanged;
            Events.AppEvents.UsersChanged += async () => await LoadUsersAsync();
        }
        #endregion
        #region Properties
        public ObservableCollection<UserModel> Users { get; set; } = new();
        public ObservableCollection<UserTypeEnum> UserTypes { get; }
        public ICommand AddUserCommand { get; }
        public ICommand SaveNewUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand ConfigureUserCommand { get; }
        public UserModel SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public UserModel NewUser
        {
            get => newUser;
            set
            {
                newUser = value;
                OnPropertyChanged(nameof(NewUser));
            }
        }

        public bool IsExpanderOpen
        {
            get => isExpanderOpen;
            set
            {
                isExpanderOpen = value;
                OnPropertyChanged(nameof(IsExpanderOpen));
            }
        }
        #endregion
        #region Methods
        private async Task LoadUsersAsync()
        {
            Users.Clear();
            var users = await userService.GetAllUsersAsync();
            foreach (var user in users)
                Users.Add(user);
        }

        private void AddNewUser()
        {
            NewUser.UserName = null;
            NewUser.Password = null;
            NewUser.Type = UserTypeEnum.Admin;
            OnPropertyChanged(nameof(NewUser));
            OnPropertyChanged(nameof(NewUser.UserName));
            OnPropertyChanged(nameof(NewUser.Password));
            IsExpanderOpen = true;
            isConfiguringExistingUser = false;
        }

        private async Task SaveUserAsync()
        {
            if (!CanSaveUser) return;

            try
            {
                //var paymentService = App.ServiceProvider.GetRequiredService<PaymentService>();
                //var test = App.ServiceProvider.GetRequiredService<HotelService>();

                await userService.ManageUserAsync(NewUser, isConfiguringExistingUser);

                await LoadUsersAsync();
                IsExpanderOpen = false;
                NewUser = new UserModel();
            }
            catch (UserAlreadyExistsException e)
            {
                MessageBox.Show(e.Message, "Невалиден потребител", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Грешка: {ex.Message}", "Възникна неочаквана грешка", MessageBoxButton.OK);
            }
        }

        private async Task DeleteUserAsync(object parameter)
        {
            if (parameter is not UserModel user) return;

            SelectedUser = user;

            bool success = await userService.DeleteUserAsync(SelectedUser);
            if (success)
                await LoadUsersAsync();
        }

        private async Task ConfigureUser(object parameter)
        {
            if (parameter is not UserModel user) return;

            SelectedUser = user;
            isConfiguringExistingUser = true;
            NewUser = new UserModel
            {
                Id = SelectedUser.Id,
                UserName = SelectedUser.UserName,
                Type = SelectedUser.Type
            };

            IsExpanderOpen = true;
        }

        //private void OnCurrentUserChanged(UserModel? user)
        //{
        //    OnPropertyChanged(nameof(Users));
        //}
        #endregion
    }

}
