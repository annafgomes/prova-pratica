using Moq;
using ProductCatalog.Application.UseCases.Products;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using Xunit;

namespace ProductCatalog.Tests.Application.Products;

public class UpdateProductUseCaseTests
{
    [Fact]
    public async Task Should_Update_Product_When_ProductExists()
    {
        // cria produto existente
        var product = new Product(
            "Notebook",
            "Descricao antiga",
            3000m,
            5,
            "Eletronicos"
        );

        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        var useCase = new UpdateProductUseCase(repositoryMock.Object);

        // executa atualizacao
        await useCase.ExecuteAsync(
            product.Id,
            "Notebook Novo",
            "Descricao nova",
            3500m,
            10,
            "Eletronicos"
        );

        // Verifica se valores foram alterados
        Assert.Equal("Notebook Novo", product.Name);
        Assert.Equal("Descricao nova", product.Description);
        Assert.Equal(3500m, product.Price);
        Assert.Equal(10, product.StockQuantity);
    }

    [Fact]
    public async Task Should_Call_UpdateAsync_When_ProductIsUpdated()
    {
        // cria produto existente
        var product = new Product(
            "Notebook",
            "Descricao antiga",
            3000m,
            5,
            "Eletronicos"
        );

        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        var useCase = new UpdateProductUseCase(repositoryMock.Object);

        // executa atualizacao
        await useCase.ExecuteAsync(
            product.Id,
            "Notebook Novo",
            "Descricao nova",
            3500m,
            10,
            "Eletronicos"
        );

        // verifica se o repositorio foi chamado
        repositoryMock.Verify(
            r => r.UpdateAsync(product),
            Times.Once);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_ProductDoesNotExist()
    {
        // repositorio retorna null
        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Product?)null);

        var useCase = new UpdateProductUseCase(repositoryMock.Object);

        // verifica se lanca excecao correta
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            useCase.ExecuteAsync(
                Guid.NewGuid(),
                "Nome",
                "Descricao",
                100m,
                5,
                "Categoria"
            ));
    }
}