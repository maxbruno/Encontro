using Encontro.Application.Interfaces;
using Encontro.Domain.Entities;
using Encontro.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Encontro.Application.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _repository;
    private readonly IImageService _imageService;
    private readonly string _webRootPath;

    public EventService(IEventRepository repository, IImageService imageService, string webRootPath)
    {
        _repository = repository;
        _imageService = imageService;
        _webRootPath = webRootPath;
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Event> CreateAsync(Event eventEntity, IFormFile? patronSaintPhoto)
    {
        if (patronSaintPhoto != null)
        {
            eventEntity.PatronSaintImageUrl = await _imageService.SaveImageAsync(patronSaintPhoto, _webRootPath);
        }
        
        return await _repository.AddAsync(eventEntity);
    }

    public async Task<Event> UpdateAsync(Event eventEntity, IFormFile? patronSaintPhoto)
    {
        // Get existing event without tracking to avoid conflict
        var existingEvent = await _repository.GetByIdAsync(eventEntity.Id);
        if (existingEvent == null)
        {
            throw new InvalidOperationException($"Event with ID {eventEntity.Id} not found");
        }

        if (patronSaintPhoto != null)
        {
            // Delete old image if exists
            if (!string.IsNullOrEmpty(existingEvent.PatronSaintImageUrl))
            {
                await _imageService.DeleteImageAsync(existingEvent.PatronSaintImageUrl, _webRootPath);
            }
            
            eventEntity.PatronSaintImageUrl = await _imageService.SaveImageAsync(patronSaintPhoto, _webRootPath);
        }
        else
        {
            // Preserve existing image URL if no new photo uploaded
            eventEntity.PatronSaintImageUrl = existingEvent.PatronSaintImageUrl;
        }
        
        // Detach existing entity to avoid tracking conflict
        _repository.Detach(existingEvent);

        return await _repository.UpdateAsync(eventEntity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var eventEntity = await _repository.GetByIdAsync(id);
        if (eventEntity != null && !string.IsNullOrEmpty(eventEntity.PatronSaintImageUrl))
        {
            await _imageService.DeleteImageAsync(eventEntity.PatronSaintImageUrl, _webRootPath);
        }
        
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Event>> GetByTypeAsync(EventType type)
    {
        return await _repository.GetByTypeAsync(type);
    }
}
