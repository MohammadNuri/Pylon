# Pylon Backend Infrastructure

A modular, scalable, and reusable backend infrastructure built with **.NET Aspire** and **Clean Architecture**. This project is designed to speed up backend development by providing a solid, extendable foundation for any web application.

## 📐 Architecture Overview

Pylon follows the **Clean Architecture** pattern, separating core business logic from external concerns for better maintainability and scalability.

```
Pylon.Backend.Infrastructure/
├── API/              # ASP.NET Core Web API (Controllers & Middleware)
├── Application/      # Business Logic & Service Layer
├── Domain/           # Entities, Interfaces, and Core Models
├── Infrastructure/   # EF Core, Repository Implementations
├── Shared/           # Common Utilities & Helper Classes
└── Tests/            # Unit Tests (In Progress)
```

## 🚀 Key Features

- **.NET Aspire** for modern, cloud-native applications
- **Clean Architecture** with clear separation of concerns
- **Generic Repository** and **Service Pattern** for code reusability
- **Custom Middleware** for centralized error handling
- **Docker** support (coming soon)
- **Unit Testing** for repository layer (in progress)

## 🛠️ Getting Started

### Prerequisites

- .NET 8 or higher
- Docker (optional, for containerized deployment)
- Git

### Clone the Repository

```bash
git clone https://github.com/your-username/Pylon.git
cd Pylon
```

### Setup & Run

1. Restore packages:

```bash
dotnet restore
```

2. Apply Migrations (EF Core):

```bash
dotnet ef database update --project src/Infrastructure
```

3. Run the application:

```bash
dotnet run --project src/API
```

The API will be available at: `https://localhost:5001`

## 📚 Project Structure

### API Layer
- Entry point of the application
- Handles HTTP requests via **Controllers**
- Includes custom **Middleware** for error handling

### Application Layer
- Implements business logic
- Contains **Services** which interact with repositories

### Domain Layer
- Defines core entities and business rules
- Includes **BaseEntity** for common properties

### Infrastructure Layer
- Implements **Generic Repository** pattern
- Manages **EF Core** DbContext and database interactions

### Shared Layer
- Contains shared utilities and helper classes

### Tests Layer
- (In Progress) Unit tests for core services and repositories

## 📦 Docker Support (Coming Soon)

A `Dockerfile` will be added to enable easy containerization of the application.

## 📊 Future Improvements

- Implement UnitOfWork pattern (optional based on business needs)
- Complete unit testing for repositories and services
- Dockerize the entire solution

## 🤝 Contributing

Contributions are welcome! Feel free to fork this repository and submit pull requests.

## 📜 License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for more details.

---

Happy coding! 💻
