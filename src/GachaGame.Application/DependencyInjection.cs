using Microsoft.Extensions.DependencyInjection;

namespace GachaGame.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // 注册应用层的服务
        // services.AddScoped<IYourService, YourService>();
        return services;
    }
}
