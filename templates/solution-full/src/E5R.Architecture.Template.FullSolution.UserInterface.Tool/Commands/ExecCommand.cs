// Copyright (c) E5R.Architecture.Template.FullSolution. Todos os direitos reservados.
// Configure suas notas de cabeçalho no arquivo ".editorconfig" na raiz da solução.

using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

using E5R.Architecture.Core;
using E5R.Architecture.Core.Utils;
using E5R.Architecture.Template.FullSolution.Domain.Data.TransferObjects;
using E5R.Architecture.Template.FullSolution.Handlers;
using E5R.Architecture.Template.FullSolution.UserInterface.Tool.Enums;

using McMaster.Extensions.CommandLineUtils;

using static E5R.Architecture.Core.MetaTagAttribute;

namespace E5R.Architecture.Template.FullSolution.UserInterface.Tool.Commands
{
    [Command(Name = CommandName, Description = CommandDescription)]
    public class ExecCommand
    {
        private const string CommandName = "exec";
        private const string CommandDescription = "Exec action handler";

        private TextWriter Out { get; }
        private ILazy<GetHelloMessageHandler> GetHelloMessageHandler { get; }
        private ILazy<GetHelloWorldMessageHandler> GetHelloWorldMessageHandler { get; }

        public ExecCommand(IConsole console, ILazy<GetHelloMessageHandler> getHelloMessageHandler,
            ILazy<GetHelloWorldMessageHandler> getHelloWorldMessageHandler)
        {
            Checker.NotNullArgument(console, nameof(console));
            Checker.NotNullArgument(console.Out, () => console.Out);
            Checker.NotNullArgument(getHelloMessageHandler, nameof(getHelloMessageHandler));
            Checker.NotNullArgument(getHelloWorldMessageHandler, nameof(getHelloWorldMessageHandler));

            Out = console.Out;
            GetHelloMessageHandler = getHelloMessageHandler;
            GetHelloWorldMessageHandler = getHelloWorldMessageHandler;
        }

        [Option("-n|--name <NAME>", Description = "Name to hello message")]
        public string Name { get; }

        [Required]
        [Option("-H|--handler <HANDLER>", Description = "Handler name")]
        [AllowedValues("get-hello-message", "get-hello-world-message", "invalid-handler")]
        public string HandlerName { get; }

        private async Task OnExecuteAsync(CommandLineApplication app)
        {
            if (!EnumUtil.TryFromTag<ExecHandlerCommandType>(CustomIdKey, HandlerName, out var execHandlerCommand))
            {
                throw new Exception("Invalid handler name!");
            }

            switch (execHandlerCommand)
            {
                case ExecHandlerCommandType.GetHelloMessage:
                {
                    var message =
                        await GetHelloMessageHandler.Value.ExecAsync(new GetHelloMessageInput
                        {
                            Name = Name
                        });

                    await Out.WriteLineAsync(message);
                    break;
                }

                case ExecHandlerCommandType.GetHelloWorldMessage:
                {
                    var message = await GetHelloWorldMessageHandler.Value.ExecAsync();

                    await Out.WriteLineAsync(message);
                    break;
                }
            }
        }
    }
}
