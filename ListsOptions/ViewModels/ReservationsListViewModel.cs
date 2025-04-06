using BusinessLayer.Services;
using DataLayer.Models;
using DataLayer.Services;
using ListsOptionsUI.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{
    public class ReservationsListViewModel : BaseViewModel
    {
        private readonly ReservationService _reservationService;
        private readonly HotelService _hotelService;
        private ObservableCollection<ReservationModel> _reservations;
        private readonly FacilityService facilityService;
        private HotelModel _selectedHotel;

        public ReservationsListViewModel(ReservationService reservationService, HotelService hotelService, FacilityService facilityService)
        {
            _reservationService = reservationService;
            _hotelService = hotelService;

            Hotels = new ObservableCollection<HotelModel>(_hotelService.GetAllHotels());
            Reservations = new ObservableCollection<ReservationModel>();

            LoadReservationsCommand = new RelayCommand(async _ => await LoadReservations());
            this.facilityService = facilityService;
        }

        public ObservableCollection<HotelModel> Hotels { get; }

        public ObservableCollection<ReservationModel> Reservations
        {
            get => _reservations;
            set
            {
                _reservations = value;
                OnPropertyChanged(nameof(Reservations));
            }
        }

        public HotelModel SelectedHotel
        {
            get => _selectedHotel;
            set
            {
                _selectedHotel = value;
                OnPropertyChanged(nameof(SelectedHotel));
                if (_selectedHotel != null)
                {
                    LoadReservations();
                }
            }
        }

        public ICommand LoadReservationsCommand { get; }

        private async Task LoadReservations()
        {
            if (SelectedHotel == null) return;
            var latestHotels = _hotelService.GetAllHotels();
            if (HotelsChanged(latestHotels))
            {
                RefreshHotels();
            }
            var allFacilities = facilityService.GetAllFacilities().ToDictionary(f => f.Id, f => f.Name);
            var reservations = await _reservationService.GetReservationsByHotelAsync(SelectedHotel?.Id ?? 0);

            foreach (var reservation in reservations)
            {
                reservation.UsedFacilitiesAsString = string.Join(", ",
                    reservation.UsedFacilities
                        .Where(id => allFacilities.ContainsKey(id))
                        .Select(id => allFacilities[id]));
            }

            Reservations = new ObservableCollection<ReservationModel>(reservations);
            //Reservations.Clear();
            //foreach (var reservation in reservations)
            //{
            //    Reservations.Add(reservation);
            //}
        }
        private void RefreshHotels()
        {
            Hotels.Clear();
            foreach (var method in _hotelService.GetAllHotels())
            {
                Hotels.Add(method);
            }
        }
        private bool HotelsChanged(List<HotelModel> newHotels)
        {
            if (newHotels.Count != Hotels.Count)
                return true;

            foreach (var newHotel in newHotels)
            {
                var existing = Hotels.FirstOrDefault(h => h.Id == newHotel.Id);
                if (existing == null || existing.Name != newHotel.Name)
                    return true;
            }

            return false;
        }

    }
}
