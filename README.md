# 🎓 LearningManagementSystem Web API

This is an ASP.NET Core 8.0 Web API application built using **Clean Architecture** to support online learning through structured courses and lessons.  
The system includes user registration with email confirmation, course progression tracking, and certificate generation upon course completion.  
It's preconfigured with an **In-Memory database** for easy testing but can easily be switched to **SQL Server**.

## 📌 Features

- ✅ **Authentication and Authorization**
  
  Users must **register**, **confirm their email**, and **log in** using **bearer tokens**.  
  Authentication is implemented using **Microsoft Identity** for simplified management of users, roles, and passwords.  
  **JWT bearer tokens** are used to securely authorize access to protected API endpoints.
  
  Email confirmation is handled using **FluentEmail** and **Papercut SMTP**.  
  **Email verification tokens** are valid for **1 hour**. If the email is not confirmed within that time, the token becomes invalid.  
     Expired tokens are **automatically removed once daily**.

- 📚 **Course and Lesson Tracking**  
  - Students can browse available courses using **cursor-based pagination**, and can also fillter to view only the courses they are currently attending.  
  - Progress is tracked as users complete lessons.  
  - Once all lessons in a course are completed, users can call an API endpoint to generate a certificate.  
    > _(In production scenarios, frontend apps typically trigger this endpoint automatically when all lessons are done.)_

- 🧑‍🏫 **Admin Functionality**  
  - Admin users can create new courses.  
  - Admins can add lessons to any course.  
  - Only admins have access to management endpoints.

- 🧑‍💻 **Admin credentials**
  - Email: admin@test.com
  - Password: Pa$$w0rd

- 📄 **Certificate Generation**  
  - A certificate is issued via API call once a user completes all lessons in a course.  
  - Certificate logic is scoped within the course domain layer.

- 🧪 **Postman Collection**  
  - A **Postman collection** is available with all API endpoints for easy testing.

- 🗃️ **In-Memory or SQL Server Database**  
  - The app uses an **In-Memory database** by default for quick setup and testing.  
  - To use **SQL Server**, set `UseInMemoryDatabase` to `false` in `appsettings.Development.json`.

## 🚀 Technologies Used

- ASP.NET Core Web API  
- Entity Framework Core  
- Microsoft SQL Server / In-Memory Database  
- MediatR  
- AutoMapper  
- FluentValidation  
- FluentEmail + Papercut SMTP  
- JWT Bearer Authentication  
- Clean Architecture  

## ⚙️ Getting Started

1. Clone the repository:  
   ```bash
   git clone https://github.com/igao94/LearningManagementSystem
2. Download and run **Papercut SMTP** from [https://github.com/ChangemakerStudios/Papercut-SMTP](https://github.com/ChangemakerStudios/Papercut-SMTP).  
   Be sure to choose the version that matches your operating system to enable and test email functionality locally.
