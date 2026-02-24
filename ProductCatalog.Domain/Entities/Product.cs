using ProductCatalog.Domain.Enums;

namespace ProductCatalog.Domain.Entities;

/// <summary>
/// Representa um produto do  catálogo.
/// Contém as regras de negócio relacionadas ao comportamento do  produto.
/// </summary>
public class Product
{
    /// <summary>
    /// Identificador único do produto.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Nome do produto.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Descrição detalhada  do produto.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Preço de venda do produto.
    /// </summary>
    public decimal Price { get; private set; }

    /// <summary>
    /// Quantidade disponível em estoque.
    /// </summary>
    public int StockQuantity { get; private set; }

    /// <summary>
    /// Categoria  à qual o produto pertence.
    /// </summary>
    public string Category { get; private set; }

    /// <summary>
    /// Status atual do produto (Ativo ou Inativo).
    /// </summary>
    public ProductStatus Status { get; private set; }

    /// <summary>
    /// Caminho  da imagem associada ao produto.
    /// </summary>
    public string? ImagePath { get; private set; }

    /// <summary>
    /// Data de criação do produto.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Construtor protegido necessário para o Entity Framework.
    /// </summary>
    protected Product() { }

    /// <summary>
    /// Construtor responsável por criar um novo produto,
    /// aplicando todas as validações de negócio.
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
        Status = ProductStatus.Active;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Valida as  regras de negócio obrigatórias para o produto.
    /// </summary>
    private void Validate(
        string name,
        decimal price,
        int stockQuantity,
        string category)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        if (price <= 0)
            throw new  ArgumentException("Price must be greater than zero.", nameof(price));

        if (stockQuantity < 0)
            throw new ArgumentException("Stock quantity cannot be negative.", nameof(stockQuantity));

        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category is required.", nameof(category));
    }
     
    /// <summary>
    /// Atualiza informações principais do produto.
    /// </summary>
    public void UpdateDetails(string name, string description, string category)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category is required.", nameof(category));

        Name = name;
        Description = description;
        Category = category;
    }

    /// <summary>
    /// Atualiza o preço do produto.
    /// </summary>
    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("Price must be greater than zero.", nameof(newPrice));

        Price = newPrice;
    }

    /// <summary>
    /// Atualiza a quantidade em estoque.
    /// </summary>
    public void UpdateStock(int newStock)
    {
        if (newStock < 0)
            throw new ArgumentException("Stock quantity cannot be negative.", nameof(newStock));

        StockQuantity = newStock;
    }

    /// <summary>
    /// Define ou altera o caminho da imagem do produto.
    /// </summary>
    public void SetImage(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
            throw new ArgumentException("Image path is invalid.", nameof(imagePath));

        ImagePath = imagePath;
    }

    /// <summary>
    /// Ativa o produto para venda.
    /// </summary>
    public void Activate()
    {
        Status = ProductStatus.Active;
    }

    /// <summary>
    /// Desativa o produto para venda .
    /// </summary>
    public void Deactivate()
    {
        Status = ProductStatus.Inactive;
    }
}