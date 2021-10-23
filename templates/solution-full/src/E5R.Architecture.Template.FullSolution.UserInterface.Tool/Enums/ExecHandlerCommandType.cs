// Copyright (c) E5R.Architecture.Template.FullSolution. Todos os direitos reservados.
// Configure suas notas de cabeçalho no arquivo ".editorconfig" na raiz da solução.

using E5R.Architecture.Core;

using static E5R.Architecture.Core.MetaTagAttribute;

namespace E5R.Architecture.Template.FullSolution.UserInterface.Tool.Enums
{
    public enum ExecHandlerCommandType
    {
        [MetaTag(CustomIdKey, "get-hello-message")]
        GetHelloMessage,

        [MetaTag(CustomIdKey, "get-hello-world-message")]
        GetHelloWorldMessage
    }
}
