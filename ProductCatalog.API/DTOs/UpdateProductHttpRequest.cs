namespace ProductCatalog.API.DTOs
{
    /// <summary>
    /// DTO para atualizar um produto
    /// </summary>
    public class UpdateProductHttpRequest
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Category { get; set; } = default!;
    }
}