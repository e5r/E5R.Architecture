// Copyright (c) E5R.Architecture.Template.FullSolution. Todos os direitos reservados.
// Configure suas notas de cabeçalho no arquivo ".editorconfig" na raiz da solução.

using System.Threading.Tasks;

using E5R.Architecture.Business;
using E5R.Architecture.Core;
using E5R.Architecture.Template.FullSolution.Domain.Data.TransferObjects;

namespace E5R.Architecture.Template.FullSolution.Handlers
{
    /// <summary>
    /// Make a hello message with custom name option
    /// </summary>
    public class GetHelloMessageHandler : ActionHandler<GetHelloMessageInput, string>
    {
        private IRuleSet<GetHelloMessageInput> Rules { get; }

        public GetHelloMessageHandler(IRuleSet<GetHelloMessageInput> rules)
        {
            Checker.NotNullArgument(rules, nameof(rules));

            Rules = rules;
        }

        protected override async Task<string> ExecActionAsync(GetHelloMessageInput input)
        {
            await Rules.EnsureAsync(input);

            return $"Hello {input.Name}";
        }
    }
}
