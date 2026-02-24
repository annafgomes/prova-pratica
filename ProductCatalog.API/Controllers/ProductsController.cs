using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.DTOs;
using ProductCatalog.Application.UseCases.Products;

namespace ProductCatalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    /// <summary>
    /// Cria um novo produto
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromServices] CreateProductUseCase useCase,
        [FromBody] CreateProductHttpRequest request)
    {
        var applicationRequest = new ProductCatalog.Application.UseCases.Products.CreateProductRequest
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            Category = request.Category
        };

        var productId = await useCase.ExecuteAsync(applicationRequest);

        return CreatedAtAction(
            nameof(GetById),
            new { id = productId },
            null);
    }

    /// <summary>
    /// Retorna todos os produtos
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] GetAllProductsUseCase useCase)
    {
        var products = await useCase.ExecuteAsync();
        return Ok(products);
    }

    /// <summary>
    /// Retorna produto por id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromServices] GetProductByIdUseCase useCase,
        Guid id)
    {
        var product = await useCase.ExecuteAsync(id);
        return Ok(product);
    }

    /// <summary>
    /// Atualiza um produto existente
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromServices] UpdateProductUseCase useCase,
        Guid id,
        [FromBody] UpdateProductHttpRequest request)
    {
        await useCase.ExecuteAsync(
            id,
            request.Name,
            request.Description,
            request.Price,
            request.StockQuantity,
            request.Category);

        return NoContent();
    }

    /// <summary>
    /// Remove um produto existente
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromServices] DeleteProductUseCase useCase,
        Guid id)
    {
        await useCase.ExecuteAsync(id);
        return NoContent();
    }
}