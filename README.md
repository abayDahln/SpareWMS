# SpareWMS (Spare Part Warehouse Management System)

SpareWMS is a desktop-based application designed to manage spare part inventory and warehouse organization. It helps in tracking spare parts across different racks and bins, managing transactions (IN/OUT), and maintaining user roles for secure access.

## Tech Stack

- **Framework:** .NET Framework 4.8
- **Language:** C#
- **UI:** Windows Forms (WinForms)
- **Database:** Microsoft SQL Server
- **ORM:** LINQ to SQL

## Features

- **Inventory Tracking:** Real-time tracking of spare parts in specific warehouse locations (Racks and Bins).
- **Transaction Logs:** Records all incoming (IN) and outgoing (OUT) transactions.
- **Warehouse Management:** Configuration of Racks, Rows, and Bins based on categories (e.g., Finished Goods, Raw Materials).
- **User Management:** Role-based access control for Supervisors and Operators.
- **Reporting:** Visual data representation (based on project references).

## Database Setup

1. Open **SQL Server Management Studio (SSMS)**.
2. Open the `DbSpareWMS.sql` file provided in the root directory.
3. Execute the script to create the `SpareWMS` database, tables, and seed data.
4. Update the connection string in `App.config` if necessary.

## Getting Started

1. Clone the repository.
2. Open `SpareWMS.sln` in **Visual Studio**.
3. Restore any missing NuGet packages (if applicable).
4. Build and run the project.

## Project Structure

- `DbSpareWMS.sql`: Database initialization and seed data script.
- `DataClasses1.dbml`: LINQ to SQL classes mapping.
- `Form1.cs` to `Form6.cs`: Various application screens (Login, Dashboard, Inventory, etc.).
- `Program.cs`: Main entry point.

## License

Internal Project - All rights reserved.
