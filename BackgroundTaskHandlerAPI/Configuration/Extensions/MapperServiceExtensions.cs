using BackgroundTaskHandlerAPI.Utils.Mapper;
using BackgroundTaskHandlerAPI.Utils.Mapper.Implementation;

namespace BackgroundTaskHandlerAPI.Configuration.Extensions;

public static class MapperServiceExtensions
{ 
    public static IServiceCollection RegisterMappers(this IServiceCollection services)
    {
        services.AddSingleton<ITaskMapper, TaskMapper>();
        return services;
    }
}