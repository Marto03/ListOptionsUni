using DataLayer.Models;
using DataLayer.Services;
using ListsOptionsUI.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{
    public class FacilityViewModel : BaseViewModel
    {
        private readonly FacilityService _facilityService;
        private string _newFacilityName;
        
        public FacilityViewModel(FacilityService facilityService)
        {
            _facilityService = facilityService;
            Facilities = new ObservableCollection<FacilityModel>(_facilityService.GetAllFacilities());
            AddFacilityCommand = new RelayCommand(AddFacility);
        }

        public ObservableCollection<FacilityModel> Facilities { get; set; }
        public string NewFacilityName
        {
            get => _newFacilityName;
            set
            {
                _newFacilityName = value;
                OnPropertyChanged(nameof(NewFacilityName));
            }
        }

        public ICommand AddFacilityCommand { get; }


        private void AddFacility(object o)
        {
            if (!string.IsNullOrWhiteSpace(NewFacilityName))
            {
                _facilityService.AddFacility(NewFacilityName);
                Facilities.Add(new FacilityModel { Name = NewFacilityName , IsCustomAdded = true});
                NewFacilityName = "";
                //roomTypeService.SaveChanges();
            }
        }
    }
}
