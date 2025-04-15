using BackgroundTaskHandlerAPI.Configuration.Initializer;

namespace BackgroundTaskHandlerAPI.Configuration.Extensions;

public static class InitializerServiceExtensions
{
    public static IServiceCollection AddInitializerService(this IServiceCollection services)
    {
        services.AddHostedService<ManualTaskAutomaticRegister>();
        return services;
    }
}