using CSharpParallelThreadApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        // Resolve the Worker service and call the Run method
        var worker = host.Services.GetRequiredService<Worker>();
        worker.Run().Wait();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
          .ConfigureAppConfiguration((hostingContext, config) =>
          {
              //var env = hostingContext.HostingEnvironment;
              //Console.WriteLine($"Current Environment: {env.EnvironmentName}");
              config.AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
              config.AddJsonFile($"appSettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: false, reloadOnChange: true);
          })
        .ConfigureServices((hostContext, services) =>
        {
            // Register the Worker class as a service
            services.AddTransient<Worker>();
        });
}
