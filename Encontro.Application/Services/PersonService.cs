using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Encontro.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Encontro.Application.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;
    private readonly IEventParticipantRepository _eventParticipantRepository;
    private readonly IImageService _imageService;

    public PersonService(
        IPersonRepository repository, 
        IEventParticipantRepository eventParticipantRepository,
        IImageService imageService)
    {
        _repository = repository;
        _eventParticipantRepository = eventParticipantRepository;
        _imageService = imageService;
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
            person.PhotoUrl = await _imageService.SaveImageAsync(photo);
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
                await _imageService.DeleteImageAsync(oldPhotoUrl);
            }

            person.PhotoUrl = await _imageService.SaveImageAsync(photo);
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
        // Com soft delete, não precisamos deletar a foto
        // A pessoa será apenas inativada, mantendo todos os dados e relacionamentos
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
