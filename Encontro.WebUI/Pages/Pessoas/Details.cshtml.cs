using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Encontro.WebUI.Pages.Pessoas
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IPersonService _personService;

        public DetailsModel(IPersonService personService)
        {
            _personService = personService;
        }

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
    }
}
