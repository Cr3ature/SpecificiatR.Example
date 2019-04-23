using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpecificatR.Infrastructure.Configuration;

namespace SpecificationPattern.Demo.Infrastructure.Configuration
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString))
                .BuildServiceProvider();

            services.AddSpecificatR<ApplicationContext>();

            return services;
        }
    }
}
