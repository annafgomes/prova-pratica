using ProductCatalog.Domain.Interfaces;



namespace ProductCatalog.Application.UseCases.Products;

/// <summary>
/// Torna o produto ativo
/// </summary>

public class ActivateProductUseCase

{

    private readonly IProductRepository _repository;

    public ActivateProductUseCase(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(Guid id)

    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)

            throw new KeyNotFoundException("Product not found.");

        product.Activate();

        _repository.Update(product);

        await _repository.SaveChangesAsync();

    }

}