// Copyright (c) E5R.Architecture.Template.FullSolution. Todos os direitos reservados.
// Configure suas notas de cabeçalho no arquivo ".editorconfig" na raiz da solução.

using System.IO;
using System.Threading.Tasks;

using E5R.Architecture.Core;

using McMaster.Extensions.CommandLineUtils;

namespace E5R.Architecture.Template.FullSolution.UserInterface.Tool.Commands
{
    [Command(Name = CommandName, Description = CommandDescription)]
    public class HelloWorldCommand
    {
        private const string CommandName = "hello-world";
        private const string CommandDescription = "Show hello world message";

        private TextWriter Out { get; }

        public HelloWorldCommand(IConsole console)
        {
            Checker.NotNullArgument(console, nameof(console));
            Checker.NotNullArgument(console.Out, () => console.Out);

            Out = console.Out;
        }

        private async Task OnExecuteAsync()
        {
            await Out.WriteLineAsync("Hello world!");
        }
    }
}
