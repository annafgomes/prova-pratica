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
    public async Task<IActionResult> GetById(
    [FromServices] GetProductByIdUseCase useCase,
    Guid id)
    {
        try
        {
            var product = await useCase.ExecuteAsync(id);
            return Ok(product);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}