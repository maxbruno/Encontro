using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Encontro.Domain.Interfaces;

namespace Encontro.Application.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _repository;

    public EventService(IEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Event> CreateAsync(Event eventEntity)
    {
        return await _repository.AddAsync(eventEntity);
    }

    public async Task<Event> UpdateAsync(Event eventEntity)
    {
        var existingEvent = await _repository.GetByIdAsync(eventEntity.Id);
        if (existingEvent == null)
        {
            throw new InvalidOperationException($"Event with ID {eventEntity.Id} not found");
        }

        return await _repository.UpdateAsync(eventEntity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Event>> GetByTypeAsync(EventType type)
    {
        return await _repository.GetByTypeAsync(type);
    }
}
