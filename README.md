🚀 EmployeeManager API - .NET 10
A robust and scalable Employee Management Backend built with .NET 10. 
This is my debut project in backend development, where I’ve translated my experience in Game Development (Unity/C#) into building structured and scalable server-side systems.

🛠️ Tech Stack
Framework: .NET 10 (Web API)

Language: C# 14

Database: SQL Server

ORM: Entity Framework Core

Documentation: Swagger / OpenAPI

🏗️ Architecture & Patterns
I implemented an N-Layer Architecture to ensure separation of concerns:

Controllers: Handling HTTP requests and responses.

Services: Core business logic and data processing.

DTOs (Data Transfer Objects): Ensuring secure and efficient data exchange.

Repository Pattern (via EF Core): For streamlined database interactions.

🌟 Key Features
Full CRUD: Seamless Create, Read, Update, and Delete operations for employees.

Relational Database: Optimized 1:N relationship management between Profiles and Employees.

Asynchronous Programming: Fully async/await driven to maximize server throughput.

Clean Code: Adhering to SOLID principles for better scalability.

🔐 Security & Identity
- **JWT Authentication:** Secure access using JSON Web Tokens with `HS256` signature.
- **Role-Based Authorization (RBAC):** Distinct access levels for `Admin` and `Employee`.
- **Password Hashing:** Industry-standard protection using `BCrypt` to ensure user data safety.
- **Identity Context:** Integrated `GetMe` endpoint to retrieve authenticated user sessions from token claims.

🚦 API Endpoints (Quick Reference)

#### 🔑 Authentication
- `POST /api/auth/register` - Create a new user & employee profile.
- `POST /api/auth/login` - Authenticate and receive a JWT Bearer Token.
- `GET /api/auth/me` - Retrieve current user session info (Requires Token).

#### 👥 Employees
- `GET /api/empleado` - List all employees.
- `POST /api/empleado` - Create new entry (Admin only).
- `GET /api/empleado/{id}` - Get specific details.

### 🚀 How to Run
1. **Clone the repository:** `git clone https://github.com/tu-usuario/EmployeeManager-API-NET10.git`
2. **Database Setup:** Update the connection string in `appsettings.json` with your local SQL Server instance.
3. **Apply Migrations:** Run `Update-Database` in the Package Manager Console.
4. **Run the Project:** Press `F5` in Visual Studio. Swagger will open automatically at `/swagger`.
