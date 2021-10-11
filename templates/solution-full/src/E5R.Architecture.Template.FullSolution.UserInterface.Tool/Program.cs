using System.Threading.Tasks;

using E5R.Architecture.Template.FullSolution.UserInterface.Tool.Extensions;

using McMaster.Extensions.CommandLineUtils;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace E5R.Architecture.Template.FullSolution.UserInterface.Tool
{
    internal partial class Program
    {
        private static async Task<int> Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(b => b
                    .ClearProviders()
                    .SetMinimumLevel(LogLevel.Warning)
                    .AddConsole())
                .ConfigureAppConfiguration((h, b) =>
                {
                    if (h.HostingEnvironment.IsDevelopment())
                    {
                        b.AddUserSecrets<Program>();
                    }
                })
                .ConfigureServices((b, s) => s.AddMainInfrastructure(b.Configuration))
                .Build();

            await host.StartAsync();

            using IServiceScope scope = host.Services.CreateScope();

            int exitCode = await scope.ServiceProvider
                .GetRequiredService<CommandLineApplication>()
                .ExecuteAsync(args);

            await host.StopAsync();

            return exitCode;
        }
    }
}
