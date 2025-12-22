using Encontro.Domain.Entities;
using Encontro.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Encontro.Infrastructure.Repositories;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;

    public EventRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _context.Events
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        return await _context.Events.FindAsync(id);
    }

    public async Task<Event> AddAsync(Event eventEntity)
    {
        eventEntity.CreatedAt = DateTime.UtcNow;
        eventEntity.UpdatedAt = DateTime.UtcNow;
        _context.Events.Add(eventEntity);
        await _context.SaveChangesAsync();
        return eventEntity;
    }

    public async Task<Event> UpdateAsync(Event eventEntity)
    {
        eventEntity.UpdatedAt = DateTime.UtcNow;
        _context.Attach(eventEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return eventEntity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var eventEntity = await _context.Events.FindAsync(id);
        if (eventEntity == null)
        {
            return false;
        }

        _context.Events.Remove(eventEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Event>> GetByTypeAsync(EventType type)
    {
        return await _context.Events
            .Where(e => e.EventType == type)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();
    }

    public void Detach(Event eventEntity)
    {
        _context.Entry(eventEntity).State = EntityState.Detached;
    }
}
