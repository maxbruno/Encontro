using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Encontro.Domain.Interfaces;

namespace Encontro.Application.Services;

public class EventParticipantService : IEventParticipantService
{
    private readonly IEventParticipantRepository _repository;

    public EventParticipantService(IEventParticipantRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EventParticipant>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<EventParticipant?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<EventParticipant>> GetByEventIdAsync(int eventId)
    {
        return await _repository.GetByEventIdAsync(eventId);
    }

    public async Task<IEnumerable<EventParticipant>> GetByPersonIdAsync(int personId)
    {
        return await _repository.GetByPersonIdAsync(personId);
    }

    public async Task CreateAsync(EventParticipant eventParticipant)
    {
        var exists = await _repository.ExistsAsync(eventParticipant.EventId, eventParticipant.PersonId);
        if (exists)
        {
            throw new InvalidOperationException("Esta pessoa já está inscrita neste evento.");
        }

        await _repository.AddAsync(eventParticipant);
    }

    public async Task UpdateAsync(EventParticipant eventParticipant)
    {
        var existing = await _repository.GetByIdAsync(eventParticipant.Id);
        if (existing == null)
        {
            throw new InvalidOperationException("Participante do evento não encontrado.");
        }

        await _repository.UpdateAsync(eventParticipant);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
