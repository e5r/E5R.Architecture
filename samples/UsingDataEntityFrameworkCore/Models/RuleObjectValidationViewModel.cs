using System.ComponentModel.DataAnnotations;
using E5R.Architecture.Core;

namespace UsingDataEntityFrameworkCore.Models
{
    public class RuleObjectValidationViewModel : RuleValidatableObject<RuleObjectValidationViewModel>
    {
        [Required]
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
    }
}
