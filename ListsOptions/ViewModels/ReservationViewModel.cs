using BusinessLayer.Services;
using DataLayer.Models;
using DataLayer.Services;
using ListsOptionsUI.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{
    public class ReservationViewModel : BaseViewModel
    {
        private readonly ReservationService _reservationService;
        private readonly HotelService _hotelService;
        private readonly FacilityService _facilityService;
        private readonly PaymentMethodService _paymentMethodService;

        public ReservationViewModel(ReservationService reservationService, HotelService hotelService, FacilityService facilityService, PaymentMethodService paymentMethodService)
        {
            _reservationService = reservationService;
            _hotelService = hotelService;
            _facilityService = facilityService;
            _paymentMethodService = paymentMethodService;

            Hotels = new ObservableCollection<HotelModel>(_hotelService.GetAllHotels());
            Facilities = new ObservableCollection<FacilityModel>(_facilityService.GetAllFacilities());
            PaymentMethods = new ObservableCollection<PaymentMethodModel>(_paymentMethodService.GetPaymentMethods());

            CreateReservationCommand = new RelayCommand(async _ => await CreateReservationAsync(), _ => CanCreateReservation);
        }

        public ObservableCollection<HotelModel> Hotels { get; }
        public ObservableCollection<FacilityModel> Facilities { get; }
        public ObservableCollection<PaymentMethodModel> PaymentMethods { get; }

        private HotelModel _selectedHotel;
        public HotelModel SelectedHotel
        {
            get => _selectedHotel;
            set
            {
                _selectedHotel = value;
                OnPropertyChanged(nameof(SelectedHotel));
            }
        }

        private FacilityModel _selectedFacility;
        public FacilityModel SelectedFacility
        {
            get => _selectedFacility;
            set
            {
                _selectedFacility = value;
                OnPropertyChanged(nameof(SelectedFacility));
            }
        }

        private PaymentMethodModel _selectedPaymentMethod;
        public PaymentMethodModel SelectedPaymentMethod
        {
            get => _selectedPaymentMethod;
            set
            {
                _selectedPaymentMethod = value;
                OnPropertyChanged(nameof(SelectedPaymentMethod));
            }
        }

        private RoomTypeEnum _selectedRoomType;
        public RoomTypeEnum SelectedRoomType
        {
            get => _selectedRoomType;
            set
            {
                _selectedRoomType = value;
                OnPropertyChanged(nameof(SelectedRoomType));
            }
        }

        private DateTime _checkInDate;
        public DateTime CheckInDate
        {
            get => _checkInDate;
            set
            {
                _checkInDate = value;
                OnPropertyChanged(nameof(CheckInDate));
            }
        }

        private DateTime _checkOutDate;
        public DateTime CheckOutDate
        {
            get => _checkOutDate;
            set
            {
                _checkOutDate = value;
                OnPropertyChanged(nameof(CheckOutDate));
            }
        }

        public ICommand CreateReservationCommand { get; }

        private bool CanCreateReservation => SelectedHotel != null && SelectedPaymentMethod != null && CheckInDate < CheckOutDate;

        private async Task CreateReservationAsync()
        {
            try
            {
                var reservation = new ReservationModel
                {
                    HotelId = SelectedHotel.Id,
                    UserId = CurrentUser.Id,
                    RoomType = SelectedRoomType,
                    CheckInDate = CheckInDate,
                    CheckOutDate = CheckOutDate,
                    Price = CalculatePrice(),
                    PaymentId = SelectedPaymentMethod.Id,
                    UsedFacilities = new List<FacilityModel> { SelectedFacility }
                };

                //await _reservationService.AddReservationAsync(reservation);
                MessageBox.Show("Reservation created successfully!", "Success", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK);
            }
        }

        private decimal CalculatePrice()
        {
            // Implement your price calculation logic here
            return 100m; // Placeholder value
        }
    }
}
