using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Encontro.WebUI.Pages.Eventos
{
    [Authorize(Roles = "Administrador")]
    public class EditModel : PageModel
    {
        private readonly IEventService _eventService;
        private readonly ILogger<EditModel> _logger;

        public EditModel(IEventService eventService, ILogger<EditModel> logger)
        {
            _eventService = eventService;
            _logger = logger;
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

        [BindProperty]
        public IFormFile? PatronSaintPhoto { get; set; }

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("OnPostAsync called");
            _logger.LogInformation($"Event.Id: {Event.Id}");
            _logger.LogInformation($"Event.PatronSaintName: '{Event.PatronSaintName}'");
            _logger.LogInformation($"Event.PatronSaintImageUrl: '{Event.PatronSaintImageUrl}'");
            _logger.LogInformation($"PatronSaintPhoto: {(PatronSaintPhoto != null ? $"File uploaded: {PatronSaintPhoto.FileName}" : "No file")}");
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Ensure UpdatedAt is set
                Event.UpdatedAt = DateTime.UtcNow;
                
                var result = await _eventService.UpdateAsync(Event, PatronSaintPhoto);
                
                _logger.LogInformation($"Updated event - PatronSaintName: '{result.PatronSaintName}', PatronSaintImageUrl: '{result.PatronSaintImageUrl}'");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Event not found");
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
