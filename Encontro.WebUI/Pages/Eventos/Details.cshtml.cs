using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Encontro.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Encontro.WebUI.Pages.Eventos
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IEventService _eventService;
        private readonly IEventParticipantService _eventParticipantService;
        private readonly ITeamRepository _teamRepository;
        private readonly IRoleRepository _roleRepository;

        public DetailsModel(
            IEventService eventService,
            IEventParticipantService eventParticipantService,
            ITeamRepository teamRepository,
            IRoleRepository roleRepository)
        {
            _eventService = eventService;
            _eventParticipantService = eventParticipantService;
            _teamRepository = teamRepository;
            _roleRepository = roleRepository;
        }

        public Event Event { get; set; } = default!;
        public IList<EventParticipant> Participants { get; set; } = new List<EventParticipant>();
        public IList<EventParticipant> FilteredParticipants { get; set; } = new List<EventParticipant>();
        public SelectList TeamsSelectList { get; set; } = default!;
        public SelectList RolesSelectList { get; set; } = default!;

        // Statistics
        public int TotalParticipants { get; set; }
        public int ParticipantsWithTeam { get; set; }
        public int ParticipantsWithRole { get; set; }
        public int ParticipantsWithoutTeam { get; set; }
        public int TeamsCount { get; set; }
        public int RolesCount { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedTeamId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedRoleId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventEntity = await _eventService.GetByIdAsync(id.Value);

            if (eventEntity == null)
            {
                return NotFound();
            }

            Event = eventEntity;

            // Load participants
            Participants = (await _eventParticipantService.GetByEventIdAsync(id.Value)).ToList();

            // Calculate statistics
            TotalParticipants = Participants.Count;
            ParticipantsWithTeam = Participants.Count(p => p.TeamId != null);
            ParticipantsWithRole = Participants.Count(p => p.RoleId != null);
            ParticipantsWithoutTeam = Participants.Count(p => p.TeamId == null);
            TeamsCount = Participants.Where(p => p.TeamId != null).Select(p => p.TeamId).Distinct().Count();
            RolesCount = Participants.Where(p => p.RoleId != null).Select(p => p.RoleId).Distinct().Count();

            // Apply filters
            FilteredParticipants = Participants;

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                FilteredParticipants = FilteredParticipants
                    .Where(p => p.Person.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(SelectedTeamId))
            {
                if (SelectedTeamId == "none")
                {
                    FilteredParticipants = FilteredParticipants.Where(p => p.TeamId == null).ToList();
                }
                else if (int.TryParse(SelectedTeamId, out var teamId))
                {
                    FilteredParticipants = FilteredParticipants.Where(p => p.TeamId == teamId).ToList();
                }
            }

            if (!string.IsNullOrWhiteSpace(SelectedRoleId))
            {
                if (SelectedRoleId == "none")
                {
                    FilteredParticipants = FilteredParticipants.Where(p => p.RoleId == null).ToList();
                }
                else if (int.TryParse(SelectedRoleId, out var roleId))
                {
                    FilteredParticipants = FilteredParticipants.Where(p => p.RoleId == roleId).ToList();
                }
            }

            // Apply sorting
            FilteredParticipants = ApplySorting(FilteredParticipants);

            // Load teams for filter dropdown
            var teams = await _teamRepository.GetAllAsync();
            var teamList = teams.Select(t => new
            {
                Id = t.Id.ToString(),
                Display = $"{t.Order} - {t.Name}"
            }).ToList();

            teamList.Insert(0, new { Id = "", Display = "Todas as Equipes" });
            teamList.Insert(1, new { Id = "none", Display = "Sem Equipe" });
            TeamsSelectList = new SelectList(teamList, "Id", "Display");

            // Load roles for filter dropdown
            var roles = await _roleRepository.GetAllAsync();
            var roleList = roles.Select(r => new
            {
                Id = r.Id.ToString(),
                Display = $"{r.Order} - {r.Name}"
            }).ToList();

            roleList.Insert(0, new { Id = "", Display = "Todas as Funções" });
            roleList.Insert(1, new { Id = "none", Display = "Sem Função" });
            RolesSelectList = new SelectList(roleList, "Id", "Display");

            return Page();
        }

        private IList<EventParticipant> ApplySorting(IList<EventParticipant> participants)
        {
            if (string.IsNullOrWhiteSpace(SortBy))
            {
                return participants.OrderBy(p => p.Person.Name).ToList();
            }

            var sortOrder = SortOrder?.ToLower() == "desc";

            return SortBy.ToLower() switch
            {
                "name" => sortOrder
                    ? participants.OrderByDescending(p => p.Person.Name).ToList()
                    : participants.OrderBy(p => p.Person.Name).ToList(),
                
                "team" => sortOrder
                    ? participants.OrderByDescending(p => p.Team?.Name ?? "ZZZZ").ToList()
                    : participants.OrderBy(p => p.Team?.Name ?? "ZZZZ").ToList(),
                
                "role" => sortOrder
                    ? participants.OrderByDescending(p => p.Role?.Name ?? "ZZZZ").ToList()
                    : participants.OrderBy(p => p.Role?.Name ?? "ZZZZ").ToList(),
                
                "date" => sortOrder
                    ? participants.OrderByDescending(p => p.RegisteredAt).ToList()
                    : participants.OrderBy(p => p.RegisteredAt).ToList(),
                
                _ => participants.OrderBy(p => p.Person.Name).ToList()
            };
        }
    }
}
