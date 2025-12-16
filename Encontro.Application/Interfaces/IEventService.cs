using Encontro.Domain.Entities;

namespace Encontro.Application.Interfaces;

public interface IEventService
{
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event?> GetByIdAsync(int id);
    Task<Event> CreateAsync(Event eventEntity);
    Task<Event> UpdateAsync(Event eventEntity);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Event>> GetByTypeAsync(EventType type);
}
