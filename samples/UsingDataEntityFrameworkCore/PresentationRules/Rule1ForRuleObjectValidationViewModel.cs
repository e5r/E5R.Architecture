using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using E5R.Architecture.Core;
using UsingDataEntityFrameworkCore.Models;

namespace UsingDataEntityFrameworkCore.PresentationRules
{
    public class Rule1ForRuleObjectValidationViewModel : RuleFor<RuleObjectValidationViewModel>
    {
        public Rule1ForRuleObjectValidationViewModel() 
            : base("RA-001", "Regra de apresentação 1")
        {
        }
    
        public override Task<RuleCheckResult> CheckAsync(RuleObjectValidationViewModel target)
        {
            Checker.NotNullArgument(target, nameof(target));
            
            // Quando "Property2" é preenchida, "Property3" também é obrigatória
            if(!string.IsNullOrEmpty(target.Property2) && string.IsNullOrEmpty(target.Property3))
            {
                return Task.FromResult(new RuleCheckResult(false,
                    new Dictionary<string, string>
                    {
                        {
                            nameof(target.Property3),
                            $"Quando {nameof(target.Property2)} é preenchido, {nameof(target.Property3)} também é obrigatório."
                        }
                    }));
            }
            
            // Quando "Property3" é preenchida, "Property1" deve ser "AUTO"
            if(!string.IsNullOrEmpty(target.Property3) && !String.Equals("auto", target.Property1, StringComparison.CurrentCultureIgnoreCase))
            {
                return Task.FromResult(new RuleCheckResult(false,
                    new Dictionary<string, string>
                    {
                        {
                            nameof(target.Property3),
                            $"Quando {nameof(target.Property3)} é preenchido, {nameof(target.Property1)} deve ser \"AUTO\"."
                        }
                    }));
            }
    
            return Task.FromResult(RuleCheckResult.Success);
        }
    }
}
