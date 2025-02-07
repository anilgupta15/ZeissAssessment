using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products.Application.Interfaces;
using Products.Database.DataContext;
using Products.Domain.Entities;

namespace Products.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly AppDbContext _context;
        private readonly ICacheService _cache;
        private const string CacheKey = "Product_";

        public ProductRepository(AppDbContext context, ILogger<ProductRepository> logger, ICacheService cache)
        {
            _context = context;
            _logger = logger;
            _cache = cache;
        }

        /// <summary>
        /// Retrieves all products from the database.
        /// </summary>
        /// <returns>A collection of all products.</returns>
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all products");
                throw;
            }
        }

        /// <summary>
        /// Retrieves a product by its ID, with caching support.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID.</returns>
        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                var cachedProduct = await _cache.GetCachedItemAsync<Product>(CacheKey + id);
                if (cachedProduct != null) return cachedProduct;

                var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
                if (product != null)
                {
                    await _cache.SetCachedItemAsync(CacheKey + id, product, TimeSpan.FromMinutes(5));
                }
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching product by ID: {id}");
                throw;
            }
        }

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="product">The product to add.</param>
        public async Task AddProductAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a product");
                throw;
            }
        }

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="product">The product to update.</param>
        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                var existingProduct = await _context.Products.FindAsync(product.ProductId);

                if (existingProduct == null)
                {
                    _logger.LogWarning($"Product with ID {product.ProductId} not found.");
                    return;
                }
                _context.Entry(existingProduct).CurrentValues.SetValues(product);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating product with ID: {product.ProductId}");
                throw;
            }
        }


        /// <summary>
        /// Deletes a product by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        public async Task DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting product with ID: {id}");
                throw;
            }
        }

        /// <summary>
        /// Checks if a product exists by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to check.</param>
        /// <returns>True if the product exists; otherwise, false.</returns>
        public async Task<bool> ExistsProductAsync(int id)
        {
            try
            {
                return await _context.Products.AsNoTracking().AnyAsync(p => p.ProductId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while checking if product with ID: {id} exists");
                throw;
            }
        }

        /// <summary>
        /// Decrements the stock of a product by a specified quantity.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <param name="quantity">The quantity to decrement.</param>
        /// <returns>True if the stock was decremented; otherwise, false.</returns>
        public async Task<bool> DecrementStockAsync(int id, int quantity)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null || product.StockAvailable < quantity)
                    return false;

                product.StockAvailable -= quantity;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while decrementing stock for product with ID: {id}");
                throw;
            }
        }

        /// <summary>
        /// Adds to the stock of a product by a specified quantity.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <param name="quantity">The quantity to add.</param>
        /// <returns>True if the stock was added; otherwise, false.</returns>
        public async Task<bool> AddToStockAsync(int id, int quantity)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                    return false;

                product.StockAvailable += quantity;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding stock for product with ID: {id}");
                throw;
            }
        }
    }
}
