using FluentValidation;

namespace Products.Domain.DTOs
{
    public class CreateProductDTO
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the available stock of the product.
        /// </summary>
        public int StockAvailable { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the product.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the last updated date of the product.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the product is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        public string Category { get; set; }
    }

    public class CreateProductDTOValidator : AbstractValidator<CreateProductDTO>
    {
        public CreateProductDTOValidator()
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
