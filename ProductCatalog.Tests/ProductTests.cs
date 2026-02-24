using Xunit;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Enums;

namespace ProductCatalog.Tests;

/// <summary>
/// Testes unitários da entidade Product.
/// Garante que as regras de negócio do domínio estejam protegidas.
/// </summary>
public class ProductTests
{
    /// <summary>
    /// Deve criar um produto ativo quando os dados forem válidos.
    /// </summary>
    [Fact]
    public void CreateProduct_WithValidData_ShouldSucceed()
    {
        // Arrange
        var name = "Notebook";
        var description = "High performance notebook";
        var price = 3500m;
        var stock = 10;
        var category = "Electronics";

        // Act
        var product = new Product(name, description, price, stock, category);

        // Assert
        Assert.Equal(name, product.Name);
        Assert.Equal(description, product.Description);
        Assert.Equal(price, product.Price);
        Assert.Equal(stock, product.StockQuantity);
        Assert.Equal(category, product.Category);
        Assert.Equal(ProductStatus.Active, product.Status);
        Assert.NotEqual(Guid.Empty, product.Id);
    }

    /// <summary>
    /// Deve lançar exceção quando o preço for inválido.
    /// </summary>
    [Fact]
    public void CreateProduct_WithInvalidPrice_ShouldThrowException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Product("Notebook", "Desc", 0m, 10, "Electronics"));
    }

    /// <summary>
    /// Deve lançar exceção quando o estoque for negativo.
    /// </summary>
    [Fact]
    public void CreateProduct_WithNegativeStock_ShouldThrowException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Product("Notebook", "Desc", 100m, -1, "Electronics"));
    }
}