using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Encontro.WebUI.Pages.Pessoas
{
    public class DeleteModel : PageModel
    {
        private readonly IPersonService _personService;

        public DeleteModel(IPersonService personService)
        {
            _personService = personService;
        }

        [BindProperty]
        public Person Pessoa { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _personService.GetByIdAsync(id.Value);

            if (pessoa == null)
            {
                return NotFound();
            }

            Pessoa = pessoa;
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
