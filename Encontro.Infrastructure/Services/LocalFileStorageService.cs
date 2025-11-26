using Encontro.Application.Interfaces;

namespace Encontro.Infrastructure.Services;

/// <summary>
/// Implementação de armazenamento local em sistema de arquivos
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
        var uploadPath = Path.Combine(_webRootPath, UploadFolder);
        
        // Criar diretório se não existir
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        var caminhoCompleto = Path.Combine(uploadPath, fileName);

        // Salvar o arquivo
        using var fileStream = new FileStream(caminhoCompleto, FileMode.Create, FileAccess.Write);
        await stream.CopyToAsync(fileStream);

        // Retornar caminho relativo
        return $"{UploadFolder}/{fileName}";
    }

    public Task DeleteFileAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return Task.CompletedTask;

        try
        {
            var caminhoCompleto = Path.Combine(_webRootPath, filePath);
            
            if (File.Exists(caminhoCompleto))
                File.Delete(caminhoCompleto);
        }
        catch
        {
            // Ignorar erros ao excluir arquivo
        }

        return Task.CompletedTask;
    }

    public string GetFileUrl(string filePath)
    {
        // Para armazenamento local, retorna o caminho relativo
        // que será servido como arquivo estático pelo ASP.NET Core
        return $"/{filePath}";
    }

    public Task<bool> FileExistsAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return Task.FromResult(false);

        var caminhoCompleto = Path.Combine(_webRootPath, filePath);
        return Task.FromResult(File.Exists(caminhoCompleto));
    }
}
