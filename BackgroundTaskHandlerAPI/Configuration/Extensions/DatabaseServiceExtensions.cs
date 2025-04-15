using System.Data;
using Microsoft.Data.SqlClient;

namespace BackgroundTaskHandlerAPI.Configuration.Extensions;

public static class DatabaseServiceExtensions
{
    public static IServiceCollection AddDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDbConnection>(sp =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        });

        return services;
    }
}