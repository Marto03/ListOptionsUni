using ListsOptionsUI.ViewModels;
using System.Windows.Controls;

namespace ListsOptionsUI.Views
{
    /// <summary>
    /// Interaction logic for ReservationView.xaml
    /// </summary>
    public partial class ReservationView : UserControl
    {
        public ReservationView(ReservationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
