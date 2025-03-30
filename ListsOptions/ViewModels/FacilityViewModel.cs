using DataLayer.Models;
using DataLayer.Services;
using ListsOptionsUI.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{
    public class FacilityViewModel : BaseViewModel
    {
        #region fields
        private readonly FacilityService facilityService;
        private string newFacilityName;
        #endregion
        #region Constructor
        public FacilityViewModel(FacilityService facilityService)
        {
            this.facilityService = facilityService;
            Facilities = new ObservableCollection<FacilityModel>(this.facilityService.GetAllFacilities());
            AddFacilityCommand = new RelayCommand(_ => AddFacility(_), _ => CurrentUser?.Type == UserTypeEnum.Admin);
            DeleteFacilityCommand = new RelayCommand(DeleteFacility, _ => _ is FacilityModel selectedFacilityModel && selectedFacilityModel?.IsCustomAdded == true && CurrentUser?.Type == UserTypeEnum.Admin);
        }
        #endregion
        #region Properties
        public ObservableCollection<FacilityModel> Facilities { get; set; }
        public ICommand AddFacilityCommand { get; }
        public ICommand DeleteFacilityCommand { get; }
        public string NewFacilityName
        {
            get => newFacilityName;
            set
            {
                newFacilityName = value;
                OnPropertyChanged(nameof(NewFacilityName));
            }
        }
        #endregion
        #region Methods
        private void AddFacility(object o)
        {
            if (!string.IsNullOrWhiteSpace(NewFacilityName))
            {
                try
                {
                    var newFacility = facilityService.AddFacility(NewFacilityName); // Взимаме обекта от базата
                    if (newFacility != null)
                    {
                        Facilities.Add(newFacility); // Добавяме реалния обект с валидно Id
                        NewFacilityName = "";
                        OnPropertyChanged(nameof(Facilities));
                    }
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Хотелското удобство вече съществува!", "Невалидно удобство", MessageBoxButton.OK);
                }
            }
        }

        private void DeleteFacility(object parameter)
        {
            if (parameter is FacilityModel facility)
            {
                facilityService.RemoveFacility(facility);
                Facilities.Remove(facility);
                OnPropertyChanged(nameof(Facilities));
            }
        }
        #endregion
    }
}
