using ImageProcessorFunction.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        s.AddOptions<AppSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.Bind(settings);
            });

        var appSettings = s.BuildServiceProvider().GetService<IOptions<AppSettings>>().Value;

        //s.AddDbContext<>(options => options.UseSqlServer(appSettings.DatabaseConnectionString));
    })
    .Build();

host.Run();