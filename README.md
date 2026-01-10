# ExpensesTracker API

**Personal Finance & Receipt Tracker API**  

An ASP.NET Core 9 Web API for tracking income, expenses, budgets, and receipt images.  
Designed with **SOLID principles**, **DTOs**, **AutoMapper**, **EF Core**, and **Serilog logging** for a realistic, maintainable backend.

---

## **Features**

- **User Management**: registration, roles, authentication  
- **Transactions**: CRUD operations for income and expenses  
- **Categories**: categorize transactions for better reporting  
- **Budgets**: create, track, and validate monthly/annual budgets  
- **Receipts**: upload, store, and link receipt images to transactions  
- **Filtering & Sorting**: query transactions with multiple parameters  
- **Pagination**: handle large datasets efficiently  
- **API Versioning**: supports v1 and v2 endpoints  
- **Logging**: structured logging with Serilog  
- **Unit Testing Ready**: prepared for testing with xUnit  

---

## **Tech Stack**

- ASP.NET Core 9 Web API  
- Entity Framework Core 8/9  
- AutoMapper  
- Serilog  
- SQL Server / SQLite (configurable)  
- Git & GitHub for version control  

---

## **Setup Instructions**

### 1. Clone the repository

```bash
git clone https://github.com/AramAlTaki/expenses-tracker-api.git
cd expenses-tracker-api
```

### 2. Open the solution

Open `ExpensesTracker.sln` in **Visual Studio 2022/2023** or **VS Code** with the C# extension.

### 3. Configure your local database

add `appsettings.Development.json` as an app settings file and set your connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=ExpensesTrackerDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```
### 4. Restore NuGet packages

Run the following command in the solution root to restore all required packages:

```bash
dotnet restore
```

### 5. Apply EF Core migrations

Run the following command to create or update the database schema based on your EF Core migrations:

```bash
dotnet ef database update --project src/ExpensesTracker.API/ExpensesTracker.API.csproj
```

### 6. Run the API

Run the API using the following command:

```bash
dotnet run --project src/ExpensesTracker.API/ExpensesTracker.API.csproj
```

### 7. Test the endpoints

- Open **Swagger UI** at `https://localhost:7007/swagger/index.html`  
You can also use **Postman** or any other API client to test the endpoints.
