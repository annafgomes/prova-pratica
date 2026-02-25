using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.DTOs;
using ProductCatalog.Application.UseCases.Products;
using ProductCatalog.Domain.Filters;
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
    public async Task<IActionResult> Get(
    [FromServices] GetAllProductsUseCase useCase,
    [FromQuery] GetProductsFilter filter)
    {
        var result = await useCase.ExecuteAsync(filter);
        return Ok(result);
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
    /// <summary>

    /// Ativa um produto

    /// </summary>

    [HttpPatch("{id}/activate")]

    [ProducesResponseType(StatusCodes.Status204NoContent)]

    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Activate(

        [FromServices] ActivateProductUseCase useCase,

        Guid id)

    {

        await useCase.ExecuteAsync(id);

        return NoContent();

    }



    /// <summary>

    /// Desativa um produto

    /// </summary>

    [HttpPatch("{id}/deactivate")]

    [ProducesResponseType(StatusCodes.Status204NoContent)]

    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Deactivate(

        [FromServices] DeactivateProductUseCase useCase,

        Guid id)

    {

        await useCase.ExecuteAsync(id);

        return NoContent();

    }
}