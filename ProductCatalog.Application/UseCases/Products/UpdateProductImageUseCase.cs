using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.UseCases.Products;

/// <summary>
/// Caso de uso responsavel por atualizar imagem do produto e limpar arquivos antigos
/// </summary>
public class UpdateProductImageUseCase
{
    private readonly IProductRepository _repository;
    private readonly IStorageService _storageService;

    /// <summary>
    /// Construtor para injecao do repositorio e do servico de armazenamento
    /// </summary>
    public UpdateProductImageUseCase(
        IProductRepository repository,
        IStorageService storageService)
    {
        _repository = repository;
        _storageService = storageService;
    }

    /// <summary>
    /// Executa a atualizacao da imagem removendo a anterior se existir
    /// </summary>
    public async Task ExecuteAsync(Guid id, string imagePath)
    {
        /// <summary>
        /// Busca o produto no banco de dados e valida se ele existe
        /// </summary>
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
        {
            throw new Exception("produto nao encontrado");
        }

        /// <summary>
        /// Verifica se ja existe uma imagem vinculada para deletar do storage
        /// </summary>
        if (!string.IsNullOrEmpty(product.ImagePath))
        {
            await _storageService.DeleteAsync(product.ImagePath);
        }

        /// <summary>
        /// Atualiza a entidade com o novo caminho e persiste no banco
        /// </summary>
        product.UpdateImage(imagePath);

        _repository.Update(product);
        await _repository.SaveChangesAsync();
    }
}