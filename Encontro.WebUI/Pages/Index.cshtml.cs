using Encontro.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Encontro.WebUI.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IEventService _eventService;
        private readonly IPersonService _personService;
        private readonly IEventParticipantService _eventParticipantService;

        public IndexModel(
            IEventService eventService,
            IPersonService personService,
            IEventParticipantService eventParticipantService)
        {
            _eventService = eventService;
            _personService = personService;
            _eventParticipantService = eventParticipantService;
        }

        public int TotalEvents { get; set; }
        public int TotalPeople { get; set; }
        public int TotalParticipants { get; set; }

        public async Task OnGetAsync()
        {
            var events = await _eventService.GetAllAsync();
            var people = await _personService.GetAllAsync();
            var participants = await _eventParticipantService.GetAllAsync();

            TotalEvents = events.Count();
            TotalPeople = people.Count();
            TotalParticipants = participants.Count();
        }
    }
}
