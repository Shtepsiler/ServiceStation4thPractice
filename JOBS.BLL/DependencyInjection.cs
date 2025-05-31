using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JOBS.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBuisnesLogicLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}