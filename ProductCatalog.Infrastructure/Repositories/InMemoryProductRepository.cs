using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.Infrastructure.Repositories;

/// <summary>
/// Implementação em memoria do repositorio de produtos
/// Utilizado para testes ou ambientes sem banco de dados
/// </summary>

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();

    /// <summary>
    /// Retorna um produto pelo ID
    /// </summary>

    public Task<Product?> GetByIdAsync(Guid id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);

        return Task.FromResult(product);
    }


    /// <summary>
    /// Retorna todos os produtos
    /// </summary>
    public Task<IEnumerable<Product>> GetAllAsync()

    {

        return Task.FromResult<IEnumerable<Product>>(_products);

    }
    /// <summary>
    /// Adiciona um novo produto
    /// </summary>

    public Task AddAsync(Product product)
    {
        _products.Add(product);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Marca um produto para atualização
    /// (Em memoria não precisa fazer nada)
    /// </summary>
   
    public void Update(Product product)
    {
        // Nada necessario para lista em memoria
    }

    /// <summary>
    /// Remove um produto
    /// </summary>
    public void Remove(Product product)
    {

        _products.Remove(product);
    }
    /// <summary>
    /// Persiste alterações
    /// (Em memória não precisa fazer nada)
    /// </summary>
    public Task SaveChangesAsync()
    {

        return Task.CompletedTask;

    }
}