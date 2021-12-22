// Copyright (c) E5R.Architecture.Template.FullSolution. Todos os direitos reservados.
// Configure suas notas de cabeçalho no arquivo ".editorconfig" na raiz da solução.

using System.Threading.Tasks;

using E5R.Architecture.Core;
using E5R.Architecture.Template.FullSolution.Domain.Data.TransferObjects;

namespace E5R.Architecture.Template.FullSolution.Rules.GetHello
{
    public class NameMustBeLeast3CharactersLongRule : RuleFor<GetHelloMessageInput>
    {
        private new const string Code = "RNGH-02";
        private new const string Description = "The name must be at least 3 characters long";

        public NameMustBeLeast3CharactersLongRule() : base(Code, RuleCategory.GetHello, Description)
        {
        }

        public override Task<RuleCheckResult> CheckAsync(GetHelloMessageInput target)
        {
            return string.IsNullOrEmpty(target?.Name)
                ? Fail()
                : target.Name.Trim().Length >= 3
                    ? Success()
                    : Fail();
        }
    }
}
