# E-Commerce API (.NET 9 + SQL Server)

This is a simple E-Commerce REST API built with **ASP.NET Core 9**, **Entity Framework Core**, and **SQL Server Management Studio (SSMS)**. It provides a foundational backend for an e-commerce platform, handling key business logic and data management.

---

## 🚀 Features

* **User Management:** Complete CRUD operations for users, including registration and login.
* **Category Management:** Full CRUD operations for product categories, with a unique name constraint to prevent duplicates.
* **Product Management:** Full CRUD operations for products, with the ability to search by name or description. Products are linked to a category.
* **Authentication & Authorization:** Secure the API using **JWT-based authentication**, with role-based access control (**Admin / User**) to protect certain endpoints.
* **EF Core Migrations:** Manage database schema changes with Entity Framework Core migrations, making it easy to evolve the database structure over time.

---

## 📂 Project Structure

The project follows a clean, layered architecture to ensure separation of concerns and maintainability.
ECommerce/
├── ECommerce.Domain         # Entities & Interfaces (Business Logic)
├── ECommerce.Infrastructure # Data & Repository Layer (Database Access)
├── ECommerce.Application    # Services (Application Logic)
└── ECommerce                # Controllers & Startup (API Endpoints & Configuration)

---

## ⚙️ Setup Instructions

1.  **Clone the Repository**

    ```bash
    git clone [https://github.com/Sikhul007/3S.git](https://github.com/Sikhul007/3S.git)
    cd 3S
    ```

2.  **Configure Database**

    Open the `appsettings.json` file in the `ECommerce` project and update the connection string with your SQL Server details.

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=YourServerName;Database=YourDatabaseName;Trusted_Connection=True;TrustServerCertificate=True;"
    },
    "Jwt": {
      "Key": "THIS_IS_A_SUPER_SECRET_KEY_DONT_CHANGE_IT",
      "Issuer": "ECommerceAPI",
      "Audience": "ECommerceClient"
    }
    ```

---

## 🔑 API Endpoints

### Authentication

* `POST /api/auth/register` – Register a new user
* `POST /api/auth/login` – Authenticate and receive a JWT

### Users

* `GET /api/users` – Get all users (Admin only)
* `GET /api/users/{id}` – Get a user by ID
* `PUT /api/users/{id}` – Update a user
* `DELETE /api/users/{id}` – Delete a user (Admin only)

### Categories

* `GET /api/categories` – List all categories
* `POST /api/categories` – Create a new category (Admin only)
* `PUT /api/categories/{id}` – Update a category (Admin only)
* `DELETE /api/categories/{id}` – Delete a category (Admin only)

### Products

* `GET /api/products` – List all products
* `GET /api/products/{id}` – Get a product by ID
* `POST /api/products` – Create a new product (Admin only)
* `PUT /api/products/{id}` – Update a product (Admin only)
* `DELETE /api/products/{id}` – Delete a product (Admin only)
* `GET /api/products/search?q=keyword` – Search products by name or description

---

## 📝 API Documentation & Testing (Swagger)

You can easily test all API endpoints using either Swagger UI or Postman.

1.  **Using Swagger UI**
    * Run the application from your terminal: `dotnet run`
    * Open your browser and navigate to: `https://localhost:7077/swagger/index.html`

2.  **Using Postman**
    * Run the application locally.
    * Copy the desired endpoint URL from Swagger.
    * Set the correct HTTP method (GET, POST, PUT, DELETE).
    * For protected endpoints, go to the **Authorization** tab, select **Bearer Token**, and paste the JWT you received from the `/api/auth/login` endpoint.
    * Add the request body (for POST/PUT requests) and send the request.
    * Inspect the response in Postman.

---

**Thank you.**

*Built by Shihab*
