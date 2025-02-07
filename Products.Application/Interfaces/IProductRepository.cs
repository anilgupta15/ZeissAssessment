using Products.Domain.Entities;

namespace Products.Application.Interfaces
{
    public interface IProductRepository
    {
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A collection of all products.</returns>
        Task<IEnumerable<Product>> GetAllProductsAsync();

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID.</returns>
        Task<Product> GetProductByIdAsync(int id);

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="product">The product to add.</param>
        Task AddProductAsync(Product product);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">The product to update.</param>
        Task UpdateProductAsync(Product product);

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        Task DeleteProductAsync(int id);

        /// <summary>
        /// Checks if a product exists by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to check.</param>
        /// <returns>True if the product exists; otherwise, false.</returns>
        Task<bool> ExistsProductAsync(int id);

        /// <summary>
        /// Decrements the stock of a product by a specified quantity.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <param name="quantity">The quantity to decrement.</param>
        /// <returns>True if the stock was decremented; otherwise, false.</returns>
        Task<bool> DecrementStockAsync(int id, int quantity);

        /// <summary>
        /// Adds to the stock of a product by a specified quantity.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <param name="quantity">The quantity to add.</param>
        /// <returns>True if the stock was added; otherwise, false.</returns>
        Task<bool> AddToStockAsync(int id, int quantity);
    }
}
