namespace ProductCatalog.Domain.Entities;

/// <summary>
/// Representa um produto do catálogo.
/// Contém as regras de negócio relacionadas ao produto.
/// </summary>
public class Product
{
    // Identificador único do produto
    public Guid Id { get; private set; }

    // Nome do produto
    public string Name { get; private set; }

    // Descrição detalhada
    public string Description { get; private set; }

    // Preço do produto
    public decimal Price { get; private set; }

    // Quantidade disponível em estoque
    public int StockQuantity { get; private set; }

    // Categoria do produto (ex: Eletrônicos, Roupas)
    public string Category { get; private set; }

    // Indica se o produto está ativo para venda
    public bool IsActive { get; private set; }

    // Data de criação do produto
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Construtor responsável por criar um novo produto.
    /// Executa todas as validações necessárias.
    /// </summary>
    public Product(
        string name,
        string description,
        decimal price,
        int stockQuantity,
        string category)
    {
        Validate(name, price, stockQuantity, category);

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
        Category = category;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Valida as regras de negócio do produto.
    /// </summary>
    private void Validate(
        string name,
        decimal price,
        int stockQuantity,
        string category)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do produto não pode ser vazio.", nameof(name));

        if (price < 0)
            throw new ArgumentException("O preço não pode ser negativo.", nameof(price));

        if (stockQuantity < 0)
            throw new ArgumentException("O estoque não pode ser negativo.", nameof(stockQuantity));

        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("A categoria não pode ser vazia.", nameof(category));
    }

    /// <summary>
    /// Atualiza o preço do produto.
    /// </summary>
    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentException("O preço não pode ser negativo.", nameof(newPrice));

        Price = newPrice;
    }

    /// <summary>
    /// Atualiza a quantidade em estoque.
    /// </summary>
    public void UpdateStock(int newStock)
    {
        if (newStock < 0)
            throw new ArgumentException("O estoque não pode ser negativo.", nameof(newStock));

        StockQuantity = newStock;
    }

    /// <summary>
    /// Desativa o produto.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
    }
}