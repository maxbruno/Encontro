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

        public EditModel(IEventService eventService)
        {
            _eventService = eventService;
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _eventService.UpdateAsync(Event);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
