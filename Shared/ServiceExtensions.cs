using Application.interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.services;

namespace Shared
{
    public  static class ServiceExtensions
    {
        public static void AddSharedInfraestructure(this IServiceCollection services, IConfiguration configuration  )
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
        }
    }
}
