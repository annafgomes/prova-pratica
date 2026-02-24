using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.UseCases.Products;

/// <summary>
/// Caso de uso responsavel por buscar um produto pelo Id
/// </summary>
public class GetProductByIdUseCase
{
    private readonly IProductRepository _repository;

    public GetProductByIdUseCase(IProductRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Executa a busca de produto pelo  Id
    /// </summary>
    public async Task<Product> ExecuteAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product is null)
            throw new KeyNotFoundException("Product not found.");

        return product;
    }
}