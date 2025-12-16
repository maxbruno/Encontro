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

        var term = searchTerm.ToLower();
        return await _context.People
            .Where(p => p.Name.ToLower().Contains(term) ||
                       (p.Type != null && p.Type.ToLower().Contains(term)) ||
                       (p.Email != null && p.Email.ToLower().Contains(term)) ||
                       (p.CellPhone != null && p.CellPhone.Contains(term)) ||
                       (p.Group != null && p.Group.ToLower().Contains(term)))
            .OrderBy(p => p.Name)
            .ToListAsync();
    }
}
