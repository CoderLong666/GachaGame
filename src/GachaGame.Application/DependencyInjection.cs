using Microsoft.Extensions.DependencyInjection;

namespace GachaGame.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // ע��Ӧ�ò�ķ���
        // services.AddScoped<IYourService, YourService>();
        return services;
    }
}
