using Encontro.Domain.Entities;

namespace Encontro.Application.Interfaces;

public interface ILookupService
{
    Task<IEnumerable<Person>> GetPeopleForSelectionAsync();
    Task<IEnumerable<Team>> GetTeamsForSelectionAsync();
    Task<IEnumerable<Role>> GetRolesForSelectionAsync();
    Task<IEnumerable<Event>> GetEventsForSelectionAsync();
}
