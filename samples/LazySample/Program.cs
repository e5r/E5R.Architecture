using System;
using System.Linq;
using System.Threading.Tasks;
using E5R.Architecture.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LazySample
{
    public class OkInnerDependency
    {
        public string Message => "Ok inner dependency message!";
    }

    public class OkDependency
    {
        private ILazy<OkInnerDependency> Inner { get; set; }

        public OkDependency(ILazy<OkInnerDependency> inner)
        {
            Inner = inner;
        }

        public string GetMessage() => Inner.Value.Message;
    }
    
    public class FailInnerDependency
    {
        public string Message => "Fail inner dependency message!";
    }

    public class FailDependency
    {
        private ILazy<FailInnerDependency> Inner { get; set; }

        public FailDependency(ILazy<FailInnerDependency> inner)
        {
            Inner = inner;
        }

        public string GetMessage() => Inner.Value.Message;
    }
    
    public class Program
    {
        private string[] _args;
        private OkDependency OkDependency { get; }
        private FailDependency FailDependency { get; }

        public Program(OkDependency okDependency, FailDependency failDependency)
        {
            OkDependency = okDependency;
            FailDependency = failDependency;
        }

        private async Task Run()
        {
            if (_args?.Length != 1 || !new[] {"ok", "fail"}.Any(p =>
                p.Equals(_args?.First(), StringComparison.InvariantCultureIgnoreCase)))
            {
                await Console.Error.WriteLineAsync(
                    "You must enter a single parameter:\n  OK - Runs successfully\n  FAIL - Executes with failure");
            }

            var selectedOption = _args?.First();

            if (selectedOption != null &&
                selectedOption.Equals("ok", StringComparison.InvariantCultureIgnoreCase))
            {
                await Console.Out.WriteLineAsync(OkDependency.GetMessage());
                
                return;
            }
            
            if (selectedOption != null &&
                selectedOption.Equals("fail", StringComparison.InvariantCultureIgnoreCase))
            {
                await Console.Out.WriteLineAsync(FailDependency.GetMessage());
                return;
            }

            await Console.Error.WriteLineAsync("Unidentified error!");
        }
        
        private static async Task Main(string[] args)
        {
            var program = CreateProgramHost();

            program._args = args;

            await program.Run();
        }

        private static Program CreateProgramHost() => Configure(new ServiceCollection())
            .BuildServiceProvider().GetRequiredService<Program>();

        private static IServiceCollection Configure(IServiceCollection services)
        {
            services
                .AddInfrastructure(new ConfigurationBuilder().Build(), infra =>
                {
#if DEBUG
                    infra.EnableDeveloperMode();
#endif
                })
                .AddTransient<Program>()
                .AddTransient<OkDependency>()
                .AddTransient<OkInnerDependency>()
                .AddTransient<FailDependency>()
                //.AddTransient<FailInnerDependency>()
                ;

            return services;
        }
    }
}
