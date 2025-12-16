using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Encontro.WebUI.Pages.Eventos
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IEventService _eventService;
        private readonly IEventParticipantService _eventParticipantService;

        public IndexModel(IEventService eventService, IEventParticipantService eventParticipantService)
        {
            _eventService = eventService;
            _eventParticipantService = eventParticipantService;
        }

        public IList<Event> Events { get; set; } = new List<Event>();
        public IList<Event> AllEvents { get; set; } = new List<Event>();
        public Dictionary<int, int> EventParticipantCounts { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? FilterType { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; } = "desc";

        // Estatísticas
        public int TotalEvents { get; set; }
        public int SeguiMeEvents { get; set; }
        public int ECCEvents { get; set; }

        public async Task OnGetAsync()
        {
            AllEvents = (await _eventService.GetAllAsync()).ToList();
            var filteredEvents = AllEvents.AsEnumerable();

            // Buscar contagem de participantes para cada evento
            var allParticipants = await _eventParticipantService.GetAllAsync();
            EventParticipantCounts = allParticipants
                .GroupBy(p => p.EventId)
                .ToDictionary(g => g.Key, g => g.Count());

            // Aplicar busca
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                filteredEvents = filteredEvents.Where(e => 
                    e.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // Aplicar filtro por tipo
            if (!string.IsNullOrWhiteSpace(FilterType))
            {
                if (Enum.TryParse<EventType>(FilterType, out var eventType))
                {
                    filteredEvents = filteredEvents.Where(e => e.EventType == eventType);
                }
            }

            // Aplicar ordenação
            filteredEvents = SortBy switch
            {
                "name" => SortOrder == "desc" ? filteredEvents.OrderByDescending(e => e.Name) : filteredEvents.OrderBy(e => e.Name),
                "type" => SortOrder == "desc" ? filteredEvents.OrderByDescending(e => e.EventType) : filteredEvents.OrderBy(e => e.EventType),
                _ => SortOrder == "desc" ? filteredEvents.OrderByDescending(e => e.CreatedAt) : filteredEvents.OrderBy(e => e.CreatedAt)
            };

            Events = filteredEvents.ToList();

            // Calcular estatísticas
            TotalEvents = AllEvents.Count;
            SeguiMeEvents = AllEvents.Count(e => e.EventType == EventType.SeguiMe);
            ECCEvents = AllEvents.Count(e => e.EventType == EventType.ECC);
        }
    }
}
