using HotelApp.BusinessLayer.Services;
using HotelApp.BusinessLayer.ServicesReusability;
using HotelApp.Core.Interfaces;
using HotelApp.Core.InterfacesReusability;
using HotelApp.Core.Repositories;
using HotelApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseConfig
{
    public static class DatabaseConfigure
    {
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
            services.AddScoped<ICarRentalService, CarRentalService>();

            //services.AddScoped<IHotelFacilityEditorViewModel, HotelFacilityEditorViewModel>();


            return services;
        }
    }
}
