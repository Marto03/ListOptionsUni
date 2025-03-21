using ListsOptionsUI.ViewModels;
using System.Windows.Controls;

namespace ListsOptionsUI.Views
{
    /// <summary>
    /// Interaction logic for RoomTypeView.xaml
    /// </summary>
    public partial class RoomTypeView : UserControl
    {
        public RoomTypeView(RoomTypeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
