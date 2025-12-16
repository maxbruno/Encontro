using Encontro.Domain.Entities;

namespace Encontro.Application.Interfaces;

public interface IEventParticipantService
{
    Task<IEnumerable<EventParticipant>> GetAllAsync();
    Task<EventParticipant?> GetByIdAsync(int id);
    Task<IEnumerable<EventParticipant>> GetByEventIdAsync(int eventId);
    Task<IEnumerable<EventParticipant>> GetByPersonIdAsync(int personId);
    Task CreateAsync(EventParticipant eventParticipant);
    Task UpdateAsync(EventParticipant eventParticipant);
    Task DeleteAsync(int id);
}
