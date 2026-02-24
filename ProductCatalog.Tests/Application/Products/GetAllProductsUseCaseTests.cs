using Moq;
using ProductCatalog.Application.UseCases.Products;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using Xunit;

namespace ProductCatalog.Tests.Application.Products;

public class GetAllProductsUseCaseTests
{
    [Fact]
    public async Task Should_Return_List_Of_Products_When_Products_Exist()
    {
        // prepara lista simulada de produto
        var products = new List<Product>
        {
            new Product("Notebook", "Notebook Gamer", 3500m, 5, "Eletrônicos"),
            new Product("Mouse", "Mouse Gamer", 150m, 10, "Eletrônicos")
        };

        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(products);

        var useCase = new GetAllProductsUseCase(repositoryMock.Object);

        // executa o caso de uso
        var result = await useCase.ExecuteAsync();

        //  verifica se retornou corretamente
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }


    [Fact]
    public async Task Should_Return_Empty_List_When_No_Products_Exist()
    {
        // Arrange - prepara lista vazia simulada
        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Product>());

        var useCase = new GetAllProductsUseCase(repositoryMock.Object);

        // Act - executa o caso de uso
        var result = await useCase.ExecuteAsync();

        // Assert - verifica se a lista está vazia
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}