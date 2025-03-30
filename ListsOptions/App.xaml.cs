using BusinessLayer.Services;
using DataLayer.Models;
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
    private readonly ServiceProvider _serviceProvider;

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

        // Регистриране на ViewModel-и
        services.AddScoped<FacilityViewModel>();
        services.AddScoped<PaymentMethodViewModel>();
        services.AddScoped<RoomTypeViewModel>();
        services.AddScoped<UserViewModel>();
        services.AddScoped<UserDetailsViewModel>();
        services.AddScoped<MainViewModel>();
        //services.AddScoped<PaymentMethodViewModel>();
        services.AddScoped<MainWindow>();

        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>(); // DI за MainWindow
        mainWindow.DataContext = _serviceProvider.GetRequiredService<MainViewModel>(); // Свързване на MainViewModel
        mainWindow.Show();
        base.OnStartup(e);
    }
}

