using HotelApp.Core.DTOs;
using HotelApp.Core.Interfaces;
using HotelApp.Core.Models;
using ListsOptionsUI.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{
    public class HotelFacilityEditorViewModel : BaseViewModel
    {
        private readonly IHotelFacilityService _facilityService;
        private readonly IFacilityService _facilityListService;
        private DateTime? fromDate = DateTime.Today;
        private DateTime? toDate = DateTime.Today.AddDays(1);
        private bool useDateRange;

        public HotelFacilityEditorViewModel(IHotelFacilityService service, IFacilityService facilityListService, IUserSessionService userSessionService) : base(userSessionService)
        {
            _facilityService = service;
            _facilityListService = facilityListService;
            HotelListByFacility = new HotelListByFacilityViewModel(service,facilityListService);
            AddFacilityCommand = new RelayCommand(AddFacility, _ => CanSave && CurrentHotel != null);
            RemoveFacilityCommand = new RelayCommand<HotelFacilityDTO>(RemoveFacility);
            SaveCommand = new RelayCommand(SaveFacilities, _ => CanSave && CurrentHotel != null);

            LoadFacilities();
            userSessionService.CurrentUserChanged += OnCurrentUserChanged;
            Events.AppEvents.UsersChanged += () => LoadFacilities();
        }

        public DateTime? FromDate
        {
            get
            {
                return fromDate;
            }
            set
            {
                if (fromDate != value)
                    fromDate = value;
                OnPropertyChanged(nameof(FromDate));
            }
        }
        public DateTime? ToDate
        {
            get
            {
                return toDate;
            }
            set
            {
                if (toDate != value)
                    toDate = value;

                OnPropertyChanged(nameof(ToDate));
            }
        }
        public bool UseDateRange
        {
            get => useDateRange;
            set
            {
                if (useDateRange != value)
                    useDateRange = value;
                OnPropertyChanged(nameof(UseDateRange));
            }
        }

        public HotelListByFacilityViewModel HotelListByFacility { get; set; }

        public ObservableCollection<FacilityModel> AvailableFacilities { get; set; } = new();
        public ObservableCollection<HotelFacilityDTO> FacilityList { get; set; } = new();

        public FacilityModel SelectedFacility { get; set; }
        public decimal NewPrice { get; set; }
        public double NewDiscount { get; set; }

        public string CurrentHotel { get; set; }

        public ICommand AddFacilityCommand { get; }
        public ICommand RemoveFacilityCommand { get; }
        public ICommand SaveCommand { get; }
        public void LoadFacilities()
        {
            OnPropertyChanged(nameof(CurrentUser));
            CurrentHotel = _facilityService.GetHotelNameByUserHotelId(CurrentUser?.HotelId ?? 0);
            var available = _facilityListService.GetAllFacilities();
            AvailableFacilities.Clear();
            foreach (var f in available)
                AvailableFacilities.Add(f);

            var current = _facilityService.GetFacilitiesForHotel(CurrentUser?.HotelId ?? 0);
            FacilityList.Clear();
            foreach (var f in current)
                FacilityList.Add(f);
            OnPropertyChanged(nameof(CurrentHotel));
        }
        private void OnCurrentUserChanged(UserModel? user)
        {
            LoadFacilities();
        }

        private void AddFacility(object o)
        {
            if (SelectedFacility == null || FacilityList.Any(r => r.FacilityId == SelectedFacility.Id) || NewPrice == 0) return;

            FacilityList.Add(new HotelFacilityDTO
            {
                FacilityId = SelectedFacility.Id,
                FacilityName = SelectedFacility.Name,
                Price = NewPrice,
                Discount = NewDiscount,
                FromDate = UseDateRange ? FromDate : null,
                ToDate = UseDateRange ? ToDate : null
            });

            // Clear input
            SelectedFacility = null;
            NewPrice = 0;
            NewDiscount = 0;
            OnPropertyChanged(nameof(SelectedFacility));
            OnPropertyChanged(nameof(NewPrice));
            OnPropertyChanged(nameof(NewDiscount));
        }

        private void RemoveFacility(HotelFacilityDTO dto)
        {
            FacilityList.Remove(dto);
        }

        private void SaveFacilities(object o)
        {
            _facilityService.SaveFacilitiesForHotel(CurrentUser?.HotelId ?? 0, FacilityList.ToList());
        }
    }
}
