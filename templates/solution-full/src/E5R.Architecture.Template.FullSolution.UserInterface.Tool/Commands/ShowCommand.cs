// Copyright (c) E5R.Architecture.Template.FullSolution. Todos os direitos reservados.
// Configure suas notas de cabeçalho no arquivo ".editorconfig" na raiz da solução.

using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

using E5R.Architecture.Core;
using E5R.Architecture.Template.FullSolution.UserInterface.Tool.Commands.ShowSubcommands;

using McMaster.Extensions.CommandLineUtils;

namespace E5R.Architecture.Template.FullSolution.UserInterface.Tool.Commands
{
    [Command(Name = CommandName, Description = CommandDescription)]
    [Subcommand(typeof(ShowHelloSubcommand))]
    [Subcommand(typeof(ShowHelloWorldSubcommand))]
    public class ShowCommand
    {
        private const string CommandName = "show";
        private const string CommandDescription = "Shows various messages, including custom message";

        private TextWriter Out { get; }

        public ShowCommand(IConsole console)
        {
            Checker.NotNullArgument(console, nameof(console));
            Checker.NotNullArgument(console.Out, () => console.Out);

            Out = console.Out;
        }

        [Option("-m|--message <MESSAGE>", Description = "You custom message")]
        public string Message { get; }

        private async Task OnExecuteAsync(CommandLineApplication app)
        {
            if (string.IsNullOrWhiteSpace(Message))
            {
                app.ShowHelp();

                return;
            }

            await Out.WriteLineAsync(Message);
        }
    }
}
