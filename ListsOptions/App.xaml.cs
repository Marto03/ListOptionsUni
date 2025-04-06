using BusinessLayer.Services;
using DataLayer.Models;
using DataLayer.Repositories;
using DataLayer.Services;
using ListsOptionsUI.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ListsOptions;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        ServiceCollection services = new ServiceCollection();

        // Добавяне на DbContext (Базата данни)
        services.AddDbContext<HotelDbContextModel>(options =>
            options.UseSqlite("Data Source=hotel.db"));

        // Регистриране на сървиси от BusinessLayer
        services.AddScoped<FacilityService>();
        services.AddScoped<PaymentMethodService>();
        services.AddScoped<RoomTypeService>();
        services.AddScoped<UserService>();

        services.AddScoped<HotelFacilityService>();

        // Регистрираме Repository слоя
        services.AddScoped<IHotelRepository, HotelRepository>();
        //services.AddScoped<IReservationRepository, ReservationRepository>();
        //services.AddScoped<IPaymentRepository, PaymentRepository>();

        // Регистрираме Service слоя
        services.AddScoped<HotelService>();
        services.AddScoped<ReservationService>();
        services.AddScoped<PaymentService>();

        // Регистриране на ViewModel-и
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
        //services.AddScoped<PaymentMethodViewModel>();
        services.AddScoped<MainWindow>();

        ServiceProvider = services.BuildServiceProvider();
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

