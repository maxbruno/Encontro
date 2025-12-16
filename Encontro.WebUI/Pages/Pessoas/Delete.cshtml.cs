using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Encontro.WebUI.Pages.Pessoas
{
    [Authorize(Roles = "Administrador")]
    public class DeleteModel : PageModel
    {
        private readonly IPersonService _personService;

        public DeleteModel(IPersonService personService)
        {
            _personService = personService;
        }

        [BindProperty]
        public Person Person { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleted = await _personService.DeleteAsync(id.Value);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
