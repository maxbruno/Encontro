using Encontro.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace Encontro.Application.Services;

public class ImageService : IImageService
{
    private readonly IStorageService _storageService;
    private const int MaxWidthOrHeight = 800;
    private const int JpegQuality = 75;

    public ImageService(IStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<string> SaveImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Invalid image file");

        // Validate file size (5MB max)
        const long MaxFileSize = 5 * 1024 * 1024; // 5MB
        if (file.Length > MaxFileSize)
            throw new ArgumentException("File too large. Maximum size is 5MB.");

        // Validate Content-Type header
        var allowedContentTypes = new[] { 
            "image/jpeg", "image/jpg", "image/png", 
            "image/gif", "image/bmp" 
        };
        if (!allowedContentTypes.Contains(file.ContentType?.ToLower()))
            throw new ArgumentException("Invalid content type. Only image files are allowed.");

        // Validate file extension
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        
        if (!allowedExtensions.Contains(extension))
            throw new ArgumentException("Unsupported file type. Use JPG, PNG, GIF or BMP.");

        // Generate unique name with GUID
        var fileName = $"{Guid.NewGuid()}.jpg";

        // Process and compress image using ImageSharp
        // Magic Bytes Validation: ImageSharp validates file content
        Image image;
        try
        {
            image = await Image.LoadAsync(file.OpenReadStream());
        }
        catch (Exception ex)
        {
            throw new ArgumentException("File is not a valid image.", ex);
        }
        
        using (image)
        
        // Resize if necessary while maintaining aspect ratio
        if (image.Width > MaxWidthOrHeight || image.Height > MaxWidthOrHeight)
        {
            var options = new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(MaxWidthOrHeight, MaxWidthOrHeight)
            };
            image.Mutate(x => x.Resize(options));
        }

        // Save to memory stream
        using var memoryStream = new MemoryStream();
        await image.SaveAsJpegAsync(memoryStream, new JpegEncoder
        {
            Quality = JpegQuality
        });
        
        memoryStream.Position = 0;

        // Use storage service to save the file
        return await _storageService.SaveFileAsync(fileName, memoryStream, "image/jpeg");
    }

    public async Task DeleteImageAsync(string? imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
            return;

        try
        {
            await _storageService.DeleteFileAsync(imagePath);
        }
        catch
        {
            // Ignore errors when deleting file (file may not exist)
        }
    }
}
