using DatabaseConfig;
using ListsOptionsUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
namespace ListsOptions;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;
    public App()
    {
        ServiceCollection services = new ServiceCollection();
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddBusinessLayer("Data Source=hotel.db");
                // Регистрация на ViewModel-и
                services.AddScoped<BaseViewModel>();
                services.AddScoped<FacilityViewModel>();
                services.AddScoped<PaymentMethodViewModel>();
                services.AddScoped<RoomTypeViewModel>();
                services.AddScoped<UserViewModel>();
                services.AddScoped<UserDetailsViewModel>();
                services.AddScoped<MainViewModel>();

                services.AddScoped<HotelConfigurationViewModel>();
                services.AddScoped<HotelFacilityEditorViewModel>();
                services.AddScoped<ReservationViewModel>();
                services.AddScoped<ReservationsListViewModel>();
                services.AddScoped<HotelListByFacilityViewModel>();
                services.AddScoped<MainWindow>();



            })
            .Build();

        // Записване на глобалния контейнер
        ServiceProvider = _host.Services;
    }
    public static IServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>(); // DI за MainWindow
        mainWindow.DataContext = ServiceProvider.GetRequiredService<MainViewModel>(); // Свързване на MainViewModel
        mainWindow.Show();
        base.OnStartup(e);
    }
}

