using Castle.Core.Configuration;
using ImageProcessorFunction.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        s.AddOptions<AppSettings>()
            .Configure<IConfiguration>()
    })
    .Build();

host.Run();