using AutoMapper;
using Products.Application.Interfaces;
using Products.Domain.DTOs;
using Products.Domain.Entities;

namespace Products.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A collection of all products.</returns>
        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _repository.GetAllProductsAsync();
            if (products == null)
            {
                return new List<ProductDTO>();
            }
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }


        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID.</returns>
        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return null;
            }
            return _mapper.Map<ProductDTO>(product);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productDto">The product to create.</param>
        public async Task<Product> CreateProductAsync(CreateProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _repository.AddProductAsync(product); 
            return product; 
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="productDto">The product to update.</param>
        public async Task UpdateProductAsync(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _repository.UpdateProductAsync(product);
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        public async Task DeleteProductAsync(int id)
            => await _repository.DeleteProductAsync(id);

        /// <summary>
        /// Decrements the stock of a product by a specified quantity.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <param name="quantity">The quantity to decrement.</param>
        /// <returns>True if the stock was decremented; otherwise, false.</returns>
        public async Task<bool> DecrementStockAsync(int id, int quantity)
            => await _repository.DecrementStockAsync(id, quantity);

        /// <summary>
        /// Adds to the stock of a product by a specified quantity.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <param name="quantity">The quantity to add.</param>
        /// <returns>True if the stock was added; otherwise, false.</returns>
        public async Task<bool> AddToStockAsync(int id, int quantity)
            => await _repository.AddToStockAsync(id, quantity);
    }
}
