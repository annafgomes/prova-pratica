using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Middlewares;
using ProductCatalog.Application.UseCases.Products;
using ProductCatalog.Domain.Interfaces;
using ProductCatalog.Infrastructure.Services;
using ProductCatalog.Infrastructure.Persistence;
using ProductCatalog.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra o servico de armazenamento como singleton no container de dependencias

builder.Services.AddSingleton<IStorageService, MinioStorageService>();
//Registra a imagem
builder.Services.AddScoped<UpdateProductImageUseCase>();

// Banco de dados Postgres
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Application Use Cases
builder.Services.AddTransient<CreateProductUseCase>();
builder.Services.AddTransient<GetAllProductsUseCase>();
builder.Services.AddTransient<GetProductByIdUseCase>();
builder.Services.AddTransient<UpdateProductUseCase>();
builder.Services.AddTransient<DeleteProductUseCase>();
builder.Services.AddTransient<ActivateProductUseCase>();
builder.Services.AddTransient<DeactivateProductUseCase>();
builder.Services.AddScoped<DeleteProductImageUseCase>();


var app = builder.Build();

// Middleware Pipeline
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();
//app.UseHttpsRedirection(); - Desativar direcionamento do HTTPS
app.UseAuthorization();

app.MapControllers();

app.Run();