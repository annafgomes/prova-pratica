using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using ProductCatalog.Infrastructure.Persistence;


namespace ProductCatalog.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório de produtos usando Entity Framework.
/// </summary>

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;
    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }
    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }
    public void Update(Product product)
    {
        _context.Products.Update(product);
    }
    public void Remove(Product product)
    {
        _context.Products.Remove(product);
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}