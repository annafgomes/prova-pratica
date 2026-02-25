using Microsoft.EntityFrameworkCore;

using ProductCatalog.Domain.Entities; // ajuste para seu namespace



namespace ProductCatalog.Infrastructure.Data;



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



            entity.Property(p => p.Price)

                  .HasColumnType("decimal(18,2)");

        });

    }

}

