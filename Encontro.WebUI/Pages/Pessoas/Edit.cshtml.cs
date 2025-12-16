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
                return Page();
            }

            try
            {
                await _personService.UpdateAsync(Person, Photo);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
