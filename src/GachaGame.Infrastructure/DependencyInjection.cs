using Microsoft.Extensions.DependencyInjection;

namespace GachaGame.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // ע�������ʩ��ķ���
        // services.AddScoped<IYourRepository, YourRepository>();
        return services;
    }
}
