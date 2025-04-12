using Microsoft.Extensions.DependencyInjection;

namespace HotelApp.Common.ServiceConfiguration
{
    public interface IAppConfigurator
    {
        void Configure(IServiceCollection services);
    }

}
