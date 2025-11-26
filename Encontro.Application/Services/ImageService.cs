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

    public async Task<string> SaveImageAsync(IFormFile file, string webRootPath)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Invalid image file");

        // Validate file type
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        
        if (!allowedExtensions.Contains(extension))
            throw new ArgumentException("Unsupported file type. Use JPG, PNG, GIF or BMP.");

        // Generate unique name with GUID
        var fileName = $"{Guid.NewGuid()}.jpg";

        // Processar e comprimir imagem usando ImageSharp
        using var image = await Image.LoadAsync(file.OpenReadStream());
        
        // Redimensionar se necessário mantendo proporção
        if (image.Width > MaxWidthOrHeight || image.Height > MaxWidthOrHeight)
        {
            var options = new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(MaxWidthOrHeight, MaxWidthOrHeight)
            };
            image.Mutate(x => x.Resize(options));
        }

        // Salvar em stream de memória
        using var memoryStream = new MemoryStream();
        await image.SaveAsJpegAsync(memoryStream, new JpegEncoder
        {
            Quality = JpegQuality
        });
        
        memoryStream.Position = 0;

        // Use storage service to save the file
        return await _storageService.SaveFileAsync(fileName, memoryStream, "image/jpeg");
    }

    public async Task DeleteImageAsync(string? imagePath, string webRootPath)
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
