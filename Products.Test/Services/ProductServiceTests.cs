using AutoMapper;
using Castle.Core.Logging;
using Moq;
using Products.Application.Interfaces;
using Products.Application.Services;
using Products.Domain.DTOs;
using Products.Domain.Entities;
using Xunit;

namespace Products.Test.Services
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger> _loggerMock;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _mapper = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger>();
        
            _productService = new ProductService(_productRepositoryMock.Object, _mapper.Object);
        }

        [Fact]
        public async Task CreateProductAsync_ShouldCallAddProduct_WhenProductIsValid()
        {
            // Arrange
            var productDTO = new ProductDTO { Name = "Test Product", Description = "Test Description", Price = 100 };
            var product = new Product { Name = productDTO.Name, Description = productDTO.Description, Price = productDTO.Price };

            _mapper.Setup(m => m.Map<Product>(It.IsAny<ProductDTO>())).Returns(product);

            _productRepositoryMock.Setup(r => r.AddProductAsync(It.IsAny<Product>()))
                                  .Returns(Task.CompletedTask);

            // Act
            await _productService.CreateProductAsync(productDTO);

            // Assert
            _productRepositoryMock.Verify(r => r.AddProductAsync(It.Is<Product>(p => p.Name == product.Name && p.Description == product.Description && p.Price == product.Price)), Times.Once);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnListOfProducts()
        {
            // Arrange
            var productEntities = new List<Product>
            {
                new Product { ProductId = 1, Name = "Sample Product" }
            };

            _productRepositoryMock.Setup(repo => repo.GetAllProductsAsync())
                .ReturnsAsync(productEntities);

            var productDTOs = productEntities.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                Name = p.Name
            }).ToList();

            _mapper.Setup(m => m.Map<IEnumerable<ProductDTO>>(It.IsAny<IEnumerable<Product>>()))
                .Returns(productDTOs);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal("Sample Product", result.First().Name);
        }
    }
}
