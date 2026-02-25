using Moq;
using ProductCatalog.Application.UseCases.Products;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using Xunit;

namespace ProductCatalog.Tests.Application.Products;

public class DeleteProductUseCaseTests
{
    [Fact]
    public async Task Should_Delete_Product_When_Product_Exists()
    {
        // cria produto existente
        var product = new Product(
            "Notebook",
            "Descricao",
            3000m,
            5,
            "Eletronicos"
        );

        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(r => r.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        var useCase = new DeleteProductUseCase(repositoryMock.Object);

        // executa remocao
        await useCase.ExecuteAsync(product.Id);

        // verifica se DeleteAsync foi chamado
        repositoryMock.Verify(
            r => r.Remove(product),
            Times.Once);

        repositoryMock.Verify(
            r => r.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_Product_Does_Not_Exist()
    {
        // repositorio retorna null
        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Product?)null);

        var useCase = new DeleteProductUseCase(repositoryMock.Object);

        // verifica se lanca excecao correta
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            useCase.ExecuteAsync(Guid.NewGuid()));
    }
}