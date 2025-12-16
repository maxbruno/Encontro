using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Encontro.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Encontro.Application.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;
    private readonly IImageService _imageService;
    private readonly string _webRootPath;

    public PersonService(IPersonRepository repository, IImageService imageService, string webRootPath)
    {
        _repository = repository;
        _imageService = imageService;
        _webRootPath = webRootPath;
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Person> CreateAsync(Person person, IFormFile? photo)
    {
        // Save photo if provided
        if (photo != null && photo.Length > 0)
        {
            person.PhotoUrl = await _imageService.SaveImageAsync(photo, _webRootPath);
        }

        return await _repository.AddAsync(person);
    }

    public async Task<Person> UpdateAsync(Person person, IFormFile? photo)
    {
        // Check if person exists
        var existingPerson = await _repository.GetByIdAsync(person.Id);
        if (existingPerson == null)
        {
            throw new InvalidOperationException($"Person with ID {person.Id} not found.");
        }

        var oldPhotoUrl = existingPerson.PhotoUrl;

        // Process new photo if provided
        if (photo != null && photo.Length > 0)
        {
            // Delete old photo if exists
            if (!string.IsNullOrWhiteSpace(oldPhotoUrl))
            {
                await _imageService.DeleteImageAsync(oldPhotoUrl, _webRootPath);
            }

            person.PhotoUrl = await _imageService.SaveImageAsync(photo, _webRootPath);
        }
        else
        {
            // Preserve existing photo if no new photo
            person.PhotoUrl = oldPhotoUrl;
        }

        return await _repository.UpdateAsync(person);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        // Get person to delete associated photo
        var person = await _repository.GetByIdAsync(id);
        if (person != null && !string.IsNullOrWhiteSpace(person.PhotoUrl))
        {
            await _imageService.DeleteImageAsync(person.PhotoUrl, _webRootPath);
        }

        return await _repository.DeleteAsync(id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _repository.ExistsAsync(id);
    }

    public async Task<IEnumerable<Person>> SearchAsync(string searchTerm)
    {
        return await _repository.SearchAsync(searchTerm);
    }
}
