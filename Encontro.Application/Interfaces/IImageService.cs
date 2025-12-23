using Microsoft.AspNetCore.Http;

namespace Encontro.Application.Interfaces;

public interface IImageService
{
    /// <summary>
    /// Saves an image to the server, compressing it and generating a unique GUID name
    /// </summary>
    /// <param name="file">Uploaded image file</param>
    /// <returns>Relative path of the saved image (e.g., "uploads/abc123.jpg")</returns>
    Task<string> SaveImageAsync(IFormFile file);

    /// <summary>
    /// Deletes an image from the server by path
    /// </summary>
    /// <param name="imagePath">Relative path of the image (e.g., "uploads/abc123.jpg")</param>
    Task DeleteImageAsync(string? imagePath);
}
