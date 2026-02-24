using ProductCatalog.Domain.Interfaces;
using ProductCatalog.Infrastructure.Repositories;
using ProductCatalog.Application.UseCases.Products;
using ProductCatalog.API.Middlewares;
var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddTransient<CreateProductUseCase>();
builder.Services.AddTransient<GetAllProductsUseCase>();
builder.Services.AddTransient<GetProductByIdUseCase>();
builder.Services.AddTransient<UpdateProductUseCase>();
builder.Services.AddTransient<DeleteProductUseCase>();
builder.Services.AddTransient<ActivateProductUseCase>();
builder.Services.AddTransient<DeactivateProductUseCase>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
//app.UseMiddleware<ProductCatalog.API.Middlewares.ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();