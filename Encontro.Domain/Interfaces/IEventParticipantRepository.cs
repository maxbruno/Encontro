using Encontro.Domain.Entities;

namespace Encontro.Domain.Interfaces;

public interface IEventParticipantRepository
{
    Task<IEnumerable<EventParticipant>> GetAllAsync();
    Task<EventParticipant?> GetByIdAsync(int id);
    Task<IEnumerable<EventParticipant>> GetByEventIdAsync(int eventId);
    Task<IEnumerable<EventParticipant>> GetByPersonIdAsync(int personId);
    Task<bool> ExistsAsync(int eventId, int personId);
    Task AddAsync(EventParticipant eventParticipant);
    Task UpdateAsync(EventParticipant eventParticipant);
    Task DeleteAsync(int id);
}
