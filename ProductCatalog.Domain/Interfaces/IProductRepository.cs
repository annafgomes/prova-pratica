using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Filters;

namespace ProductCatalog.Domain.Interfaces;

/// <summary>
/// Define o contrato de persistência para a entidade Product
/// 
/// Representa a abstraction de acesso a dados
/// permitindo desacoplamento entre aplicação e infraestrutura
/// 
/// Neste modelo, o repositorio apenas registra as alterações
/// A persistência efetiva ocorre quando SaveChangesAsync é chamado
/// normalmente pela camada Application (UseCases )
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Obtem um produto pelo seu identificador
    /// </summary>
    Task<Product?> GetByIdAsync(Guid id);

    /// <summary>
    /// Retorna todos os produtos cadastrados
    /// </summary>
    /// <returns>Lista de produtos</returns>
    Task<IEnumerable<Product>> GetAllAsync();
   
    /// <summary>
    /// Retorna produtos filtrados
    /// </summary>
    Task<IEnumerable<Product>> GetFilteredAsync(GetProductsFilter filter);
    /// <summary>
    /// Adiciona um novo produto ao contexto
    /// (ainda não persiste no banco)
    /// </summary>

    Task AddAsync(Product product);

    /// <summary>
    /// Marca um produto para atualização
    /// (ainda não persiste no banco)
    /// </summary>
    void Update(Product product);

    /// <summary>
    /// Marca um produto para remoção
    /// (ainda não persiste no banco)
    /// </summary>
    void Remove(Product product);

    /// <summary>
    /// Persiste todas as alteraçoes pendentes no banco de dados
    /// </summary>
    Task SaveChangesAsync();


}