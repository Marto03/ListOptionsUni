using HotelApp.Core.Interfaces;
using HotelApp.Core.Models;
using ListsOptions;
using ListsOptionsUI.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{
    public class HotelListByFacilityViewModel : BaseViewModel
    {
        private IHotelFacilityService hotelFacilityService;
        private IFacilityService facilityService;
        public HotelListByFacilityViewModel(IHotelFacilityService hotelFacilityService, IFacilityService facilityService) : base(App.ServiceProvider.GetRequiredService<IUserSessionService>())
        {
            this.hotelFacilityService = hotelFacilityService;
            this.facilityService = facilityService; 
            Facilities = new ObservableCollection<FacilityModel>(this.facilityService.GetAllFacilities());
            LoadFacilities();

            ShowHotelsCommand = new RelayCommand(ShowHotelsWithFacility, _ => SelectedFacility != null);

        }
        public FacilityModel SelectedFacility { get; set; }
        public ObservableCollection<FacilityModel> Facilities { get; set; }

        public ObservableCollection<HotelModel> HotelsWithSelectedFacility { get; set; } = new();

        public ICommand ShowHotelsCommand { get; }

        private void ShowHotelsWithFacility(object obj)
        {
            if (SelectedFacility == null) return;

            var hotels = hotelFacilityService.GetHotelsByFacilityId(SelectedFacility.Id);
            HotelsWithSelectedFacility.Clear();
            foreach (var h in hotels)
            {
                HotelsWithSelectedFacility.Add(h);
            }
            OnPropertyChanged(nameof(HotelsWithSelectedFacility));
        }
        private void LoadFacilities()
        {
            var available = facilityService.GetAllFacilities();
            Facilities.Clear();
            foreach (var f in available)
                Facilities.Add(f);
            OnPropertyChanged(nameof(Facilities));
        }
    }
}
