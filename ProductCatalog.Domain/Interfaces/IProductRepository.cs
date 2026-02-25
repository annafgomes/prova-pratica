using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Domain.Interfaces;

/// <summary>
/// Define o contrato de persistência para a entidade Product
/// 
/// Representa a abstração de acesso a dados
/// permitindo desacoplamento entre aplicação e infraestrutura
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Obtém um produto pelo seu identificador
    /// </summary>
    Task<Product?> GetByIdAsync(Guid id);

    /// <summary>
    /// Retorna todos os produtos cadastrados
    /// </summary>
    Task<IEnumerable<Product>> GetAllAsync();

    /// <summary>
    /// Adiciona um novo produto
    /// </summary>
    Task AddAsync(Product product);

    /// <summary>
    /// Marca um produto para atualização
    /// </summary>
    void Update(Product product);

    /// <summary>
    /// Marca um produto para remoção
    /// </summary>
    void Remove(Product product);

    /// <summary>
    /// Persiste as alterações no banco de dados
    /// </summary>
    Task SaveChangesAsync();
}