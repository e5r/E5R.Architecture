using System.ComponentModel.DataAnnotations;
using E5R.Architecture.Core;
using Microsoft.AspNetCore.Mvc;
using UsingDataEntityFrameworkCore.Models;

namespace UsingDataEntityFrameworkCore.Controllers
{
    public class ValidationController : Controller
    {
        private readonly IRuleModelValidator _ruleModelValidator;
        
        public ValidationController(IRuleModelValidator ruleModelValidator)
        {
            Checker.NotNullArgument(ruleModelValidator, nameof(ruleModelValidator));

            _ruleModelValidator = ruleModelValidator;
        }
        
        // GET
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        // POST 2
        [HttpPost]
        public IActionResult Index(RuleObjectValidationViewModel model)
        {
            bool isValid = ModelState.IsValid;

            var validationContext = new ValidationContext(model, HttpContext.RequestServices, null);
            var validateResults = model.Validate(validationContext);
            var validateResults2 = _ruleModelValidator.Validate(model);

            return View(model);
        }
    }
}
