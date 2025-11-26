using Microsoft.AspNetCore.Http;

namespace Encontro.Application.Interfaces;

public interface IImageService
{
    /// <summary>
    /// Saves an image to the server, compressing it and generating a unique GUID name
    /// </summary>
    /// <param name="file">Uploaded image file</param>
    /// <param name="webRootPath">Path to wwwroot</param>
    /// <returns>Relative path of the saved image (e.g., "uploads/abc123.jpg")</returns>
    Task<string> SaveImageAsync(IFormFile file, string webRootPath);

    /// <summary>
    /// Deletes an image from the server by path
    /// </summary>
    /// <param name="imagePath">Relative path of the image (e.g., "uploads/abc123.jpg")</param>
    /// <param name="webRootPath">Path to wwwroot</param>
    Task DeleteImageAsync(string? imagePath, string webRootPath);
}
