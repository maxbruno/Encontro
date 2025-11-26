using Encontro.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Encontro.Application.Interfaces;

public interface IPersonService
{
    Task<IEnumerable<Person>> GetAllAsync();
    Task<Person?> GetByIdAsync(int id);
    Task<Person> CreateAsync(Person person, IFormFile? photo);
    Task<Person> UpdateAsync(Person person, IFormFile? photo);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
