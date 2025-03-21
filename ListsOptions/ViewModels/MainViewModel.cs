using ListsOptionsUI.Commands;
using ListsOptionsUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{

    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<TabItemViewModel> OpenTabs { get; set; } = new();
        public object SelectedView { get; set; }

        public ICommand OpenFacilityCommand { get; }
        public ICommand OpenPaymentMethodCommand { get; }
        public ICommand OpenRoomTypeCommand { get; }
        public ICommand OpenUserConfigurationCommand { get; }

        private UserViewModel userViewModel;

        public MainViewModel(FacilityViewModel facilityViewModel, PaymentMethodViewModel paymentMethodViewModel, RoomTypeViewModel roomTypeViewModel, UserDetailsViewModel userDetailsViewModel, UserViewModel userViewModel)
        {
            OpenFacilityCommand = new RelayCommand(o => OpenTab("Хотелски удобства", new FacilityView(facilityViewModel)));
            
            // Ако искам да бъде нова инстанция 
            //OpenFacilityCommand = new RelayCommand(o => OpenTab("Facility", new FacilityView(new FacilityViewModel(facilityViewModel._facilityService))));
            
            OpenPaymentMethodCommand = new RelayCommand(o => OpenTab("Платежни методи", new PaymentMethodView(paymentMethodViewModel)));
            OpenRoomTypeCommand = new RelayCommand(o => OpenTab("Типове стаи", new RoomTypeView(roomTypeViewModel)));
            OpenUserConfigurationCommand = new RelayCommand(o => OpenTab("Конфигурация на потребителите", new UserDetailsView(userDetailsViewModel)));
            this.userViewModel = userViewModel;
        }
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
    }
}
