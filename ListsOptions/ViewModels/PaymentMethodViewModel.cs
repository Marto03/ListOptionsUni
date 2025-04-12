using HotelApp.BusinessLayer.Services;
using HotelApp.Core.Interfaces;
using HotelApp.Core.Models;
using ListsOptions;
using ListsOptionsUI.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{
    public class PaymentMethodViewModel : BaseViewModel
    {
        #region fields
        private readonly IPaymentMethodService paymentMethodService;
        private string newPaymentMethod;
        #endregion
        #region Constructor
        public PaymentMethodViewModel(IPaymentMethodService paymentMethodService, IUserSessionService userSessionService) : base(userSessionService)
        {
            this.paymentMethodService = paymentMethodService;
            PaymentMethods = new ObservableCollection<PaymentMethodModel>(this.paymentMethodService.GetPaymentMethods());
            AddPaymentMethodCommand = new RelayCommand(AddPaymentMethod);
        }
        #endregion
        #region Properties
        public ICommand AddPaymentMethodCommand { get; }
        public ObservableCollection<PaymentMethodModel> PaymentMethods { get; set; }
        public string NewPaymentMethod
        {
            get => newPaymentMethod;
            set
            {
                newPaymentMethod = value;
                OnPropertyChanged(nameof(NewPaymentMethod));
            }
        }

        #endregion
        #region Methods
        private void AddPaymentMethod(object o)
        {
            if (!string.IsNullOrWhiteSpace(NewPaymentMethod))
            {
                try
                {
                    paymentMethodService.AddPaymentMethod(NewPaymentMethod);
                    PaymentMethods.Add(new PaymentMethodModel { Name = NewPaymentMethod, IsSystemDefined = true });
                    NewPaymentMethod = "";
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Платежният метод вече съществува!", "Невалиден метод", MessageBoxButton.OK);
                }

            }
        }
        #endregion
    }
}
