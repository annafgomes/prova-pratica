using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Infrastructure.Repositories;

/// <summary>
/// Implementacao em memoria do repositorio de produtos
/// Usado inicialmente  para testes e desenvolvimento
/// </summary>
public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();

    public Task AddAsync(Product product)
    {
        _products.Add(product);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Product>>(_products);
    }

    public Task<Product?> GetByIdAsync(Guid id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }

    public Task UpdateAsync(Product product)
    {
        // Como esta em memoria o objeto ja esta atualizado
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Product product)
    {
        _products.Remove(product);
        return Task.CompletedTask;
    }
}