using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Encontro.WebUI.Pages.Participantes
{
    [Authorize(Roles = "Administrador")]
    public class DeleteModel : PageModel
    {
        private readonly IEventParticipantService _eventParticipantService;
        private readonly IEventService _eventService;
        private readonly IPersonService _personService;

        public DeleteModel(
            IEventParticipantService eventParticipantService,
            IEventService eventService,
            IPersonService personService)
        {
            _eventParticipantService = eventParticipantService;
            _eventService = eventService;
            _personService = personService;
        }

        [BindProperty]
        public EventParticipant Participant { get; set; } = default!;

        public Event CurrentEvent { get; set; } = default!;
        public Person CurrentPerson { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participant = await _eventParticipantService.GetByIdAsync(id.Value);

            if (participant == null)
            {
                return NotFound();
            }

            Participant = participant;
            CurrentEvent = (await _eventService.GetByIdAsync(participant.EventId))!;
            CurrentPerson = (await _personService.GetByIdAsync(participant.PersonId))!;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participant = await _eventParticipantService.GetByIdAsync(id.Value);

            if (participant == null)
            {
                return NotFound();
            }

            var eventId = participant.EventId;

            await _eventParticipantService.DeleteAsync(id.Value);
            TempData["SuccessMessage"] = "Participante removido com sucesso!";

            return RedirectToPage("/Eventos/Details", new { id = eventId });
        }
    }
}
