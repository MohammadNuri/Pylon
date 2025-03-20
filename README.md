# Pylon App (ASP.NET Core & .NET Aspire)

Pylon Backend Infrastructure is a modular and extensible backend solution built with ASP.NET Core and .NET Aspire. It is designed to provide a robust foundation for building scalable and maintainable applications while following Clean Architecture principles.

## 📂 Project Structure

```
📦 Pylon Solution
├── 📁 Pylon.ApiService            # API Gateway (Controllers, Middlewares)
│    ├── Controllers              # API endpoints
│    └── Middlewares              # Custom middleware for error handling and logging
│
├── 📁 Pylon.AppHost              # Application Orchestration (Aspire-based)
│
├── 📁 Pylon.Application          # Application Layer
│    ├── CustomAttributes         # Custom attributes for validations and annotations
│    ├── DTOs                     # Data Transfer Objects
│    ├── Exceptions               # Custom exception handling
│    ├── Interfaces               # Abstractions and contracts
│    └── Services                 # Business logic services
│
├── 📁 Pylon.Domain               # Domain Layer
│    ├── Entities                 # Core entities (with BaseEntity)
│    ├── Enums                    # Enumeration definitions
│    └── Repositories             # Generic repository interface
│
├── 📁 Pylon.Infrastructure       # Infrastructure Layer
│    ├── Configurations           # Entity Framework configurations
│    ├── Migrations               # Database migrations (EF Core)
│    ├── Persistence              # AppDbContext (Database context)
│    ├── Repositories             # Generic repository implementations
│    └── Security                 # Authentication and security logic
│
├── 📁 Pylon.ServiceDefaults      # Shared service configurations (for Aspire orchestration)
│
├── 📁 Pylon.Shared               # Shared Utilities
│    ├── Constants                # Global constants
│    ├── Enums                    # Shared enumerations
│    ├── Helpers                  # Utility classes and methods
│    └── Logging                  # Logging configuration
│
├── 📁 Pylon.Tests                # Unit and Integration Tests
│    └── Tests                    # Test cases (starting with Repository layer)
│
└── 📁 Pylon.Web                  # Custom Web Design (PWA Frontend integration)
```

## 🧱 Architecture Overview

Pylon follows the Clean Architecture pattern, separating core business logic from external concerns for better maintainability and scalability.

```
Pylon/
├── API/              # ASP.NET Core Web API (Controllers & Middleware)
├── Application/      # Business Logic & Service Layer
├── Domain/           # Entities, Interfaces, and Core Models
├── Infrastructure/   # EF Core, Repository Implementations
├── Shared/           # Common Utilities & Helper Classes
└── Tests/            # Unit Tests (In Progress)
```

- **Pylon.ApiService**: Entry point for external requests via RESTful APIs, responsible for handling HTTP requests, responses, and middleware.
- **Pylon.Application**: Contains business logic, application services, and DTOs. This layer is decoupled from external frameworks and focuses on core functionalities.
- **Pylon.Domain**: Represents core business entities, enums, and repository abstractions. This layer is independent of any external technology.
- **Pylon.Infrastructure**: Handles database interactions (via EF Core), security, and external dependencies. Implements repository interfaces defined in the Domain layer.
- **Pylon.Shared**: Contains shared utilities, constants, and helper methods that can be used across multiple layers.
- **Pylon.ServiceDefaults**: Provides shared service configurations for .NET Aspire-based orchestration.
- **Pylon.AppHost**: The orchestration layer responsible for managing service lifecycles and coordination using .NET Aspire.
- **Pylon.Web**: Custom frontend for the application, designed as a Progressive Web App (PWA).

## 🚀 Features
- **.NET Aspire Integration**: Modern orchestration for distributed and modular applications.
- **Clean Architecture**: Clear separation of concerns between API, Application, Domain, and Infrastructure layers.
- **Generic Repository Pattern**: Simplifies database access across the application.
- **Error Handling Middleware**: Centralized exception management with intelligent error responses.
- **Docker Support**: Built-in Docker configuration for Redis and API services.
- **Logging System**: Centralized logging for better traceability and monitoring.

## 🛠️ Prerequisites
- .NET 8.0 or higher
- Docker (for Redis and service containers)

## 📦 Getting Started

1. **Clone the Repository:**
```bash
git clone https://github.com/yourusername/Pylon.git
cd Pylon
```

2. **Run Docker Containers:**
Ensure Docker is installed and running on your machine.

```bash
docker-compose up -d
```

3. **Apply Migrations:**
```bash
dotnet ef database update --project Pylon.Infrastructure
```

4. **Run the Solution:**
```bash
dotnet run --project Pylon.AppHost
```

## 🧪 Running Tests
```bash
dotnet test Pylon.Tests
```

## 📊 Environment Variables
| Key              | Description                  |
|------------------|------------------------------|
| `ConnectionStrings:DefaultConnection` | Database connection string |
| `Redis:Host`     | Redis server host address    |

## 📖 Future Enhancements
- Implementing Unit of Work (UoW) pattern
- Advanced logging and tracing
- Enhanced security and authentication mechanisms

## 🤝 Contributing
Contributions are welcome! Feel free to fork the repository and submit a pull request.

## 📄 License
MIT License - See [LICENSE](LICENSE) for details.

