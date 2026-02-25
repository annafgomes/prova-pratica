using Moq;
using ProductCatalog.Application.UseCases.Products;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using Xunit;

namespace ProductCatalog.Tests.Application.Products;

/// <summary>
/// Classe de testes para o caso de uso de exclusao de produto
/// </summary>
public class DeleteProductUseCaseTests
{
    /// <summary>
    /// Testa se o produto e removido corretamente quando o id existe
    /// </summary>
    [Fact]
    public async Task Should_Delete_Product_When_Product_Exists()
    {
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

        var storageMock = new Mock<IStorageService>();

        var useCase = new DeleteProductUseCase(
            repositoryMock.Object,
            storageMock.Object);

        /// <summary>
        /// Executa o caso de uso e verifica as chamadas no repositorio
        /// </summary>
        await useCase.ExecuteAsync(product.Id);

        repositoryMock.Verify(r => r.Remove(product), Times.Once);
        repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    /// <summary>
    /// Testa se uma excecao e lancada quando o produto nao e encontrado
    /// </summary>
    [Fact]
    public async Task Should_Throw_Exception_When_Product_Does_Not_Exist()
    {
        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Product?)null);

        var storageMock = new Mock<IStorageService>();

        var useCase = new DeleteProductUseCase(
            repositoryMock.Object,
            storageMock.Object);

        /// <summary>
        /// Valida se a excecao esperada e disparada
        /// </summary>
        await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(Guid.NewGuid()));
    }
}