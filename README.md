```Products/
'''│-- Products (Main API Project)
'''│   │-- Controllers/
'''│   │   │-- ProductsController.cs
'''│   │-- Middleware/
'''│   │   │-- ExceptionHandlingMiddleware.cs
'''│   │-- Program.cs
'''│   │-- appsettings.json
'''│
'''│-- Products.Application (Business Logic Layer)
'''│   │-- Interfaces/
'''│   │   │-- ICacheService.cs
'''│   │   │-- IProductRepository.cs
'''│   │   │-- IProductService.cs
'''│   │-- Services/
'''│   │   │-- ProductService.cs
'''│   │-- Utilities/
'''│   │   │-- MappingProfile.cs
'''│
'''│-- Products.Infrastructure (Data Access Layer)
'''│   │-- Repositories/
'''│   │-- Services/
'''│
'''│-- Products.Domain (Core Entities and Models)
'''│   │-- Entities/
'''│   │-- DTOs/
'''│
'''│-- Products.Database (Persistence Layer)
'''│   │-- DataContext/
'''│   │   │-- AppDbContext.cs
'''│   │   │-- AppDbContextFactory.cs
'''│   │-- Migrations/
'''│   │   │-- 20250207042145_InitialProductMigration.cs
'''│   │   │-- 2025020705236_UpdateColumnName.cs
'''│
'''│-- Products.Test (Unit and Integration Tests)

**1. Products (API Layer)**
Purpose: Acts as the entry point for handling HTTP requests and routing them to the application layer.

Controllers: Manages incoming requests, maps them to appropriate service calls, and returns API responses.
ProductsController.cs: Handles product-related operations such as fetching, creating, updating, and deleting products.
Middleware: Provides centralized exception handling for API requests.
ExceptionHandlingMiddleware.cs: Captures and formats error responses in case of unhandled exceptions.
Program.cs: Configures the web application, dependency injection, middleware, and other startup configurations.
appsettings.json: Stores environment-specific configurations such as database connection strings, API settings, and logging options.

**2. Products.Application (Business Logic Layer)**
Purpose: Encapsulates core application logic and ensures the orchestration of various services.

Interfaces: Defines contracts for services and repositories, enabling loose coupling.
ICacheService.cs: Defines methods for caching operations.
IProductRepository.cs: Specifies operations for accessing product data.
IProductService.cs: Declares product-related business logic.
Services: Implements business logic by interacting with repositories and external services.
ProductService.cs: Provides methods for product-related operations (fetching, CRUD).
Utilities: Contains common utility classes for application operations.
MappingProfile.cs: Uses AutoMapper for object-to-object mapping between DTOs and entities.

**3. Products.Infrastructure (Data Access and External Services Layer)**
Purpose: Handles interactions with data sources and other infrastructure components.

Repositories: Implements interfaces for database operations.
Services: Implements infrastructure-specific logic, such as caching.

**4. Products.Domain (Core Domain Layer)**
Purpose: Defines core business entities and rules.

Entities: Plain Old CLR Objects (POCOs) representing the database models and core business entities.
DTOs: Data Transfer Objects used for communication between application layers.

**5. Products.Database (Persistence Layer)**
Purpose: Manages database interactions and schema migrations.

DataContext: Provides the DbContext for database operations.
AppDbContext.cs: Defines DbSets and database configurations for entities.
AppDbContextFactory.cs: Helps create DbContext instances, potentially for testing or migrations.
Migrations: Contains EF Core migration files for managing database schema.
Example files:
20250207042145_InitialProductMigration.cs: Initial schema setup.
2025020705236_UpdateColumnName.cs: Schema updates.

**6. Products.Test (Testing Layer)**
Purpose: Contains unit tests to ensure the correctness of the system.

**Technical Design Highlights**
_Dependency Injection_: All services and repositories are injected via the Program.cs configuration.
_Separation of Concerns_: The architecture strictly enforces separation between business logic, data access, and presentation.
_AutoMapper Integration_: Simplifies the mapping between entities and DTOs in the application layer.
_Middleware_: Provides centralized exception handling.
_Entity Framework Core_: Used for database interactions and migrations.
