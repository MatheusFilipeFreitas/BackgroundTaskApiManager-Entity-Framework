using System.Reflection;
using BackgroundTaskHandlerAPI.Models;

namespace BackgroundTaskHandlerAPI.Configuration.Extensions;

public static class TaskServiceExtensions
{
    public static IServiceCollection RegisterTasks(this IServiceCollection services)
    {
        var manualTasks = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.Namespace == "BackgroundTaskHandlerAPI.Models.Tasks.ManualTasks"
                        && typeof(TaskBase).IsAssignableFrom(t)
                        && !t.IsInterface && !t.IsAbstract);
        var automaticTasks = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.Namespace == "BackgroundTaskHandlerAPI.Models.Tasks.StartupTasks"
                        && typeof(TaskBase).IsAssignableFrom(t)
                        && !t.IsInterface && !t.IsAbstract);
        var allTasks = manualTasks.Concat(automaticTasks);
        
        foreach (var type in allTasks)
        {
            services.AddScoped(type);
        }
        return services;
    }
}