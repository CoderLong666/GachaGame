using Microsoft.Extensions.DependencyInjection;

namespace GachaGame.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // 注册基础设施层的服务
        // services.AddScoped<IYourRepository, YourRepository>();
        return services;
    }
}
