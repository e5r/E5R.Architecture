// Copyright (c) E5R.Architecture.Template.FullSolution. Todos os direitos reservados.
// Configure suas notas de cabeçalho no arquivo ".editorconfig" na raiz da solução.

using System.Threading.Tasks;

using E5R.Architecture.Core;
using E5R.Architecture.Template.FullSolution.Domain.Data.TransferObjects;

namespace E5R.Architecture.Template.FullSolution.Rules.GetHello
{
    public class NameIsRequiredForGetHelloMessageRule : RuleFor<GetHelloMessageInput>
    {
        private new const string Code = "RNGH-01";
        private new const string Description = "Name is required for get a hello message";

        public NameIsRequiredForGetHelloMessageRule() : base(Code, RuleCategory.GetHello, Description)
        {
        }

        public override Task<RuleCheckResult> CheckAsync(GetHelloMessageInput target)
        {
            return string.IsNullOrEmpty(target?.Name)
                ? Fail()
                : Success();
        }
    }
}
