using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;

namespace Products.Database.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Represents the Products table in the database.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Configures the model and its relationships.
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configures a sequence for generating Product IDs
            modelBuilder.HasSequence<int>("ProductIdSequence")
                .StartsAt(1)
                .IncrementsBy(1)
                .HasMin(1)
                .HasMax(999999);

            // Configures the Product entity to use the sequence for its ID
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductId)
                .HasDefaultValueSql("NEXT VALUE FOR ProductIdSequence")
                .ValueGeneratedOnAdd();
        }
    }
}
