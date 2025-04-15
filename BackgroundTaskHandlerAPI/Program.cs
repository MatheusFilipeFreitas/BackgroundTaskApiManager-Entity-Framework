using System.Text.Json.Serialization;
using BackgroundTaskHandlerAPI.Configuration.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocs();
builder.Services.RegisterMappers();
builder.Services.RegisterTasks();
builder.Services.RegisterServices();
builder.Services.AddDatabaseConnection(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInitializerService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapHealthChecks("/health");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();