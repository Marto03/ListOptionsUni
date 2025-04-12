//using HotelApp.Common.ServiceConfiguration;
//using HotelApp.Data.Repositories;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;

//namespace HotelApp.Data
//{
//    public class DataConfig : IAppConfigurator
//    {
//        public void Configure(IServiceCollection services)
//        {
//            // Добавяне на DbContext (Базата данни)
//            services.AddDbContext<DatabaseContext>(options =>
//                options.UseSqlite("Data Source=hotel.db"));

//        }
//    }
//}
