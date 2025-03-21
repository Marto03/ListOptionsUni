using ListsOptionsUI.ViewModels;
using System.Windows.Controls;

namespace ListsOptionsUI.Views
{
    /// <summary>
    /// Interaction logic for PaymentMethodView.xaml
    /// </summary>
    public partial class PaymentMethodView : UserControl
    {
        public PaymentMethodView(PaymentMethodViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
