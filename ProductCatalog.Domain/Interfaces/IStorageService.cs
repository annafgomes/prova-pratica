namespace ProductCatalog.Domain.Interfaces;

/// <summary>
/// Contrato para servico de armazenamento de arquivos
/// </summary>
public interface IStorageService
{
    /// <summary>
    /// Realiza o upload de um arquivo para o storage
    /// </summary>
 
    Task<string> UploadAsync(Stream fileStream, string fileName, string contentType);
    Task DeleteAsync(string fileName);
}