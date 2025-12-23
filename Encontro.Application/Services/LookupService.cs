using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Encontro.Domain.Interfaces;

namespace Encontro.Application.Services;

public class LookupService : ILookupService
{
    private readonly IPersonRepository _personRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IEventRepository _eventRepository;

    public LookupService(
        IPersonRepository personRepository,
        ITeamRepository teamRepository,
        IRoleRepository roleRepository,
        IEventRepository eventRepository)
    {
        _personRepository = personRepository;
        _teamRepository = teamRepository;
        _roleRepository = roleRepository;
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<Person>> GetPeopleForSelectionAsync()
    {
        return await _personRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Team>> GetTeamsForSelectionAsync()
    {
        return await _teamRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Role>> GetRolesForSelectionAsync()
    {
        return await _roleRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Event>> GetEventsForSelectionAsync()
    {
        return await _eventRepository.GetAllAsync();
    }
}
