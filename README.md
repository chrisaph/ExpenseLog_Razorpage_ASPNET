Entity Framework Code First

Open Package Manager Console and run:

Update-Database

If there are no migrations yet, you may need:

Add-Migration InitialCreate
Update-Database

# ExpenseLog_RazorPage_ASPNET

A simple expense tracking web application built with ASP.NET Core Razor Pages. This project allows users to register an account, securely log in, and manage their personal expenses through a clean and responsive interface.

## Features

- User Registration and Login
- ASP.NET Core Identity Authentication
- Create, Read, Update, and Delete (CRUD) expenses
- Search and filter expenses by category
- User profile and account management
- Personal data download and account deletion
- SQL Server database with Entity Framework Core
- Responsive Bootstrap interface

## Technologies Used

- ASP.NET Core Razor Pages
- C#
- Entity Framework Core
- ASP.NET Core Identity
- SQL Server
- Bootstrap

## Project Structure

- Razor Pages
- Models
- Data
- Services
- Entity Framework Migrations

## Screenshots

### Login
![Login](screenshots/login.png)

### Register
![Register](screenshots/register.png)

### Expense Ledger
![Ledger](screenshots/ledger.png)

### Account Management
![Account](screenshots/account.png)

## Getting Started

1. Clone the repository.
2. Open the solution in Visual Studio 2022.
3. Configure the SQL Server connection string in `appsettings.json`.
4. Apply the Entity Framework Core migrations.
5. Run the application.

## License

MIT License
