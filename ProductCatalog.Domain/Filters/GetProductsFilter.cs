using ProductCatalog.Domain.Enums;

namespace ProductCatalog.Domain.Filters;

/// <summary>
/// Filtro para consulta de produtos
/// </summary>
public class GetProductsFilter
{
    /// <summary>
    /// Categoria do produto
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Preco minimo
    /// </summary>
    public decimal? MinPrice { get; set; }

    /// <summary>
    /// Preco maximo
    /// </summary>
    public decimal? MaxPrice { get; set; }

    /// <summary>
    /// Status do produto
    /// </summary>
    public ProductStatus? Status { get; set; }
}