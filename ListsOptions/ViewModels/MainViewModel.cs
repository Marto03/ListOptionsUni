using BusinessLayer.Services;
using ListsOptions;
using ListsOptionsUI.Commands;
using ListsOptionsUI.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{

    public class MainViewModel : BaseViewModel
    {
        #region fields
        private UserViewModel userViewModel;
        #endregion
        #region Constructor
        public MainViewModel(FacilityViewModel facilityViewModel, PaymentMethodViewModel paymentMethodViewModel, 
            RoomTypeViewModel roomTypeViewModel, UserDetailsViewModel userDetailsViewModel, UserViewModel userViewModel)
        {
            OpenFacilityCommand = new RelayCommand(o => OpenTab("Хотелски удобства", new FacilityView(facilityViewModel)));

            //OpenFacilityCommand = new RelayCommand(o =>
            //{
            //    var facilityView = new FacilityView();
            //    facilityView.DataContext = facilityViewModel;  // Задаваме DataContext тук
            //    OpenTab("Хотелски удобства", facilityView);
            //});

            // Ако искам да бъде нова инстанция 
            //OpenFacilityCommand = new RelayCommand(o => OpenTab("Facility", new FacilityView(new FacilityViewModel(facilityViewModel.facilityService))));
            
            OpenPaymentMethodCommand = new RelayCommand(o => OpenTab("Платежни методи", new PaymentMethodView(paymentMethodViewModel)));
            OpenRoomTypeCommand = new RelayCommand(o => OpenTab("Типове стаи", new RoomTypeView(roomTypeViewModel)));
            OpenUserConfigurationCommand = new RelayCommand(o => OpenTab("Конфигурация на потребители", new UserDetailsView(userDetailsViewModel)));
            OpenHotelConfigurationCommand = new RelayCommand(o => OpenTab("Конфигурация на хотели", new HotelConfigurationView(App.ServiceProvider.GetRequiredService<HotelConfigurationViewModel>())));
            OpenReservationCreatingCommand = new RelayCommand(o => OpenTab("Създаване на резервация", new ReservationView(App.ServiceProvider.GetRequiredService<ReservationViewModel>())));
            OpenReservationsListCommand = new RelayCommand(o => OpenTab("Преглед на резервации", new ReservationsListView(App.ServiceProvider.GetRequiredService<ReservationsListViewModel>())));
            this.userViewModel = userViewModel;
        }
        #endregion
        #region Properties
        public ObservableCollection<TabItemViewModel> OpenTabs { get; set; } = new();
        public object SelectedView { get; set; }

        public ICommand OpenFacilityCommand { get; }
        public ICommand OpenPaymentMethodCommand { get; }
        public ICommand OpenRoomTypeCommand { get; }
        public ICommand OpenUserConfigurationCommand { get; }
        public ICommand OpenHotelConfigurationCommand { get; }
        public ICommand OpenReservationCreatingCommand { get; }
        public ICommand OpenReservationsListCommand { get; }

        public UserViewModel UserViewModel
        {
            get
            {
                return userViewModel;
            }
            set
            {
                if (userViewModel != value)
                    userViewModel = value;
                OnPropertyChanged(nameof(UserViewModel));
            }
        }
        #endregion
        #region Methods
        private void OpenTab(string title, object view)
        {
            var existingTab = OpenTabs.FirstOrDefault(t => t.Title == title);
            if (existingTab != null)
            {
                SelectTab(existingTab);
                return;
            }

            var newTab = new TabItemViewModel(title, view, CloseTab, SelectTab);
            OpenTabs.Add(newTab);

            SelectTab(newTab);
        }
        private void SelectTab(TabItemViewModel tab)
        {
            foreach (var t in OpenTabs)
            {
                t.IsActive = false;
            }

            tab.IsActive = true;
            SelectedView = tab.View;

            OnPropertyChanged(nameof(SelectedView));
            OnPropertyChanged(nameof(OpenTabs));
        }

        private void CloseTab(TabItemViewModel tab)
        {
            OpenTabs.Remove(tab);

            // Ако затвореният таб е активния, преминаваме към следващия
            if (SelectedView == tab.View)
            {
                SelectedView = OpenTabs?.LastOrDefault()?.View;
                if (SelectedView != null)
                {
                    var activeTab = OpenTabs?.LastOrDefault(t => t.View == SelectedView);
                    if (activeTab != null) activeTab.IsActive = true;
                }
            }

            OnPropertyChanged(nameof(SelectedView));
        }
        #endregion
    }
}
