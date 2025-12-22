using Encontro.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Encontro.Application.Interfaces;

public interface IEventService
{
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event?> GetByIdAsync(int id);
    Task<Event> CreateAsync(Event eventEntity, IFormFile? patronSaintPhoto);
    Task<Event> UpdateAsync(Event eventEntity, IFormFile? patronSaintPhoto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Event>> GetByTypeAsync(EventType type);
}
