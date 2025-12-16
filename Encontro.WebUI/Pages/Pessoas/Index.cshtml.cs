using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Encontro.WebUI.Pages.Pessoas
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IPersonService _personService;

        public IndexModel(IPersonService personService)
        {
            _personService = personService;
        }

        public IList<Person> People { get; set; } = default!;
        public IList<Person> AllPeople { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? FilterType { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? FilterPhoto { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; } = "asc";

        // Estatísticas
        public int TotalPeople { get; set; }
        public int PeopleWithPhoto { get; set; }
        public int PeopleWithoutPhoto { get; set; }

        public async Task OnGetAsync()
        {
            AllPeople = (await _personService.GetAllAsync()).ToList();
            var filteredPeople = AllPeople.AsEnumerable();

            // Aplicar busca
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                filteredPeople = filteredPeople.Where(p => 
                    p.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    (p.Type != null && p.Type.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (p.Email != null && p.Email.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (p.CellPhone != null && p.CellPhone.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (p.Group != null && p.Group.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)));
            }

            // Aplicar filtro por tipo
            if (!string.IsNullOrWhiteSpace(FilterType))
            {
                filteredPeople = filteredPeople.Where(p => p.Type == FilterType);
            }

            // Aplicar filtro por foto
            if (!string.IsNullOrWhiteSpace(FilterPhoto))
            {
                if (FilterPhoto == "with")
                    filteredPeople = filteredPeople.Where(p => !string.IsNullOrWhiteSpace(p.PhotoUrl));
                else if (FilterPhoto == "without")
                    filteredPeople = filteredPeople.Where(p => string.IsNullOrWhiteSpace(p.PhotoUrl));
            }

            // Aplicar ordenação
            filteredPeople = SortBy switch
            {
                "type" => SortOrder == "desc" ? filteredPeople.OrderByDescending(p => p.Type) : filteredPeople.OrderBy(p => p.Type),
                "email" => SortOrder == "desc" ? filteredPeople.OrderByDescending(p => p.Email) : filteredPeople.OrderBy(p => p.Email),
                "group" => SortOrder == "desc" ? filteredPeople.OrderByDescending(p => p.Group) : filteredPeople.OrderBy(p => p.Group),
                _ => SortOrder == "desc" ? filteredPeople.OrderByDescending(p => p.Name) : filteredPeople.OrderBy(p => p.Name)
            };

            People = filteredPeople.ToList();

            // Calcular estatísticas
            TotalPeople = AllPeople.Count;
            PeopleWithPhoto = AllPeople.Count(p => !string.IsNullOrWhiteSpace(p.PhotoUrl));
            PeopleWithoutPhoto = TotalPeople - PeopleWithPhoto;
        }
    }
}
