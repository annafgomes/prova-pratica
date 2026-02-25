using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.UseCases.Products;

/// <summary>
/// Caso de uso responsavel por remover a imagem do produto
/// </summary>
public class DeleteProductImageUseCase
{
    private readonly IProductRepository _repository;
    private readonly IStorageService _storageService;

    /// <summary>
    /// Construtor com a injecao das dependencias de repositorio e storage
    /// </summary>
    public DeleteProductImageUseCase(
        IProductRepository repository,
        IStorageService storageService)
    {
        _repository = repository;
        _storageService = storageService;
    }

    /// <summary>
    /// Executa o processo de exclusao fisica no storage e logica no banco de dados
    /// </summary>
    public async Task Execute(Guid id)
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
        /// Verifica se existe caminho de imagem para realizar a remocao
        /// </summary>
        if (!string.IsNullOrEmpty(product.ImagePath))
        {
            await _storageService.DeleteAsync(product.ImagePath);

            /// <summary>
            /// Limpa a referencia da imagem na entidade e persiste no banco
            /// </summary>
            product.UpdateImage(null);

            _repository.Update(product);
            await _repository.SaveChangesAsync();
        }
    }
}