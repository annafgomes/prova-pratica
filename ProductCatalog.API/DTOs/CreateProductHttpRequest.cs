namespace ProductCatalog.API.DTOs;

/// <summary>
/// DTO para criacao de produto
/// </summary>



public class CreateProductHttpRequest
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Category { get; set; } = null!;
}