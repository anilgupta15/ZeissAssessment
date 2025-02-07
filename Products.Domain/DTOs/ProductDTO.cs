using FluentValidation;

namespace Products.Domain.DTOs
{
    public class ProductDTO : CreateProductDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductId { get; set; }
    }

    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            // Validation rule for the Name property
            RuleFor(product => product.Name)
                .NotEmpty()
                .WithMessage("Product Name cannot be empty");

            // Validation rule for the Price property
            RuleFor(product => product.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");

            RuleFor(product => product.StockAvailable)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock Available cannot be negative");

            RuleFor(product => product.CreatedAt)
                .NotEmpty()
                .WithMessage("Created Date cannot be empty");

            RuleFor(product => product.Category)
                .NotEmpty()
                .WithMessage("Category cannot be empty");

            RuleFor(product => product.UpdatedAt)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Updated Date cannot be in the future");
        }
    }
}
