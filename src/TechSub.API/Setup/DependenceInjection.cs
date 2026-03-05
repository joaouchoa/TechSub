using TechSub.Application.ServiceCollection;
using TechSub.Infrastructure.ServiceCollection;

namespace TechSub.API.Setup
{
    public static class DependenceInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationServices();
            services.AddInfrastructureServices(configuration);

            return services;
        }
    }
}
