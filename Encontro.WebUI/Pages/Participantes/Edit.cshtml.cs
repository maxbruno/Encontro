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
    public class EditModel : PageModel
    {
        private readonly IEventParticipantService _eventParticipantService;
        private readonly ITeamRepository _teamRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEventService _eventService;
        private readonly IPersonService _personService;

        public EditModel(
            IEventParticipantService eventParticipantService,
            ITeamRepository teamRepository,
            IRoleRepository roleRepository,
            IEventService eventService,
            IPersonService personService)
        {
            _eventParticipantService = eventParticipantService;
            _teamRepository = teamRepository;
            _roleRepository = roleRepository;
            _eventService = eventService;
            _personService = personService;
        }

        [BindProperty]
        public EventParticipant Participant { get; set; } = default!;

        public Event CurrentEvent { get; set; } = default!;
        public Person CurrentPerson { get; set; } = default!;
        public SelectList TeamsSelectList { get; set; } = default!;
        public SelectList RolesSelectList { get; set; } = default!;

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

            await LoadDropdownsAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Remove navigation properties validation errors
            ModelState.Remove("Participant.Event");
            ModelState.Remove("Participant.Person");
            ModelState.Remove("Participant.Team");
            ModelState.Remove("Participant.Role");
            
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                CurrentEvent = (await _eventService.GetByIdAsync(Participant.EventId))!;
                CurrentPerson = (await _personService.GetByIdAsync(Participant.PersonId))!;
                return Page();
            }

            // TeamId and RoleId will be bound automatically now that they're ints
            
            try
            {
                await _eventParticipantService.UpdateAsync(Participant);
                TempData["SuccessMessage"] = "Participante atualizado com sucesso!";
                return RedirectToPage("/Eventos/Details", new { id = Participant.EventId });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                await LoadDropdownsAsync();
                CurrentEvent = (await _eventService.GetByIdAsync(Participant.EventId))!;
                CurrentPerson = (await _personService.GetByIdAsync(Participant.PersonId))!;
                return Page();
            }
        }

        private async Task LoadDropdownsAsync()
        {
            // Load teams
            var teams = await _teamRepository.GetAllAsync();
            TeamsSelectList = new SelectList(
                teams.OrderBy(t => t.Order),
                "Id",
                "Name",
                Participant.TeamId,
                "Order");
            
            // Load roles
            var roles = await _roleRepository.GetAllAsync();
            var roleList = roles.Select(r => new
            {
                Id = r.Id,
                Display = $"{r.Order} - {r.Name}"
            }).ToList();
            
            roleList.Insert(0, new { Id = 0, Display = "-- Sem Função --" });
            RolesSelectList = new SelectList(roleList, "Id", "Display", Participant.RoleId ?? 0);
        }
    }
}
