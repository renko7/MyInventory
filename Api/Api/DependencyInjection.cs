using MyInventory.Api.Models;
using MyInventory.Api.Services;

namespace MyInventory.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, WebApplicationBuilder builder) 
    {
        // builder.Configuration -> binds to IConfiguration. services.Configure -> binds to IOptions<AppSettings>
        services.Configure<AppSettings>(
            builder.Configuration.GetSection("Values"));

        services.AddScoped<IUploadImageService, UploadImageService>();

        return services;
    }
}
