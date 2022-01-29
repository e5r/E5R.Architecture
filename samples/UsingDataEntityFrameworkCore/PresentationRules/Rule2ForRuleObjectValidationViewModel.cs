using System;
using System.Threading.Tasks;
using E5R.Architecture.Core;
using UsingDataEntityFrameworkCore.Models;

namespace UsingDataEntityFrameworkCore.PresentationRules
{
    public class Rule2ForRuleObjectValidationViewModel : RuleFor<RuleObjectValidationViewModel>
    {
        public Rule2ForRuleObjectValidationViewModel() 
            : base("RA-002", "Regra de apresentação 2")
        {
        }
    
        public override Task<RuleCheckResult> CheckAsync(RuleObjectValidationViewModel target)
        {
            throw new NotImplementedException("Ops!");
        }
    }
}
