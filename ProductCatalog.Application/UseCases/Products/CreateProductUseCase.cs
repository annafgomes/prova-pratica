using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.UseCases.Products;

/// <summary>
/// Caso de uso responsável por criar um novo  produto
/// </summary>
public class CreateProductUseCase
{
    private readonly IProductRepository _repository;

    public CreateProductUseCase(IProductRepository repository)
    {
        _repository = repository;
    }



    /// <summary>
    /// Executa a criação de um novo produto
    /// </summary>
    public async Task<Guid> ExecuteAsync(CreateProductRequest request)
    {
        var product = new Product(
            request.Name,
            request.Description,
            request.Price,
            request.StockQuantity,
            request.Category);

        await _repository.AddAsync(product);

        await _repository.SaveChangesAsync();

        return product.Id;
    }
}