// Copyright (c) E5R.Architecture.Template.FullSolution. Todos os direitos reservados.
// Configure suas notas de cabeçalho no arquivo ".editorconfig" na raiz da solução.

using E5R.Architecture.Infrastructure.Abstractions;
using E5R.Architecture.Template.FullSolution.Handlers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E5R.Architecture.Template.FullSolution.UserInterface.Tool
{
    public class CrossCuttingRegistrar : ICrossCuttingRegistrar
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<GetHelloMessageHandler>();
            services.AddScoped<GetHelloWorldMessageHandler>();
        }
    }
}
