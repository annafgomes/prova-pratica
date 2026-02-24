using Moq;
using ProductCatalog.Application.UseCases.Products;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using Xunit;

namespace ProductCatalog.Tests.Application.Products;

public class CreateProductUseCaseTests
{
    [Fact]
    public async Task Should_Create_Product_When_DataIsValid()
    {
        // preparação do cenario
        var repositoryMock = new Mock<IProductRepository>();

        repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Product>()))
            .Returns(Task.CompletedTask);

        var useCase = new CreateProductUseCase(repositoryMock.Object);

        var request = new CreateProductRequest
        {
            Name = "Notebook",
            Description = "Notebook Gamer",
            Price = 3500m,
            StockQuantity = 5,
            Category = "Eletrônicos"
        };

        // Executa
        var result = await useCase.ExecuteAsync(request);

        // executa e verifica se lança exceção
        repositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
        Assert.NotEqual(Guid.Empty, result);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_NameIsEmpty()
    {
        // preparação do cenario
        var repositoryMock = new Mock<IProductRepository>();
        var useCase = new CreateProductUseCase(repositoryMock.Object);

        var request = new CreateProductRequest
        {
            Name = "",
            Description = "Produto teste",
            Price = 100,
            StockQuantity = 1,
            Category = "Teste"
        };

        // executa e verifica se lança exceção
        await Assert.ThrowsAsync<ArgumentException>(() =>
            useCase.ExecuteAsync(request));
    }

    [Fact]
    public async Task Should_Throw_Exception_When_Price_IsLessThanOrEqualZero()
    {
        // preparação do cenário
        var repositoryMock = new Mock<IProductRepository>();
        var useCase = new CreateProductUseCase(repositoryMock.Object);

        var request = new CreateProductRequest
        {
            Name = "Produto Teste",
            Description = "Descrição teste",
            Price = 0, // preço inválido
            StockQuantity = 10,
            Category = "Categoria Teste"
        };

        // executa e verifica se lança exceção
        await Assert.ThrowsAsync<ArgumentException>(() =>
            useCase.ExecuteAsync(request));
    }

    [Fact]
    public async Task Should_Not_Call_Repository_When_Data_Is_Invalid()
    {
        //  prepara cenário com preço inválido
        var repositoryMock = new Mock<IProductRepository>();
        var useCase = new CreateProductUseCase(repositoryMock.Object);

        var request = new CreateProductRequest
        {
            Name = "Produto Teste",
            Description = "Descrição teste",
            Price = 0, // inválido
            StockQuantity = 10,
            Category = "Categoria Teste"
        };

        //  executa e exceção
        await Assert.ThrowsAsync<ArgumentException>(() =>
            useCase.ExecuteAsync(request));

        // garante que o repositório nunca foi chamado
        repositoryMock.Verify(
            r => r.AddAsync(It.IsAny<Product>()),
            Times.Never);
    }
}


