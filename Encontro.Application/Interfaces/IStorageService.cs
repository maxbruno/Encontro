namespace Encontro.Application.Interfaces;

/// <summary>
/// Interface para serviços de armazenamento de arquivos.
/// Permite implementações para diferentes provedores (local, S3, Azure Blob Storage, etc.)
/// </summary>
public interface IStorageService
{
    /// <summary>
    /// Salva um arquivo no armazenamento
    /// </summary>
    /// <param name="fileName">Nome do arquivo (incluindo extensão)</param>
    /// <param name="stream">Stream com o conteúdo do arquivo</param>
    /// <param name="contentType">Tipo de conteúdo (MIME type)</param>
    /// <returns>Caminho/URL relativo do arquivo salvo</returns>
    Task<string> SaveFileAsync(string fileName, Stream stream, string contentType);

    /// <summary>
    /// Exclui um arquivo do armazenamento
    /// </summary>
    /// <param name="filePath">Caminho/URL relativo do arquivo</param>
    Task DeleteFileAsync(string filePath);

    /// <summary>
    /// Obtém a URL completa para acessar o arquivo
    /// </summary>
    /// <param name="filePath">Caminho/URL relativo do arquivo</param>
    /// <returns>URL completa para acessar o arquivo</returns>
    string GetFileUrl(string filePath);

    /// <summary>
    /// Verifica se um arquivo existe
    /// </summary>
    /// <param name="filePath">Caminho/URL relativo do arquivo</param>
    /// <returns>True se o arquivo existe</returns>
    Task<bool> FileExistsAsync(string filePath);
}
