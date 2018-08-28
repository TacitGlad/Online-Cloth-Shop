using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Obaju.Models.BindingModels;
using Obaju.Services.Interfaces;
using System.Threading.Tasks;

namespace Obaju.App.Pages
{
    public class ContactModel : PageModel
    {
        private readonly ILogger<ContactModel> _logger;
        private readonly IUtilityManager _utilityManager;

        public ContactModel(ILogger<ContactModel> logger, IUtilityManager utilityManager)
        {
            _logger = logger;
            _utilityManager = utilityManager;
        }

        [BindProperty]
        public ContactFormBindingModel ContactForm { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _logger.LogInformation($"{ContactForm.FirstName} {ContactForm.LastName} submit an issue.");

            await _utilityManager.AddIssueAsync(ContactForm);

            return RedirectToPage();
        }
    }
}