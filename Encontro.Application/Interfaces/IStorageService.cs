namespace Encontro.Application.Interfaces;

/// <summary>
/// Interface for file storage services.
/// Allows implementations for different providers (local, S3, Azure Blob Storage, etc.)
/// </summary>
public interface IStorageService
{
    /// <summary>
    /// Saves a file to storage
    /// </summary>
    /// <param name="fileName">File name (including extension)</param>
    /// <param name="stream">Stream with file content</param>
    /// <param name="contentType">Content type (MIME type)</param>
    /// <returns>Relative path/URL of the saved file</returns>
    Task<string> SaveFileAsync(string fileName, Stream stream, string contentType);

    /// <summary>
    /// Deletes a file from storage
    /// </summary>
    /// <param name="filePath">Relative path/URL of the file</param>
    Task DeleteFileAsync(string filePath);

    /// <summary>
    /// Gets the full URL to access the file
    /// </summary>
    /// <param name="filePath">Relative path/URL of the file</param>
    /// <returns>Full URL to access the file</returns>
    string GetFileUrl(string filePath);

    /// <summary>
    /// Checks if a file exists
    /// </summary>
    /// <param name="filePath">Relative path/URL of the file</param>
    /// <returns>True if the file exists</returns>
    Task<bool> FileExistsAsync(string filePath);
}
