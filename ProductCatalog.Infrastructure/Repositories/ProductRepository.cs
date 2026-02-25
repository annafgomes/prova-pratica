using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Enums;
using ProductCatalog.Domain.Filters;
using ProductCatalog.Domain.Interfaces;
using ProductCatalog.Infrastructure.Persistence;

namespace ProductCatalog.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório de produtos usando Entity Framework
/// Responsável por realizar operações de acesso a dados na tabela Products
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    /// <summary>
    /// Construtor que recebe o DbContext via injeção de dependência.
    /// </summary>
    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna um produto pelo seu Id
    /// </summary>
    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }

    /// <summary>
    /// Retorna todos os produtos cadastrados.
    /// </summary>
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }
    /// <summary>
    /// Retorna produtos filtrados
    /// </summary>
    public async Task<IEnumerable<Product>> GetFilteredAsync(GetProductsFilter filter)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Category))
        {
            query = query.Where(p => p.Category == filter.Category);
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= filter.MaxPrice.Value);
        }

        if (filter.Status.HasValue)
        {
            query = query.Where(p => p.Status == filter.Status.Value);
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// Adiciona um novo produto ao contexto.
    /// A persistência ocorre somente após SaveChangesAsync.
    /// </summary>

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    /// <summary>
    /// Marca um produto como modificado
    /// A alteração será salva após SaveChangesAsync
    /// </summary>
   
    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    /// <summary>
    /// Marca um produto para remoção
    /// A exclusão sera efetivada apos SaveChangesAsync
    /// </summary>
    /// <param name="product">Produto a ser removido</param>
    public void Remove(Product product)
    {
        _context.Products.Remove(product);
    }

    /// <summary>
    /// Persiste todas as alterações pendentes no banco de dados
    /// </summary>
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
}