// Copyright (c) E5R.Architecture.Template.FullSolution. Todos os direitos reservados.
// Configure suas notas de cabeçalho no arquivo ".editorconfig" na raiz da solução.

using System.Threading.Tasks;

using E5R.Architecture.Business;

namespace E5R.Architecture.Template.FullSolution.Handlers
{
    public class GetHelloWorldMessageHandler : OutputOnlyActionHandler<string>
    {
        protected override Task<string> ExecActionAsync()
        {
            return Task.FromResult("Hello world message from handler");
        }
    }
}
