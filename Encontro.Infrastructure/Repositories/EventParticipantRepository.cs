using Encontro.Domain.Entities;
using Encontro.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Encontro.Infrastructure.Repositories;

public class EventParticipantRepository : IEventParticipantRepository
{
    private readonly AppDbContext _context;

    public EventParticipantRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EventParticipant>> GetAllAsync()
    {
        return await _context.EventParticipants
            .Include(ep => ep.Event)
            .Include(ep => ep.Person)
            .Include(ep => ep.Team)
            .Include(ep => ep.Role)
            .OrderByDescending(ep => ep.RegisteredAt)
            .ToListAsync();
    }

    public async Task<EventParticipant?> GetByIdAsync(int id)
    {
        return await _context.EventParticipants
            .Include(ep => ep.Event)
            .Include(ep => ep.Person)
            .Include(ep => ep.Team)
            .Include(ep => ep.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(ep => ep.Id == id);
    }

    public async Task<IEnumerable<EventParticipant>> GetByEventIdAsync(int eventId)
    {
        return await _context.EventParticipants
            .Include(ep => ep.Event)
            .Include(ep => ep.Person)
            .Include(ep => ep.Team)
            .Include(ep => ep.Role)
            .Where(ep => ep.EventId == eventId)
            .OrderBy(ep => ep.Team!.Order)
            .ThenBy(ep => ep.Person.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<EventParticipant>> GetByPersonIdAsync(int personId)
    {
        return await _context.EventParticipants
            .Include(ep => ep.Event)
            .Include(ep => ep.Person)
            .Include(ep => ep.Team)
            .Include(ep => ep.Role)
            .Where(ep => ep.PersonId == personId)
            .OrderByDescending(ep => ep.RegisteredAt)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(int eventId, int personId)
    {
        return await _context.EventParticipants
            .AnyAsync(ep => ep.EventId == eventId && ep.PersonId == personId);
    }

    public async Task AddAsync(EventParticipant eventParticipant)
    {
        eventParticipant.RegisteredAt = DateTime.Now;
        await _context.EventParticipants.AddAsync(eventParticipant);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EventParticipant eventParticipant)
    {
        // Detach any tracked entity with the same key
        var existingEntry = _context.ChangeTracker.Entries<EventParticipant>()
            .FirstOrDefault(e => e.Entity.Id == eventParticipant.Id);
        
        if (existingEntry != null)
        {
            existingEntry.State = EntityState.Detached;
        }
        
        _context.EventParticipants.Update(eventParticipant);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var eventParticipant = await _context.EventParticipants.FindAsync(id);
        if (eventParticipant != null)
        {
            _context.EventParticipants.Remove(eventParticipant);
            await _context.SaveChangesAsync();
        }
    }
}
