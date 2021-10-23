// Copyright (c) E5R.Architecture.Template.FullSolution. Todos os direitos reservados.
// Configure suas notas de cabeçalho no arquivo ".editorconfig" na raiz da solução.

using System.Linq;
using System.Threading.Tasks;

using E5R.Architecture.Core;
using E5R.Architecture.Template.FullSolution.Domain.Data.TransferObjects;

namespace E5R.Architecture.Template.FullSolution.Rules.GetHello
{
    public class NameRequiresFirstCharacterUppercaseRule : RuleFor<GetHelloMessageInput>
    {
        private new const string Code = "RNGH-03";
        private new const string Description = "The first character of the name must be capitalized";

        public NameRequiresFirstCharacterUppercaseRule() : base(Code, RuleCategory.GetHello, Description)
        {
        }

        public override Task<RuleCheckResult> CheckAsync(GetHelloMessageInput target)
        {
            return string.IsNullOrEmpty(target?.Name)
                ? Fail()
                : char.IsUpper(target.Name.First())
                    ? Success()
                    : Fail();
        }
    }
}
