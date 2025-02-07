using Microsoft.AspNetCore.Mvc;
using Products.Application.Interfaces;
using Products.Domain.DTOs;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product data to create.</param>
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO product)
        {
            var createdProduct = await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(CreateProduct), new { id = createdProduct.ProductId }, createdProduct);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product data.</param>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO product)
        {
            if (id != product.ProductId) return BadRequest();

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Decrements the stock of a product by a specified quantity.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <param name="quantity">The quantity to decrement.</param>
        [HttpPut("decrement-stock/{id}/{quantity}")]
        public async Task<IActionResult> DecrementStock(int id, int quantity)
        {
            var result = await _productService.DecrementStockAsync(id, quantity);
            if (!result) return BadRequest("Stock cannot be decremented");
            return Ok("Stock decremented successfully");
        }

        /// <summary>
        /// Adds to the stock of a product by a specified quantity.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <param name="quantity">The quantity to add.</param>
        [HttpPut("add-to-stock/{id}/{quantity}")]
        public async Task<IActionResult> AddToStock(int id, int quantity)
        {
            var result = await _productService.AddToStockAsync(id, quantity);
            if (!result) return BadRequest("Stock cannot be added");
            return Ok("Stock added successfully");
        }
    }
}
