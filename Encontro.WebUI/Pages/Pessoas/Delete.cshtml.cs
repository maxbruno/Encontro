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
        private readonly IEventParticipantService _eventParticipantService;

        public DeleteModel(IPersonService personService, IEventParticipantService eventParticipantService)
        {
            _personService = personService;
            _eventParticipantService = eventParticipantService;
        }

        [BindProperty]
        public Person Person { get; set; } = default!;
        
        public IEnumerable<EventParticipant> EventParticipations { get; set; } = new List<EventParticipant>();

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
            
            // Carregar eventos onde pessoa participa
            EventParticipations = await _eventParticipantService.GetByPersonIdAsync(id.Value);
            
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

            TempData["SuccessMessage"] = "Pessoa inativada com sucesso.";
            return RedirectToPage("./Index");
        }
    }
}
