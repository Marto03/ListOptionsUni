using HotelApp.BusinessLayer.Services;
using HotelApp.Core.Interfaces;
using HotelApp.Core.Repositories;
using HotelApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseConfig
{
    public static class DatabaseConfigure
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //services.AddScoped<IAppConfigurator>();
            //var serviceProvider = services.BuildServiceProvider();
            //serviceProvider.GetRequiredService<IAppConfigurator>().Configure(services);


            // Добавяне на DbContext (Базата данни)
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlite("Data Source=hotel.db"));

            // Регистрираме всички имплементации на IAppConfigurator
            //services.AddScoped<IAppConfigurator, DataConfig>();

            //// Изграждане на service provider
            //var serviceProvider = services.BuildServiceProvider();

            //// Извличаме всички конфигурации и ги изпълняваме
            //var configurators = serviceProvider.GetServices<IAppConfigurator>();
            //foreach (var configurator in configurators)
            //{
            //    configurator.Configure(services);
            //}
        }
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlite(connectionString));

            services.AddSingleton<IUserSessionService, UserSessionService>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // Регистрираме Service слоя
            services.AddScoped<IFacilityService, FacilityService>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IHotelFacilityService, HotelFacilityService>();

            //services.AddScoped<IHotelFacilityEditorViewModel, HotelFacilityEditorViewModel>();


            return services;
        }
    }
}
