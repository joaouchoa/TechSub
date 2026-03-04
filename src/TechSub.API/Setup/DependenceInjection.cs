using TechSub.Application.ServiceCollection;
using TechSub.Infrastructure.ServiceCollection;

namespace TechSub.API.Setup
{
    public static class DependenceInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddInfrastructureServices();

            return services;
        }
    }
}
