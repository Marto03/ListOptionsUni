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

        public ObservableCollection<HotelFacilityModel> AvailableFacilities { get; set; } = new();
        public ObservableCollection<HotelFacilityModel> SelectedFacilities { get; set; } = new();
        public ObservableCollection<PaymentMethodModel> PaymentMethods { get; set; } = new();
        public ObservableCollection<RoomTypeEnum> RoomTypes { get; set; } = new();

        public RoomTypeEnum SelectedRoomType { get; set; }
        public HotelModel CurrentHotel { get; set; }
        public int CurrentUserId { get; set; }
        public PaymentMethodModel SelectedPaymentMethod { get; set; }

        public DateTime CheckInDate { get; set; } = DateTime.Today;
        public DateTime CheckOutDate { get; set; } = DateTime.Today.AddDays(1);

        public ICommand AddReservationCommand { get; set; }

        public ReservationViewModel(ReservationService reservationService)
        {
            _reservationService = reservationService;
            AddReservationCommand = new RelayCommand(async _ => await AddReservationAsync());
            LoadRoomTypes();
        }

        private void LoadRoomTypes()
        {
            RoomTypes.Clear();
            foreach (var type in Enum.GetValues(typeof(RoomTypeEnum)).Cast<RoomTypeEnum>())
            {
                RoomTypes.Add(type);
            }
        }

        public void LoadData(HotelModel hotel, int userId,
            IEnumerable<HotelFacilityModel> hotelFacilities, IEnumerable<PaymentMethodModel> paymentMethods)
        {
            CurrentHotel = hotel;
            CurrentUserId = userId;

            AvailableFacilities = new ObservableCollection<HotelFacilityModel>(hotelFacilities);
            PaymentMethods = new ObservableCollection<PaymentMethodModel>(paymentMethods);
        }

        private async Task AddReservationAsync()
        {
            if (SelectedPaymentMethod == null || CheckOutDate <= CheckInDate)
                return;

            await _reservationService.AddReservationAsync(
                hotelId: CurrentHotel.Id,
                userId: CurrentUserId,
                roomType: SelectedRoomType,
                checkIn: CheckInDate,
                checkOut: CheckOutDate,
                selectedFacilities: SelectedFacilities.ToList(),
                paymentMethodId: SelectedPaymentMethod.Id);
        }


        //private readonly ReservationService _reservationService;
        //private readonly HotelModel _currentHotel;
        //private readonly UserModel _currentUser;

        //public ObservableCollection<FacilityModel> AvailableFacilities { get; set; }
        //public ObservableCollection<HotelFacilityModel> SelectedFacilities { get; set; } = new();

        //public ObservableCollection<PaymentModel> PaymentMethods { get; set; }

        //public RoomTypeEnum SelectedRoomType { get; set; }
        //public PaymentMethodModel SelectedPaymentMethod { get; set; }
        //public HotelFacilityModel SelectedFacility { get; set; }

        //public DateTime CheckInDate { get; set; } = DateTime.Now;
        //public DateTime CheckOutDate { get; set; } = DateTime.Now.AddDays(1);

        //public ICommand AddFacilityCommand { get; }
        //public ICommand SaveReservationCommand { get; }

        //public ReservationViewModel(ReservationService reservationService, HotelModel hotel, UserModel user)
        //{
        //    _reservationService = reservationService;
        //    _currentHotel = hotel;
        //    _currentUser = user;

        //    LoadInitialData();

        //    AddFacilityCommand = new RelayCommand(AddFacility);
        //    SaveReservationCommand = new RelayCommand(async () => await SaveReservation());
        //}

        //private void LoadInitialData()
        //{
        //    AvailableFacilities = new ObservableCollection<FacilityModel>(_currentHotel.Facilities);
        //    PaymentMethods = new ObservableCollection<PaymentModel>(_currentHotel.Payments);
        //}

        //private void AddFacility()
        //{
        //    if (SelectedFacility != null && !SelectedFacilities.Contains(SelectedFacility))
        //        SelectedFacilities.Add(SelectedFacility);
        //}

        //private async Task SaveReservation()
        //{
        //    await _reservationService.AddReservationAsync(
        //        _currentHotel.Id,
        //        _currentUser.Id,
        //        SelectedRoomType,
        //        CheckInDate,
        //        CheckOutDate,
        //        SelectedFacilities.ToList(),
        //        SelectedPaymentMethod.Id
        //    );

        //    MessageBox.Show("Резервацията е записана успешно!");
        //}
    }

}
