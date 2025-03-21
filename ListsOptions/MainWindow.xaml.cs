using ListsOptionsUI.ViewModels;
using ListsOptionsUI.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ListsOptions;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //private readonly ServiceProvider _serviceProvider;

    //public MainWindow()
    //{
    //    InitializeComponent();

    //    ServiceCollection services = new ServiceCollection();
    //    // Регистриране на сървиси и ViewModel-и
    //    services.AddScoped<FacilityViewModel>();
    //    services.AddScoped<FacilityViewModel>();

    //    // Използване на DI контейнер
    //    _serviceProvider = services.BuildServiceProvider();

    //    // Настройване на DataContext за всеки изглед (ViewModel)
    //    this.DataContext = this;

    //    // Зареждане на изгледите (ViewModels)

    //    services.AddScoped<FacilityViewModel>();
    //    services.AddScoped<FacilityViewModel>();

    //    FacilityView = _serviceProvider.GetRequiredService<FacilityViewModel>();
    //    PaymentMethodView = _serviceProvider.GetRequiredService<FacilityViewModel>();
    //}

    //public FacilityView FacilityView => new FacilityView { DataContext = paymentMethodViewModel };
    //public FacilityView PaymentMethodView => new FacilityView { DataContext = _paymentMethodViewModel };

    //private readonly FacilityViewModel paymentMethodViewModel;
    //private readonly FacilityViewModel _paymentMethodViewModel;

    //// Конструктор с инжектиране на зависимости
    //public MainWindow(FacilityViewModel facilityViewModel, FacilityViewModel paymentMethodViewModel)
    //{
    //    InitializeComponent();

    //    // Съхраняваме инжектираните зависимости
    //    paymentMethodViewModel = facilityViewModel;
    //    _paymentMethodViewModel = paymentMethodViewModel;

    //    // Задаваме DataContext на главния прозорец
    //    this.DataContext = paymentMethodViewModel; // Или каквото ViewModel искате да показвате първоначално
    //}
    //public FacilityViewModel FacilityViewModel { get; set; }
    //public FacilityViewModel PaymentMethodViewModel { get; set; }

    public MainWindow(FacilityViewModel facilityViewModel, FacilityViewModel paymentMethodViewModel)
    {
        InitializeComponent();

        // Задаваме инжектираните ViewModel-и на публични свойства
        //FacilityViewModel = facilityViewModel;
        //PaymentMethodViewModel = paymentMethodViewModel;

        //// Задаваме DataContext на MainWindow за да може да се използват PropertyBinding-ите
        //this.DataContext = this;
    }
}