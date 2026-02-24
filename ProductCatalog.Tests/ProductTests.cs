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
    #region Testes de Construtor

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
        Assert.Throws<ArgumentException>(() =>
            new Product("Notebook", "Desc", 0m, 10, "Electronics"));
    }

    /// <summary>
    /// Deve lançar exceção quando o estoque for negativo.
    /// </summary>
    [Fact]
    public void CreateProduct_WithNegativeStock_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() =>
            new Product("Notebook", "Desc", 100m, -1, "Electronics"));
    }

    #endregion

    #region Testes de Regras de Negócio

    /// <summary>
    /// Deve alterar o status para Inactive ao desativar o produto.
    /// </summary>
    /// 
    [Fact]
    public void Deactivate_ShouldChangeStatusToInactive()
    {
        // Arrange
        var product = new Product("Notebook", "Desc", 1000m, 5, "Electronics");

        // Act
        product.Deactivate();

        // Assert
        Assert.Equal(ProductStatus.Inactive, product.Status);
    }

    /// <summary>
    /// Deve alterar o status para Active ao ativar o produto.
    /// </summary>
    [Fact]
    public void Activate_ShouldChangeStatusToActive()
    {
        // Arrange
        var product = new Product("Notebook", "Desc", 1000m, 5, "Electronics");
        product.Deactivate();

        // Act
        product.Activate();

        // Assert
        Assert.Equal(ProductStatus.Active, product.Status);
    }

    /// <summary>
    /// Deve atualizar o preço  quando o novo valor for válido.
    /// </summary>
    [Fact]
    public void UpdatePrice_WithValidPrice_ShouldUpdatePrice()
    {
        // Arrange
        var product = new Product("Notebook", "Desc", 1000m, 5, "Electronics");

        // Act
        product.UpdatePrice(2000m);

        // Assert
        Assert.Equal(2000m, product.Price);
    }

    /// <summary>
    /// Deve lançar exceção quando tentar atualizar para um preço inválido.
    /// </summary>
    [Fact]

    public void UpdatePrice_WithInvalidPrice_ShouldThrowException()
    {
        // Arrange
        var product = new Product("Notebook", "Desc", 1000m, 5, "Electronics");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => product.UpdatePrice(0));
    }

    #endregion
}