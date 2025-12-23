using Encontro.Domain.Entities;
using Encontro.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Encontro.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _context.People
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
        return await _context.People
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Person> AddAsync(Person person)
    {
        _context.People.Add(person);
        await _context.SaveChangesAsync();
        return person;
    }

    public async Task<Person> UpdateAsync(Person person)
    {
        // Detach any existing tracked entity with the same ID
        var existingEntry = _context.ChangeTracker.Entries<Person>()
            .FirstOrDefault(e => e.Entity.Id == person.Id);
        
        if (existingEntry != null)
        {
            existingEntry.State = EntityState.Detached;
        }

        _context.Attach(person).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
        
        return person;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var person = await _context.People.FindAsync(id);
        
        if (person == null)
        {
            return false;
        }

        _context.People.Remove(person);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.People.AnyAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Person>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return await GetAllAsync();
        }

        return await _context.People
            .Where(p => EF.Functions.Like(p.Name, $"%{searchTerm}%") ||
                       (p.Type != null && EF.Functions.Like(p.Type, $"%{searchTerm}%")) ||
                       (p.Email != null && EF.Functions.Like(p.Email, $"%{searchTerm}%")) ||
                       (p.CellPhone != null && p.CellPhone.Contains(searchTerm)) ||
                       (p.Group != null && EF.Functions.Like(p.Group, $"%{searchTerm}%")))
            .OrderBy(p => p.Name)
            .ToListAsync();
    }
}
