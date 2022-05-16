using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Dal.Migrations.MsSql.Extensions  
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMySqlDbSpecificProvider(this IServiceCollection services)
        {
            services.AddScoped<IDbSpecificConfigurationProvider, MsSqlConfigurationProvider>();
            return services;
        }
    }
}
