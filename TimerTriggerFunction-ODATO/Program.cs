using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using TimerTriggerFunction_ODATO.Context;

namespace TimerTriggerFunction_ODATO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FunctionsDebugger.Enable();

            var host = new HostBuilder()
                 .ConfigureAppConfiguration((context, config) =>
                 {
                     config.SetBasePath(Directory.GetCurrentDirectory());
                     config.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
                     config.AddEnvironmentVariables();
                 })
                .ConfigureFunctionsWorkerDefaults(builder =>
                {
                    var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();

                    builder.Services.AddDbContext<MyDBContext>(options =>
                        options.UseSqlServer("Data Source=LAB7-PC20\\ANILAB3DPC20;Initial Catalog=Odato_ProjectDB;Integrated Security=True;Trust Server Certificate=True;",
                            sql => sql.EnableRetryOnFailure())
                        .EnableSensitiveDataLogging(),
                        ServiceLifetime.Transient
                    );
                })
                .Build();

            host.Run();
        }
    }
}
