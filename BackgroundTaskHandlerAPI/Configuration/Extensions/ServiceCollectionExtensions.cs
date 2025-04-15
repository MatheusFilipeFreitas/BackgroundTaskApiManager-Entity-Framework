using BackgroundTaskHandlerAPI.Models.Configs;
using BackgroundTaskHandlerAPI.Services;
using BackgroundTaskHandlerAPI.Services.Implementation;
using BackgroundTaskHandlerAPI.Utils.Queue;
using BackgroundTaskHandlerAPI.Utils.Queue.Implementation;

namespace BackgroundTaskHandlerAPI.Configuration.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        services.AddHostedService<QueuedHostedService>();

        services.Configure<List<StartupTaskConfig>>(configuration.GetSection("StartupTasks"));

        var startupTaskConfigs = configuration
            .GetSection("StartupTasks")
            .Get<List<StartupTaskConfig>>() ?? [];

        services.AddSingleton<IEnumerable<StartupTaskConfig>>(startupTaskConfigs);
        services.AddSingleton<Dictionary<string, CancellationTokenSource>>();

        services.AddHostedService<ScheduledStartupTaskService>();

        return services;
    }

    public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddOpenApi();
        return services;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<ITasksService, TasksService>();
        return services;
    }
}