using Products.Application.Interfaces;
using Products.Application.Services;
using Products.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Products.Middleware;
using Products.Database.DataContext;
using Products.Infrastructure.Services;
using FluentValidation.AspNetCore;
using Products.Domain.DTOs;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);

// Register AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ZeissDBConnection")));

// Register IMemoryCache
builder.Services.AddMemoryCache();

// Register Logger
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

// Add Controllers (only once)
builder.Services.AddControllers();

// Register FluentValidation
builder.Services.AddFluentValidationAutoValidation();

// Configure AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register Repositories and Services
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IValidator<ProductDTO>, ProductDTOValidator>();
builder.Services.AddScoped<IValidator<CreateProductDTO>, CreateProductDTOValidator>();


// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use middleware in the correct order
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Enable Swagger only in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
