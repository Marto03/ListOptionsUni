using BusinessLayer.Services;
using Common.DTOs;
using DataLayer.Models;
using DataLayer.Services;
using ListsOptionsUI.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{
    public class HotelFacilityEditorViewModel : BaseViewModel
    {
        private readonly HotelFacilityService _facilityService;
        private readonly FacilityService _facilityListService;

        public ObservableCollection<FacilityModel> AvailableFacilities { get; set; } = new();
        public ObservableCollection<HotelFacilityDTO> FacilityList { get; set; } = new();

        public FacilityModel SelectedFacility { get; set; }
        public decimal NewPrice { get; set; }
        public double NewDiscount { get; set; }

        public string CurrentHotel { get; set; }

        public ICommand AddFacilityCommand { get; }
        public ICommand RemoveFacilityCommand { get; }
        public ICommand SaveCommand { get; }

        public HotelFacilityEditorViewModel(HotelFacilityService service, FacilityService facilityListService)
        {
            _facilityService = service;
            _facilityListService = facilityListService;

            AddFacilityCommand = new RelayCommand(AddFacility);
            RemoveFacilityCommand = new RelayCommand<HotelFacilityDTO>(RemoveFacility);
            SaveCommand = new RelayCommand(SaveFacilities);

            LoadFacilities();
            UserSessionService.Instance.CurrentUserChanged += OnCurrentUserChanged;
            Events.AppEvents.UsersChanged += () => LoadFacilities();
        }

        private void OnCurrentUserChanged(UserModel? user)
        {
            LoadFacilities();
        }
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

        private void AddFacility(object o)
        {
            if (SelectedFacility == null || FacilityList.Any(r=>r.FacilityId == SelectedFacility.Id) || NewPrice == 0) return;

            FacilityList.Add(new HotelFacilityDTO
            {
                FacilityId = SelectedFacility.Id,
                FacilityName = SelectedFacility.Name,
                Price = NewPrice,
                Discount = NewDiscount
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
