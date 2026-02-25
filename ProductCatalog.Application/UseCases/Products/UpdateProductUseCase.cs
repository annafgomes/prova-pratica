using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Application.UseCases.Products;

/// <summary> 
/// Caso de uso responsavel  por atualizar um produto
/// </summary>
public class UpdateProductUseCase
{
    private readonly IProductRepository _repository;

    public UpdateProductUseCase(IProductRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Executa a atualizacao d o produto
    /// </summary>
    public async Task ExecuteAsync(Guid id, string name, string description, decimal price, int stockQuantity, string category)
    {
        // Busca o produto no repositorio
        var product = await _repository.GetByIdAsync(id);

        // Verifica se o produto existe
        if (product is null)
            throw new KeyNotFoundException("Product not found.");

        // Atualiza dados principais
        product.UpdateDetails(name, description, category);

        // Atualiza preco
        product.UpdatePrice(price);

        // Atualiza estoque
        product.UpdateStock(stockQuantity);

        // Salva a atualizacao
        _repository.Update(product);

        await _repository.SaveChangesAsync();


    }
}