using System.Threading.Tasks;
using E5R.Architecture.Core;
using UsingDataEntityFrameworkCore.Models;

namespace UsingDataEntityFrameworkCore.PresentationRules
{
    public class Rule3ForRuleObjectValidationViewModel : RuleFor<RuleObjectValidationViewModel>
    {
        public Rule3ForRuleObjectValidationViewModel() 
            : base("RA-003", "Regra de apresentação 3 simplesmente falha")
        {
        }
    
        public override Task<RuleCheckResult> CheckAsync(RuleObjectValidationViewModel target)
        {
            return Task.FromResult(RuleCheckResult.Fail);
        }
    }
}
