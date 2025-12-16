using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Encontro.WebUI.Pages.Pessoas
{
    [Authorize(Roles = "Administrador")]
    public class EditModel : PageModel
    {
        private readonly IPersonService _personService;

        public EditModel(IPersonService personService)
        {
            _personService = personService;
        }

        [BindProperty]
        public Person Person { get; set; } = default!;

        [BindProperty]
        public IFormFile? Photo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _personService.GetByIdAsync(id.Value);

            if (person == null)
            {
                return NotFound();
            }

            Person = person;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Log validation errors for debugging
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                    }
                }
                return Page();
            }

            try
            {
                await _personService.UpdateAsync(Person, Photo);
                TempData["SuccessMessage"] = "Pessoa atualizada com sucesso!";
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error updating person: {ex.Message}");
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                ModelState.AddModelError(string.Empty, $"Erro ao atualizar: {ex.Message}");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
