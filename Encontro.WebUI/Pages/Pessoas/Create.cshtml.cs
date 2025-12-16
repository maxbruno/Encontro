using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Encontro.WebUI.Pages.Pessoas
{
    [Authorize(Roles = "Administrador")]
    public class CreateModel : PageModel
    {
        private readonly IPersonService _personService;

        public CreateModel(IPersonService personService)
        {
            _personService = personService;
        }

        [BindProperty]
        public Person Person { get; set; } = default!;

        [BindProperty]
        public IFormFile? Photo { get; set; }

        [BindProperty]
        public string? Action { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _personService.CreateAsync(Person, Photo);

            // Check which button was clicked
            if (Action == "saveAndNew")
            {
                TempData["SuccessMessage"] = $"Pessoa '{Person.Name}' cadastrada com sucesso!";
                return RedirectToPage("./Create");
            }

            // Default: save and return to list
            TempData["SuccessMessage"] = $"Pessoa '{Person.Name}' cadastrada com sucesso!";
            return RedirectToPage("./Index");
        }
    }
}
