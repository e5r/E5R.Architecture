using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UsingDataEntityFrameworkCore.Models;

namespace UsingDataEntityFrameworkCore.Controllers
{
    public class ValidationController : Controller
    {
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

            return View(model);
        }
    }
}
