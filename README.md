
# 🛍️ E-Commerce API with Angular

A modern and secure backend API for an e-commerce platform, built using **ASP.NET Core 9** and designed to work with an **Angular** frontend. This solution handles everything from authentication to product and order management with performance and scalability in mind.

---

## 🚀 Features Overview

### 🔐 Authentication & Authorization
- JWT & Secure Cookie Authentication
- Email Verification & Password Reset

### 🛒 Product & Cart Management
- Full CRUD for Products & Categories
- Product Images Support
- Advanced Search & Pagination
- Shopping Cart & Redis-based Basket Storage

### 📦 Order Management
- Place Orders
- View Order History
- Order Processing Logic

### 🛡️ Security
- HTTPS Enforcement
- CORS Configuration
- Rate Limiting to Avoid Abuse
- Exception Handling Middleware

---

## 🧰 Tech Stack

- **Backend:** ASP.NET Core 9
- **Database:** SQL Server
- **Cache:** Redis
- **Auth:** JWT + Cookies
- **Email Service:** Gmail SMTP
- **API Docs:** Swagger / OpenAPI

---

## 📁 Project Structure

```
Ecom/
├── Ecom.API/              # API Layer
│   ├── Controllers/
│   ├── Middleware/
│   └── Helper/
├── Ecom.Core/             # Core Business Logic
│   ├── DTOs/
│   ├── Entities/
│   ├── Interfaces/
│   └── Services/
└── Ecom.Infrastructure/   # Data Access & Services
    ├── Data/
    ├── Repositories/
    └── Services/
```

---

## ⚙️ Getting Started

### 📋 Prerequisites
- .NET 9.0 SDK
- SQL Server
- Redis
- SMTP Account (Gmail used)

### 🛠 Configuration

Update `appsettings.json` with your credentials:
```json
{
  "ConnectionStrings": {
    "EcomDB": "your-db-connection",
    "redis": "localhost"
  },
  "EmailSetting": {
    "From": "your@gmail.com",
    "UserName": "your@gmail.com",
    "Password": "your-app-password",
    "Smtp": "smtp.gmail.com"
  },
  "Token": {
    "Secret": "your-secret-key",
    "Issure": "https://localhost:44306/"
  }
}
```

### ▶️ Run the App
```bash
dotnet restore
dotnet build
dotnet run --project Ecom.API
```

---

## 📡 API Endpoints

### 🔐 Account
- `POST /api/Account/Register`
- `POST /api/Account/Login`
- `POST /api/Account/active-account`
- `GET /api/Account/send-email-forget-password`
- `POST /api/Account/reset-password`

### 🛒 Product
- `GET /api/Product/get-all`
- `GET /api/Product/get-by-id/{id}`
- `POST /api/Product/Add-Product`

### 📦 Order
- `POST /api/Order/create-order`

---

## 🔒 Security

### CORS Policy
Allowed origins:
- `http://localhost:4200`
- `http://127.0.0.1:5500`

### Secure Cookie Settings
- `Secure: true`
- `HttpOnly: true`
- `SameSite: Strict`

### Rate Limiting
- 100 requests / 30 seconds / IP

### Exception Middleware
- Handles unhandled errors
- Developer-friendly messages in dev mode
- Sanitized output in production


# Ecommerce-web-API
