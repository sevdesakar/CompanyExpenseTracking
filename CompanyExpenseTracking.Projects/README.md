# Company Expense Tracking API

**Company Expense Tracking API** is a RESTful API developed to manage and report company expenses. This API allows users to add, update, delete, and report expenses. It also includes JWT-based authentication and role-based authorization.

---

## **Features**
- **JWT Authentication:**
  - User login and token generation.
  - Authorization for Admin and Personnel roles.
- **CRUD Operations:**
  - CRUD operations for users, expenses, and categories.
- **Validation:**
  - Model-based validation rules using FluentValidation.
- **Reporting:**
  - Company- and personnel-based reports using Dapper.
- **Role-Based Authorization:**
  - Access control for Admin and Personnel roles.
- **Database Seed Data:**
  - Includes Admin and Personnel users, categories, and sample expenses.

---

## **Database Configuration**
Edit the `ConnectionStrings` section in the `appsettings.json` file:
```json
"ConnectionStrings": {
  "MsSqlConnection": "Server=localhost;Database=CompanyExpenseTrackingDB;Trusted_Connection=false;TrustServerCertificate=True;User Id=sa;Password=yourpassword;"
}
```

### **Update the Database**
```bash
dotnet ef database update
```

### **Run the Application**
```bash
dotnet run
```

---

## **API Usage**

### **1. Authentication**
- **POST /api/Auth/Login**
  - Authenticates the user and returns a JWT token.
  - **Request Body:**
    ```json
    {
      "email": "admin@company.com",
      "password": "hashedpassword"
    }
    ```

---

### **2. User Operations**
- **GET /api/User**
  - Lists all users (Admin only).
- **POST /api/User**
  - Creates a new user (Admin only).
  - **Request Body:**
    ```json
    {
      "username": "testuser",
      "email": "testuser@example.com",
      "passwordHash": "password123",
      "role": "Personnel",
      "iban": "TR000000000000000000000000"
    }
    ```

---

### **3. Expense Operations**
- **GET /api/Expense**
  - Lists all expenses or only the current user's expenses.
- **POST /api/Expense**
  - Adds a new expense (Personnel only).
  - **Request Body:**
    ```json
    {
      "description": "Meal expense",
      "amount": 50.75,
      "date": "2025-05-07T12:00:00.000Z",
      "categoryId": 1,
      "status": "Pending"
    }
    ```

---

### **4. Category Operations**
- **GET /api/Category**
  - Lists all categories.
- **POST /api/Category**
  - Adds a new category (Admin only).
  - **Request Body:**
    ```json
    {
      "name": "Food"
    }
    ```

---

### **5. Reporting**
- **GET /api/Reports/PersonalExpenses/{userId}**
  - Lists expenses of a specific user.
- **GET /api/Reports/CompanyExpenses**
  - Daily company-based expense report.
- **GET /api/Reports/PersonnelSpending**
  - Personnel-based spending intensity report.

---

## **Validation Rules**
- **User:**
  - Username cannot be empty and must be a maximum of 50 characters.
  - Email must be in a valid format.
  - Password must be at least 6 characters long.
  - Role must be either "Admin" or "Personnel".
  - IBAN is required for Personnel and must be in a valid format.
- **Expense:**
  - Amount must be greater than zero.
  - Date cannot be in the future.
  - Status must be "Pending", "Approved", or "Rejected".
- **Category:**
  - Category name cannot be empty and must be a maximum of 100 characters.

---

## **Database Seed Data**
- **Users:**
  - Admin:
    ```json
    {
      "email": "admin@company.com",
      "password": "hashedpassword",
      "role": "Admin"
    }
    ```

---

## **Development Notes**
- **Dependency Injection:** `ApplicationDbContext`, FluentValidation, and JWT configurations are managed via DI.
- **Dapper Usage:** Dapper is used for reporting operations.
- **Swagger:** Swagger UI is available for testing API endpoints.

---