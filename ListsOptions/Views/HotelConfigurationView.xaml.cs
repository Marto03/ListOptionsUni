using ListsOptionsUI.ViewModels;
using System.Windows.Controls;

namespace ListsOptionsUI.Views
{
    /// <summary>
    /// Interaction logic for HotelConfigurationView.xaml
    /// </summary>
    public partial class HotelConfigurationView : UserControl
    {
        public HotelConfigurationView(HotelConfigurationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
