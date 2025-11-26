using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Encontro.WebUI.Pages.Pessoas
{
    public class DetailsModel : PageModel
    {
        private readonly IPersonService _personService;

        public DetailsModel(IPersonService personService)
        {
            _personService = personService;
        }

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
    }
}
