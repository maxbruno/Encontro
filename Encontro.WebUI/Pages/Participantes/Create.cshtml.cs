using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Encontro.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Encontro.WebUI.Pages.Participantes
{
    [Authorize(Roles = "Administrador")]
    public class CreateModel : PageModel
    {
        private readonly IEventParticipantService _eventParticipantService;
        private readonly IPersonRepository _personRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEventService _eventService;

        public CreateModel(
            IEventParticipantService eventParticipantService,
            IPersonRepository personRepository,
            ITeamRepository teamRepository,
            IRoleRepository roleRepository,
            IEventService eventService)
        {
            _eventParticipantService = eventParticipantService;
            _personRepository = personRepository;
            _teamRepository = teamRepository;
            _roleRepository = roleRepository;
            _eventService = eventService;
        }

        [BindProperty]
        public EventParticipant Participant { get; set; } = default!;

        [BindProperty]
        public int EventId { get; set; }

        [BindProperty]
        public string? Action { get; set; }

        public Event CurrentEvent { get; set; } = default!;
        public SelectList PeopleSelectList { get; set; } = default!;
        public SelectList TeamsSelectList { get; set; } = default!;
        public SelectList RolesSelectList { get; set; } = default!;
        public int ParticipantCount { get; set; }

        public async Task<IActionResult> OnGetAsync(int? eventId)
        {
            if (eventId == null)
            {
                return NotFound();
            }

            EventId = eventId.Value;
            var eventEntity = await _eventService.GetByIdAsync(EventId);

            if (eventEntity == null)
            {
                return NotFound();
            }

            CurrentEvent = eventEntity;
            
            // Get participant count for this event
            var participants = await _eventParticipantService.GetByEventIdAsync(EventId);
            ParticipantCount = participants.Count();
            
            await LoadDropdownsAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Remove navigation properties validation errors
            ModelState.Remove("Participant.EventId");
            ModelState.Remove("Participant.Event");
            ModelState.Remove("Participant.Person");
            ModelState.Remove("Participant.Team");
            ModelState.Remove("Participant.Role");
            
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return Page();
            }

            Participant.EventId = EventId;

            // TeamId will be bound automatically now that it's an int

            try
            {
                // Ensure navigation properties are null before saving
                Participant.Event = null!;
                Participant.Person = null!;
                Participant.Team = null!;
                Participant.Role = null!;
                
                await _eventParticipantService.CreateAsync(Participant);
                
                // Get person name for success message
                var person = await _personRepository.GetByIdAsync(Participant.PersonId);
                var personName = person?.Name ?? "Participante";
                
                TempData["SuccessMessage"] = $"{personName} adicionado(a) ao evento com sucesso!";
                
                // Check which button was clicked
                if (Action == "saveAndNew")
                {
                    return RedirectToPage("./Create", new { eventId = EventId });
                }

                // Default: save and return to event details
                return RedirectToPage("/Eventos/Details", new { id = EventId });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                await LoadDropdownsAsync();
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao adicionar participante: {ex.InnerException?.Message ?? ex.Message}");
                await LoadDropdownsAsync();
                return Page();
            }
        }

        private async Task LoadDropdownsAsync()
        {
            CurrentEvent = (await _eventService.GetByIdAsync(EventId))!;

            // Load people
            var people = await _personRepository.GetAllAsync();
            PeopleSelectList = new SelectList(
                people.OrderBy(p => p.Name),
                "Id",
                "Name");

            // Load teams with Order - Name format
            var teams = await _teamRepository.GetAllAsync();
            var teamList = teams
                .OrderBy(t => t.Order)
                .Select(t => new
                {
                    Id = t.Id,
                    Display = $"{t.Order} - {t.Name}"
                }).ToList();
            TeamsSelectList = new SelectList(teamList, "Id", "Display");
            
            // Load roles
            var roles = await _roleRepository.GetAllAsync();
            var roleList = roles.Select(r => new
            {
                Id = r.Id,
                Display = $"{r.Order} - {r.Name}"
            }).ToList();
            
            RolesSelectList = new SelectList(roleList, "Id", "Display");
            
            // Get participant count
            var participants = await _eventParticipantService.GetByEventIdAsync(EventId);
            ParticipantCount = participants.Count();
        }
    }
}