using Encontro.Domain.Entities;

namespace Encontro.Domain.Interfaces;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event?> GetByIdAsync(int id);
    Task<Event> AddAsync(Event eventEntity);
    Task<Event> UpdateAsync(Event eventEntity);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Event>> GetByTypeAsync(EventType type);
}
