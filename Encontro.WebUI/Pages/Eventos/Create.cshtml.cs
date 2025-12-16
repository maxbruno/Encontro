using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Encontro.WebUI.Pages.Eventos
{
    [Authorize(Roles = "Administrador")]
    public class CreateModel : PageModel
    {
        private readonly IEventService _eventService;

        public CreateModel(IEventService eventService)
        {
            _eventService = eventService;
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

        [BindProperty]
        public string? Action { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var createdEvent = await _eventService.CreateAsync(Event);

            TempData["SuccessMessage"] = $"Evento '{Event.Name}' criado com sucesso!";

            // Check which button was clicked
            if (Action == "saveAndViewDetails")
            {
                return RedirectToPage("/Eventos/Details", new { id = createdEvent.Id });
            }

            // Default: save and return to list
            return RedirectToPage("./Index");
        }
    }
}
