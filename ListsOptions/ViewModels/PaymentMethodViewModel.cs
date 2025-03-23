using DataLayer.Models;
using DataLayer.Services;
using ListsOptionsUI.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ListsOptionsUI.ViewModels
{
    public class PaymentMethodViewModel : BaseViewModel
    {
        private readonly PaymentMethodService paymentMethodService;
        private string newPaymentMethod;

        public PaymentMethodViewModel(PaymentMethodService paymentMethodService)
        {
            this.paymentMethodService = paymentMethodService;
            PaymentMethods = new ObservableCollection<PaymentMethodModel>(this.paymentMethodService.GetPaymentMethods());
            AddPaymentMethodCommand = new RelayCommand(AddPaymentMethod);
        }

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

        public ICommand AddPaymentMethodCommand { get; }


        private void AddPaymentMethod(object o)
        {
            if (!string.IsNullOrWhiteSpace(NewPaymentMethod) && !PaymentMethods.Any(p=>p.Name.ToLower().Contains(NewPaymentMethod.ToLower())))
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

                //roomTypeService.SaveChanges();
            }
        }
    }
}
