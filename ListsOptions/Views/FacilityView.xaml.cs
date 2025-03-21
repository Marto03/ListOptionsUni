using ListsOptionsUI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ListsOptionsUI.Views
{
    /// <summary>
    /// Interaction logic for FacilityView.xaml
    /// </summary>
    public partial class FacilityView : UserControl
    {
        public FacilityView(FacilityViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
