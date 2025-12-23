using Encontro.Application.Interfaces;

namespace Encontro.Infrastructure.Services;

/// <summary>
/// Local file system storage implementation
/// </summary>
public class LocalFileStorageService : IStorageService
{
    private readonly string _webRootPath;
    private const string UploadFolder = "uploads";

    public LocalFileStorageService(string webRootPath)
    {
        _webRootPath = webRootPath ?? throw new ArgumentNullException(nameof(webRootPath));
    }

    public async Task<string> SaveFileAsync(string fileName, Stream stream, string contentType)
    {
        // Path Traversal Protection: Sanitize filename
        fileName = Path.GetFileName(fileName);
        
        // Validate filename for invalid characters and path traversal attempts
        if (string.IsNullOrWhiteSpace(fileName) || 
            fileName.Contains("..") || 
            fileName.Any(c => Path.GetInvalidFileNameChars().Contains(c)))
        {
            throw new ArgumentException("Invalid filename", nameof(fileName));
        }

        var uploadPath = Path.Combine(_webRootPath, UploadFolder);
        
        // Create directory if it doesn't exist
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        var fullPath = Path.Combine(uploadPath, fileName);

        // Save the file
        using var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        await stream.CopyToAsync(fileStream);

        // Return relative path with leading slash for web URLs
        return $"/{UploadFolder}/{fileName}";
    }

    public Task DeleteFileAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return Task.CompletedTask;

        try
        {
            var fullPath = Path.GetFullPath(Path.Combine(_webRootPath, filePath));
            var uploadsPath = Path.GetFullPath(Path.Combine(_webRootPath, UploadFolder));
            
            // Path Traversal Protection: Ensure file is within uploads directory
            if (!fullPath.StartsWith(uploadsPath, StringComparison.OrdinalIgnoreCase))
            {
                // File is outside uploads directory - silently return without deleting
                return Task.CompletedTask;
            }
            
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
        catch
        {
            // Ignore errors when deleting file
        }

        return Task.CompletedTask;
    }

    public string GetFileUrl(string filePath)
    {
        // For local storage, returns the relative path
        // that will be served as a static file by ASP.NET Core
        return $"/{filePath}";
    }

    public Task<bool> FileExistsAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return Task.FromResult(false);

        var fullPath = Path.Combine(_webRootPath, filePath);
        return Task.FromResult(File.Exists(fullPath));
    }
}
