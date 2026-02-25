using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.UseCases.Products;

/// <summary>
/// Caso de uso responsavel  por remover um produto
/// </summary>
public class DeleteProductUseCase
{
    private readonly IProductRepository _repository;

    public DeleteProductUseCase(IProductRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Executa a remocao do  produto
    /// </summary>
    public async Task ExecuteAsync(Guid id)
    {
        // Busca o produto no repositorio
        var product = await _repository.GetByIdAsync(id);

        // Verifica se o produto existe
        if (product is null)
            throw new KeyNotFoundException("Product not found.");

        // Remove o  produto
        _repository.Remove(product);

        await _repository.SaveChangesAsync();
    }
}