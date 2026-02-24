namespace ProductCatalog.Application.UseCases.Products;

/// <summary>
/// DTO responsável por transportar os dados necessários
/// para criação de um produto
/// </summary>
public class CreateProductRequest
{
    /// <summary>
    /// Nome do produto
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Descrição do produto.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    /// Preço de venda
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Quantidade em estoque
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Categoria do produto.
    /// </summary>
    public string Category { get; set; } = default!;
}