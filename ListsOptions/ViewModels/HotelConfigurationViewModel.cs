using BusinessLayer.Services;
using DataLayer.Models;
using ListsOptions;
using ListsOptionsUI.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{
    public class HotelConfigurationViewModel : BaseViewModel
    {
        private readonly HotelService _hotelService;
        private readonly UserService _userService;
        private string newHotelName;
        public ObservableCollection<HotelModel> Hotels { get; set; } = new();

        private HotelModel? _selectedHotel;
        public HotelModel? SelectedHotel
        {
            get => _selectedHotel;
            set
            {
                _selectedHotel = value;
                OnPropertyChanged(nameof(SelectedHotel));
                //UpdateHotelSelection(value);
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

        private bool CanSave => CurrentUser?.Type == UserTypeEnum.Admin;
        public HotelConfigurationViewModel()
        {
            _hotelService = App.ServiceProvider.GetRequiredService<HotelService>();
            _userService = App.ServiceProvider.GetRequiredService<UserService>();

            SaveHotelCommand = new RelayCommand(async _ => await SaveHotelSelectionAsync(), _ => CanSave );
            AddHotelCommand = new RelayCommand(AddNewHotel);
            SelectHotelCommand = new RelayCommand(SelectHotel);
            LoadHotels();
        }

        private void LoadHotels()
        {
            Hotels.Clear();
            var hotelsFromDb = _hotelService.GetAllHotels();
            foreach (var hotel in hotelsFromDb)
            {
                Hotels.Add(hotel);
            }

            if (CurrentUser?.HotelId is int hotelId)
            {
                SelectedHotel = Hotels.FirstOrDefault(h => h.Id == hotelId);
            }
        }


        private void SelectHotel(object o)
        {
            var hotel = o as HotelModel;
            if (SelectedHotel != hotel)
            {
                SelectedHotel = hotel; // Променяме само ако е различен
            }
        }
        //private void UpdateHotelSelection(HotelModel? selected)
        //{
        //    // Обновяваме всички хотели, за да направим избран само този, който е в SelectedHotel
        //    foreach (var hotel in Hotels)
        //    {
        //        hotel.IsSelected = hotel == SelectedHotel;
        //    }
        //}

        private async Task SaveHotelSelectionAsync()
        {
            if (SelectedHotel != null && CurrentUser != null)
            {
                CurrentUser.HotelId = SelectedHotel.Id;
                await _userService.UpdateUserHotelAsync(CurrentUser);
            }
        }

        private void AddNewHotel(object o)
        {
            // Тук можеш да отвориш нов прозорец или изглед за създаване на хотел
            // Пример: new AddHotelWindow().ShowDialog();
            var newHotel = new HotelModel
            {
                Name = NewHotelName, // Тук може да добавиш полета за въвеждане от потребителя
                // Добави и други необходими полета...
            };

            // Добавяне на новия хотел в базата
            _hotelService.AddHotel(newHotel);  // Предполага се, че има такава услуга
            Hotels.Add(newHotel);  // 
        }

    }
}
