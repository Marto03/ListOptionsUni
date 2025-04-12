//using HotelApp.BusinessLayer.Services;
//using HotelApp.Common.ServiceConfiguration;
//using ListsOptions;
//using ListsOptionsUI.ViewModels;
//using Microsoft.Extensions.DependencyInjection;

//namespace ListsOptionsUI
//{
//    public class UIConfig : IAppConfigurator
//    {
//        public void Configure(IServiceCollection services)
//        {
            


//            // Регистриране на ViewModel-и

//            // Регистриране на сървиси от BusinessLayer
//            services.AddScoped<FacilityService>();
//            services.AddScoped<PaymentMethodService>();
//            services.AddScoped<RoomTypeService>();
//            services.AddScoped<UserService>();

//            services.AddScoped<HotelFacilityService>();

//            // Регистрираме Repository слоя
//            //services.AddScoped<IHotelRepository, HotelRepository>();
//            //services.AddScoped<IReservationRepository, ReservationRepository>();
//            //services.AddScoped<IPaymentRepository, PaymentRepository>();

//            // Регистрираме Service слоя
//            services.AddScoped<HotelService>();
//            services.AddScoped<ReservationService>();

//            // Регистриране на ViewModel-и
//            services.AddScoped<FacilityViewModel>();
//            services.AddScoped<PaymentMethodViewModel>();
//            services.AddScoped<RoomTypeViewModel>();
//            services.AddScoped<UserViewModel>();
//            services.AddScoped<UserDetailsViewModel>();
//            services.AddScoped<MainViewModel>();

//            services.AddScoped<HotelConfigurationViewModel>();
//            services.AddScoped<HotelFacilityEditorViewModel>();
//            services.AddScoped<ReservationViewModel>();
//            services.AddScoped<ReservationsListViewModel>();
//            //services.AddScoped<PaymentMethodViewModel>();
//            services.AddScoped<MainWindow>();
//        }
//    }
//}
