using E5R.Architecture.Template.FullSolution.UserInterface.Tool.Commands;

using McMaster.Extensions.CommandLineUtils;

namespace E5R.Architecture.Template.FullSolution.UserInterface.Tool
{
    [Command(Name = ProgramName, Description = ProgramDescription)]
    [Subcommand(typeof(HelloWorldCommand))]
    [Subcommand(typeof(ShowCommand))]
    [Subcommand(typeof(ExecCommand))]
    internal partial class Program
    {
        private const string ProgramName = "my-tool";
        private const string ProgramDescription = "Tool application sample";

        internal void OnExecute(CommandLineApplication app) => app.ShowHelp();
    }
}
