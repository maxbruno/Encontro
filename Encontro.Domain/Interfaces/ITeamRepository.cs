using Encontro.Domain.Entities;

namespace Encontro.Domain.Interfaces;

public interface ITeamRepository
{
    Task<IEnumerable<Team>> GetAllAsync();
    Task<Team?> GetByIdAsync(int id);
}
