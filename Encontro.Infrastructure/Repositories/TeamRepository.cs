using Encontro.Domain.Entities;
using Encontro.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Encontro.Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly AppDbContext _context;

    public TeamRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Team>> GetAllAsync()
    {
        return await _context.Teams
            .OrderBy(t => t.Order)
            .ToListAsync();
    }

    public async Task<Team?> GetByIdAsync(int id)
    {
        return await _context.Teams.FindAsync(id);
    }
}
