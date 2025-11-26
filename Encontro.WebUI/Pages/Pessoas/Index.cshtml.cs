using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Encontro.WebUI.Pages.Pessoas
{
    public class IndexModel : PageModel
    {
        private readonly IPersonService _personService;

        public IndexModel(IPersonService personService)
        {
            _personService = personService;
        }

        public IList<Person> Pessoas { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Pessoas = (await _personService.GetAllAsync()).ToList();
        }
    }
}
