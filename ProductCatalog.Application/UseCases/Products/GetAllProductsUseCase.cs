using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using ProductCatalog.Domain.Filters;

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
    /// Executa listagem com filtro
    /// </summary>
    public async Task<IEnumerable<Product>> ExecuteAsync(GetProductsFilter filter)
    {
        return await _repository.GetFilteredAsync(filter);
    }
}