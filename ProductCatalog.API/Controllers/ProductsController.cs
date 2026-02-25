using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.DTOs;
using ProductCatalog.Application.UseCases.Products;
using ProductCatalog.Domain.Filters;
using ProductCatalog.Domain.Interfaces;

namespace ProductCatalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly DeleteProductImageUseCase _deleteProductImageUseCase;

    /// <summary>
    /// Construtor com injecao do caso de uso de delecao de imagem
    /// </summary>
    public ProductsController(DeleteProductImageUseCase deleteProductImageUseCase)
    {
        _deleteProductImageUseCase = deleteProductImageUseCase;
    }

    /// <summary>
    /// Cria um novo produto no catalogo
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

        return CreatedAtAction(nameof(GetById), new { id = productId }, null);
    }

    /// <summary>
    /// Realiza o upload ou a substituicao da imagem de um produto
    /// </summary>
    [HttpPost("{id}/image")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadImage(
        [FromServices] IStorageService storageService,
        [FromServices] UpdateProductImageUseCase updateImageUseCase,
        Guid id,
        IFormFile file)
    {
        /// <summary>
        /// Valida se o arquivo enviado e nulo ou vazio
        /// </summary>
        if (file == null || file.Length == 0)
        {
            return BadRequest("arquivo invalido");
        }

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        using var stream = file.OpenReadStream();

        /// <summary>
        /// Faz o upload para o storage e atualiza a referencia no banco
        /// </summary>
        var imagePath = await storageService.UploadAsync(
            stream,
            fileName,
            file.ContentType
        );

        await updateImageUseCase.ExecuteAsync(id, imagePath);

        return Ok(new { imagePath });
    }

    /// <summary>
    /// Remove a imagem vinculada ao produto
    /// </summary>
    [HttpDelete("{id}/image")]
    public async Task<IActionResult> DeleteImage(Guid id)
    {
        await _deleteProductImageUseCase.Execute(id);
        return NoContent();
    }

    /// <summary>
    /// Lista os produtos com suporte a filtros e paginacao
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromServices] GetAllProductsUseCase useCase,
        [FromQuery] GetProductsFilter filter)
    {
        var result = await useCase.ExecuteAsync(filter);
        return Ok(result);
    }

    /// <summary>
    /// Busca um produto especifico pelo seu identificador unico
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        [FromServices] GetProductByIdUseCase useCase,
        Guid id)
    {
        var product = await useCase.ExecuteAsync(id);
        return Ok(product);
    }

    /// <summary>
    /// Atualiza os dados principais de um produto
    /// </summary>
    [HttpPut("{id}")]
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
    /// Remove um produto permanentemente do sistema
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        [FromServices] DeleteProductUseCase useCase,
        Guid id)
    {
        await useCase.ExecuteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Ativa o produto para que fique visivel no catalogo
    /// </summary>
    [HttpPatch("{id}/activate")]
    public async Task<IActionResult> Activate(
        [FromServices] ActivateProductUseCase useCase,
        Guid id)
    {
        await useCase.ExecuteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Desativa o produto ocultando-o do catalogo
    /// </summary>
    [HttpPatch("{id}/deactivate")]
    public async Task<IActionResult> Deactivate(
        [FromServices] DeactivateProductUseCase useCase,
        Guid id)
    {
        await useCase.ExecuteAsync(id);
        return NoContent();
    }
}