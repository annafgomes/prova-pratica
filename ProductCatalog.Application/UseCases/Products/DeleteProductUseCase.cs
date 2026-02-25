using System.IO;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.UseCases.Products;

/// <summary>
/// Caso de uso responsavel por deletar o produto e seu arquivo correspondente no storage
/// </summary>
public class DeleteProductUseCase
{
    private readonly IProductRepository _repository;
    private readonly IStorageService _storageService;

    /// <summary>
    /// Construtor para injecao do repositorio e do servico de armazenamento
    /// </summary>
    public DeleteProductUseCase(
        IProductRepository repository,
        IStorageService storageService)
    {
        _repository = repository;
        _storageService = storageService;
    }

    /// <summary>
    /// Executa a exclusao logica e fisica do produto e da imagem
    /// </summary>
    public async Task ExecuteAsync(Guid id)
    {
        /// <summary>
        /// Busca o produto e valida se o registro existe no banco de dados
        /// </summary>
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
        {
            throw new Exception("produto nao encontrado");
        }

        /// <summary>
        /// Valida o caminho da imagem e extrai o nome do arquivo para exclusao
        /// </summary>
        if (!string.IsNullOrWhiteSpace(product.ImagePath))
        {
            string fileName;

            if (product.ImagePath.StartsWith("http"))
            {
                var uri = new Uri(product.ImagePath);
                fileName = Path.GetFileName(uri.LocalPath);
            }
            else
            {
                fileName = product.ImagePath;
            }

            await _storageService.DeleteAsync(fileName);
        }

        /// <summary>
        /// Remove o registro do banco de dados e persiste as mudancas
        /// </summary>
        _repository.Remove(product);
        await _repository.SaveChangesAsync();
    }
}