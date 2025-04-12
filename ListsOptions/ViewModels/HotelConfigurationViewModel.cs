using HotelApp.BusinessLayer.Services;
using HotelApp.Core.Interfaces;
using HotelApp.Core.Models;
using ListsOptions;
using ListsOptionsUI.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{
    public class HotelConfigurationViewModel : BaseViewModel
    {
        private readonly IHotelService _hotelService;
        private readonly IUserService _userService;
        private string newHotelName;
        private HotelModel? _selectedHotel;

        private bool CanSave => CurrentUser?.Type == UserTypeEnum.Admin;
        public HotelConfigurationViewModel(IUserSessionService userSessionService) : base (userSessionService)
        {
            _hotelService = App.ServiceProvider.GetRequiredService<IHotelService>();
            _userService = App.ServiceProvider.GetRequiredService<IUserService>();

            SaveHotelCommand = new RelayCommand(async _ => await SaveHotelSelectionAsync(), _ => CanSave);
            AddHotelCommand = new RelayCommand(AddNewHotel , _ => CanSave);
            SelectHotelCommand = new RelayCommand(SelectHotel);

            LoadHotels();

            userSessionService.CurrentUserChanged += OnCurrentUserChanged;
        }
        public ObservableCollection<HotelModel> Hotels { get; set; } = new();

        public HotelModel? SelectedHotel
        {
            get => _selectedHotel;
            set
            {
                _selectedHotel = value;
                OnPropertyChanged(nameof(SelectedHotel));
            }
        }
        public string NewHotelName
        {
            get
            {
                return newHotelName;
            }
            set
            {
                if (newHotelName != value)
                    newHotelName = value;
                OnPropertyChanged(nameof(newHotelName));
            }
        }
        public ICommand SaveHotelCommand { get; }
        public ICommand AddHotelCommand { get; }
        public ICommand SelectHotelCommand { get; }

        private void LoadHotels()
        {
            Hotels.Clear();
            var hotelsFromDb = _hotelService.GetAllHotels();
            foreach (var hotel in hotelsFromDb)
            {
                Hotels.Add(hotel);
            }

            UpdateSelectedHotel();
        }

        private void UpdateSelectedHotel()
        {
            if (CurrentUser?.HotelId is int hotelId)
            {
                SelectedHotel = Hotels.FirstOrDefault(h => h.Id == hotelId);
            }
            else
            {
                SelectedHotel = null;
            }
        }

        private void OnCurrentUserChanged(UserModel? user)
        {
            UpdateSelectedHotel();
        }

        private void SelectHotel(object o)
        {
            var hotel = o as HotelModel;
            if (SelectedHotel != hotel)
            {
                SelectedHotel = hotel; // Променяме само ако е различен
            }
        }

        private async Task SaveHotelSelectionAsync()
        {
            try
            {
                if (SelectedHotel != null && CurrentUser != null)
                {
                    CurrentUser.HotelId = SelectedHotel.Id;
                    await _userService.UpdateUserHotelAsync(CurrentUser);

                    Events.AppEvents.RaiseUsersChanged();
                    //OnPropertyChanged(nameof(App.ServiceProvider.GetRequiredService<UserDetailsViewModel>().Users));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Възникна грешка", MessageBoxButton.OK);
            }
        }

        private void AddNewHotel(object o)
        {
            var newHotel = new HotelModel
            {
                Name = NewHotelName,
            };
            try
            {
                _hotelService.AddHotel(newHotel);
                Hotels.Add(newHotel);
            }
            catch(InvalidOperationException e)
            {
                MessageBox.Show(e.Message, "Грешен хотел", MessageBoxButton.OK);
            }
            finally
            {
                NewHotelName = null;
            }
        }
    }
}
