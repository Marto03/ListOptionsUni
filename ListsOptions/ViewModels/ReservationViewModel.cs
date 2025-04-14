using HotelApp.Core.DTOs;
using HotelApp.Core.Interfaces;
using HotelApp.Core.Models;
using ListsOptionsUI.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{
    public class ReservationViewModel : BaseViewModel
    {
        private readonly IReservationService _reservationService;
        private readonly IHotelFacilityService _hotelFacilityService;
        private readonly IPaymentMethodService _paymentMethodService;
        private RoomTypeEnum _selectedRoomType;
        private PaymentMethodModel _selectedPaymentMethod;
        private DateTime _checkInDate = DateTime.Today;
        private DateTime _checkOutDate = DateTime.Today.AddDays(1);
        private decimal _totalPrice;
        private int? _previousHotelId;
        private bool CanAddReservation => SelectedPaymentMethod != null && CheckOutDate > CheckInDate && CurrentUser?.HotelId != null;

        public ReservationViewModel(IReservationService reservationService, IHotelFacilityService hotelFacilityService, 
            IPaymentMethodService paymentMethodService, IUserSessionService userSessionService)
            : base (userSessionService)
        {
            _reservationService = reservationService;
            _hotelFacilityService = hotelFacilityService;
            _paymentMethodService = paymentMethodService;

            RoomTypes = new ObservableCollection<RoomTypeEnum>((RoomTypeEnum[])Enum.GetValues(typeof(RoomTypeEnum)));
            PaymentMethods = new ObservableCollection<PaymentMethodModel>(_paymentMethodService.GetPaymentMethods());
            AvailableFacilities = new ObservableCollection<HotelFacilityDTO>();
            _previousHotelId = CurrentUser?.HotelId;
            FacilitySelectionChangedCommand = new RelayCommand(async _ => await Refresh());

            SaveReservationCommand = new RelayCommand(async _ => await SaveReservationAsync(), _ => CanAddReservation);
            LoadFacilities();

        }
        public ICommand FacilitySelectionChangedCommand { get; }
        public ObservableCollection<RoomTypeEnum> RoomTypes { get; }
        public ObservableCollection<PaymentMethodModel> PaymentMethods { get; }
        public ObservableCollection<HotelFacilityDTO> AvailableFacilities { get; }
        public ObservableCollection<HotelFacilityDTO> SelectedFacilities { get; } = new();

        public RoomTypeEnum SelectedRoomType
        {
            get => _selectedRoomType;
            set
            {
                _selectedRoomType = value;
                OnPropertyChanged(nameof(SelectedRoomType));
                CalculateTotalPrice();
            }
        }

        public PaymentMethodModel SelectedPaymentMethod
        {
            get => _selectedPaymentMethod;
            set
            {
                _selectedPaymentMethod = value;
                OnPropertyChanged(nameof(SelectedPaymentMethod));
            }
        }

        public DateTime CheckInDate
        {
            get => _checkInDate;
            set
            {
                _checkInDate = value;
                OnPropertyChanged(nameof(CheckInDate));
                CalculateTotalPrice();
            }
        }

        public DateTime CheckOutDate
        {
            get => _checkOutDate;
            set
            {
                _checkOutDate = value;
                OnPropertyChanged(nameof(CheckOutDate));
                CalculateTotalPrice();
            }
        }

        public decimal TotalPrice
        {
            get => _totalPrice;
            set
            {
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public ICommand SaveReservationCommand { get; }

        private async Task SaveReservationAsync()
        {
            if (!CanAddReservation) return;

            try
            {
                var reservation = new ReservationModel
                {
                    HotelId = CurrentUser.HotelId.Value,
                    UserId = CurrentUser.Id,
                    RoomType = SelectedRoomType,
                    CheckInDate = CheckInDate,
                    CheckOutDate = CheckOutDate,
                    Price = TotalPrice,
                    UsedFacilities = AvailableFacilities.Where(r=>r.IsSelected).Select(f => f.FacilityId).ToList(),
                    PaymentId = SelectedPaymentMethod.Id
                };

                await _reservationService.AddReservationAsync(reservation);
                MessageBox.Show("Резервацията е създадена успешно!", "Успешен запис", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неуспешен запис: {ex.Message}", "Грешка", MessageBoxButton.OK);
            }
        }

        private void LoadFacilities()
        {
            if (CurrentUser?.HotelId != null)
            {
                var facilities = _hotelFacilityService.GetFacilitiesForHotel(CurrentUser.HotelId.Value);
                AvailableFacilities.Clear();
                foreach (var facility in facilities)
                {
                    //facility.PropertyChanged += Facility_PropertyChanged;
                    AvailableFacilities.Add(facility);
                }
            }
            else
                AvailableFacilities.Clear();
        }

        private async Task CalculateTotalPrice()
        {
            if (CheckOutDate <= CheckInDate) return;

            int numberOfNights = Math.Max(1, (int)(CheckOutDate - CheckInDate).TotalDays);

            TotalPrice = await _reservationService.CalculateTotalPriceAsync(CurrentUser?.HotelId ?? 0, AvailableFacilities?.Where(r => r.IsSelected).Select(f => f.FacilityId).ToList(), SelectedRoomType, numberOfNights);
        }
        private async Task Refresh()
        {
            if (CurrentUser?.HotelId != null && (_previousHotelId != CurrentUser?.HotelId || _hotelFacilityService.GetFacilitiesForHotel(CurrentUser?.HotelId.Value)?.Count() != AvailableFacilities?.Count()))
            {
                _previousHotelId = CurrentUser?.HotelId;
                LoadFacilities();
            }
            var latestMethods = _paymentMethodService.GetPaymentMethods();
            if (PaymentMethodsChanged(latestMethods))
            {
                RefreshPaymentMethods();
            }

            if (CheckOutDate <= CheckInDate) return;

            int numberOfNights = Math.Max(1, (int)(CheckOutDate - CheckInDate).TotalDays);

            TotalPrice = await _reservationService.CalculateTotalPriceAsync(CurrentUser?.HotelId ?? 0, AvailableFacilities?.Where(r => r.IsSelected).Select(f => f.FacilityId).ToList(), SelectedRoomType, numberOfNights);
        }
        private void RefreshPaymentMethods()
        {
            PaymentMethods.Clear();
            foreach (var method in _paymentMethodService.GetPaymentMethods())
            {
                PaymentMethods.Add(method);
            }
        }
        private bool PaymentMethodsChanged(List<PaymentMethodModel> newMethods)
        {
            if (newMethods.Count != PaymentMethods.Count)
                return true;

            for (int i = 0; i < newMethods.Count; i++)
            {
                var existing = PaymentMethods.FirstOrDefault(m => m.Id == newMethods[i].Id);
                if (existing == null || existing.Name != newMethods[i].Name)
                    return true;
            }

            return false;
        }
    }
}