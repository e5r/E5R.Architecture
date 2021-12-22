// Copyright (c) E5R.Architecture.Template.FullSolution. Todos os direitos reservados.
// Configure suas notas de cabeçalho no arquivo ".editorconfig" na raiz da solução.

using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

using E5R.Architecture.Core;

using McMaster.Extensions.CommandLineUtils;

namespace E5R.Architecture.Template.FullSolution.UserInterface.Tool.Commands.ShowSubcommands
{
    [Command(Name = CommandName, Description = CommandDescription)]
    public class ShowHelloSubcommand
    {
        private const string CommandName = "hello";
        private const string CommandDescription = "Show hello message";

        private TextWriter Out { get; }

        public ShowHelloSubcommand(IConsole console)
        {
            Checker.NotNullArgument(console, nameof(console));
            Checker.NotNullArgument(console.Out, () => console.Out);

            Out = console.Out;
        }

        [Required]
        [Option("-n|--name <NAME>", Description = "Name to showing in hello message")]
        public string Name { get; }

        private async Task OnExecuteAsync()
        {
            await Out.WriteLineAsync($"Hello {Name}!");
        }
    }
}
