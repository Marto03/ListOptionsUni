using BusinessLayer.Services;
using DataLayer.Models;
using ListsOptions;
using ListsOptionsUI.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{
    public class HotelConfigurationViewModel : BaseViewModel
    {
        private readonly HotelService _hotelService;
        private readonly UserService _userService;
        private string newHotelName;
        private HotelModel? _selectedHotel;

        private bool CanSave => CurrentUser?.Type == UserTypeEnum.Admin;
        public HotelConfigurationViewModel()
        {
            _hotelService = App.ServiceProvider.GetRequiredService<HotelService>();
            _userService = App.ServiceProvider.GetRequiredService<UserService>();

            SaveHotelCommand = new RelayCommand(async _ => await SaveHotelSelectionAsync(), _ => CanSave);
            AddHotelCommand = new RelayCommand(AddNewHotel , _ => CanSave);
            SelectHotelCommand = new RelayCommand(SelectHotel);

            LoadHotels();

            UserSessionService.Instance.CurrentUserChanged += OnCurrentUserChanged;
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
            if (SelectedHotel != null && CurrentUser != null)
            {
                CurrentUser.HotelId = SelectedHotel.Id;
                await _userService.UpdateUserHotelAsync(CurrentUser);

                Events.AppEvents.RaiseUsersChanged();
                //OnPropertyChanged(nameof(App.ServiceProvider.GetRequiredService<UserDetailsViewModel>().Users));
            }
        }

        private void AddNewHotel(object o)
        {
            var newHotel = new HotelModel
            {
                Name = NewHotelName,
            };

            _hotelService.AddHotel(newHotel);
            Hotels.Add(newHotel);
        }
    }
}
