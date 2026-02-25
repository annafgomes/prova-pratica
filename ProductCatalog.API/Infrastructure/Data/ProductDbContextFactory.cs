using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Design;

using Microsoft.Extensions.Configuration;

using System.IO;



namespace ProductCatalog.Infrastructure.Data;



/// <summary>

/// Factory responsavel por criar instancias do ProductDbContext
/// em tempo de design (para migrations)
/// Necessaria para que o Entity Framework consiga criar o contexto
/// quando executamos comandos como "dotnet ef migrations add"

/// </summary>

public class ProductDbContextFactory

    : IDesignTimeDbContextFactory<ProductDbContext>

{

    /// <summary>

    /// Cria manualmente uma instância do ProductDbContext
    /// utilizando a connection string definida no appsettings.json
    /// </summary>
    /// Argumentos opcionais de execucao
    /// Instancia configurada do ProductDbContext

    public ProductDbContext CreateDbContext(string[] args)

    {

        // Obtém o diretorio atual do projeto

        var basePath = Directory.GetCurrentDirectory();



        // Carrega as configuracoes do arquivo appsettings.json

        var configuration = new ConfigurationBuilder()

            .SetBasePath(basePath)

            .AddJsonFile("appsettings.json")

            .Build();



        // Configura as opcoes do DbContext

        var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();



        // Define o uso do PostgreSQL com a connection string configurada

        optionsBuilder.UseNpgsql(

            configuration.GetConnectionString("DefaultConnection"));



        // Retorna a instancia do contexto configurado

        return new ProductDbContext(optionsBuilder.Options);

    }

}