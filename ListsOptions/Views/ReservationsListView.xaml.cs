using ListsOptionsUI.ViewModels;
using System.Windows.Controls;

namespace ListsOptionsUI.Views
{
    /// <summary>
    /// Interaction logic for ReservationsListView.xaml
    /// </summary>
    public partial class ReservationsListView : UserControl
    {
        public ReservationsListView(ReservationsListViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
