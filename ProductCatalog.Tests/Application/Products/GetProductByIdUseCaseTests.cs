using Moq;
using ProductCatalog.Application.UseCases.Products;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using Xunit;

namespace ProductCatalog.Tests.Application.Products;

public class GetProductByIdUseCaseTests
{
    [Fact]
    public async Task Should_Return_Product_When_Product_Exists()
    {
        // prepara  produto simulado
        var product = new Product(
            "Notebook",
            "Notebook Gamer",
            3500m,
            5,
            "Eletronicos"
        );

        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        var useCase = new GetProductByIdUseCase(repositoryMock.Object);

        // executa o caso de  uso
        var result = await useCase.ExecuteAsync(product.Id);

        // verifica se retornou corretamente
        Assert.NotNull(result);
        Assert.Equal(product.Id, result.Id);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_Product_Does_Not_Exist()
    {
        // prepara  repositorio retornando nul
        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Product?)null);

        var useCase = new GetProductByIdUseCase(repositoryMock.Object);

        var id = Guid.NewGuid();

        // verifica se lanca  excecao correta
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            useCase.ExecuteAsync(id));
    }
}