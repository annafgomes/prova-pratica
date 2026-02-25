using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure.Persistence;

/// <summary>
/// Contexto do banco de dados da aplicação
/// Responsavel por mapear as entidades para o banco via EF Core
/// </summary>
public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name)
                  .IsRequired()
                  .HasMaxLength(200);
            entity.Property(p => p.Description)
                  .HasMaxLength(500);
            entity.Property(p => p.Category)
                  .HasMaxLength(100);
            entity.Property(p => p.Price)
                  .HasColumnType("decimal(18,2)");
            entity.Property(p => p.StockQuantity)
                  .IsRequired();
        });
        base.OnModelCreating(modelBuilder);
    }
}