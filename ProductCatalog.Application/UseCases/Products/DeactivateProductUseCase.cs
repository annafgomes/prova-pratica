using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.UseCases.Products;

/// <summary>
/// Desativa produto
/// </summary>
public class DeactivateProductUseCase

{

    private readonly IProductRepository _repository;

    public DeactivateProductUseCase(IProductRepository repository)

    {
        _repository = repository;
    }

    public async Task ExecuteAsync(Guid id)

    {

        var product = await _repository.GetByIdAsync(id);

        if (product == null)

            throw new KeyNotFoundException("Product not found.");

        product.Deactivate();

        _repository.Update(product);

        await _repository.SaveChangesAsync();

    }

}

