using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.UseCases.Products;

/// <summary>
/// Caso de uso responsável por listar todos os produtos.
/// </summary>
public class GetAllProductsUseCase
{
    private readonly IProductRepository _repository;

    public GetAllProductsUseCase(IProductRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Executa a listagem de produtos.
    /// </summary>
    public async Task<IEnumerable<Product>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}