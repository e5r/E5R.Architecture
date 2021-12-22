// Copyright (c) E5R.Architecture.Template.FullSolution. Todos os direitos reservados.
// Configure suas notas de cabeçalho no arquivo ".editorconfig" na raiz da solução.

using McMaster.Extensions.CommandLineUtils;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E5R.Architecture.Template.FullSolution.UserInterface.Tool.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMainInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddInfrastructure(config, options =>
            {
                options.UseDefaults();
#if DEBUG
                options.EnableDeveloperMode();
#endif
            });

            services.AddScoped<CommandLineApplication>(sp =>
            {
                CommandLineApplication<Program> app = new();

                app.Conventions.UseDefaultConventions().UseConstructorInjection(sp);

                return app;
            });

            return services;
        }
    }
}
