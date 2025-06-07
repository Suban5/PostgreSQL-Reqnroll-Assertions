# PostgreSQL-Reqnroll-Assertions

This project is a BDD-style test automation framework for PostgreSQL-backed applications. It uses [Reqnroll](https://reqnroll.net/) (a SpecFlow fork), [Dapper](https://github.com/DapperLib/Dapper), and [NUnit](https://nunit.org/) to help developers and QA write clear, maintainable database tests using Gherkin feature files.

## Description

This framework demonstrates how to perform assertions on PostgreSQL database state using Reqnroll DataTable. It shows how to set up, insert, and verify data in database tables directly from Gherkin scenarios with reusable step definitions. The goal is to make it easy for both developers and QA to validate data-driven workflows in a straightforward, maintainable way.

## Interesting Techniques

- **Centralized Data Filtering:**  
  [`Support/Extensions/DataTableHelper.cs`](./Support/Extensions/DataTableHelper.cs) normalizes test data, converting `"null"` strings to actual `null` values and replacing `~` with spaces.
- **Bulk Insert with Dapper:**  
  Uses Dapper for efficient, parameterized bulk inserts, making test setup fast even with large datasets.
- **Dynamic SQL Generation:**  
  WHERE clauses are built dynamically from table data, supporting flexible positive and negative assertions.
- **Generic Step Definitions:**  
  Steps are written to work with any table and column set, reducing duplication and making the framework easy to extend.

## Notable Libraries and Technologies

- [Reqnroll](https://reqnroll.net/) — BDD for .NET, Gherkin support.
- [Dapper](https://github.com/DapperLib/Dapper) — Lightweight ORM for .NET.
- [NUnit](https://nunit.org/) — Unit testing framework.
- [Npgsql](https://www.npgsql.org/) — PostgreSQL driver for .NET.

## Project Structure
 ├── Features/ 
 ├── Scripts/ 
 ├── StepDefinitions/ 
 ├── Support/ 
    └── Configuration/ 
    └── Database/ 
    └── Extensions/
    └── Models/ 
 
- **Features/**: Gherkin `.feature` files describing test scenarios in a human-readable format.
- **Scripts/**: SQL scripts, such as [`Scripts/createTable.txt`](./Scripts/createTable.txt), used for database schema setup or teardown.
- **StepDefinitions/**: C# classes with methods that implement the Gherkin steps defined in feature files.
- **Support/**: Utilities and helpers.
    - **Support/Configuration/**: Configuration-related helpers or files.
    - **Support/Database/**: Database helpers for executing queries and bulk operations, e.g., [`Support/Database/PostgreSqlDataHelper.cs`](./Support/Database/PostgreSqlDataHelper.cs).
    - **Support/Extensions/**: Extension methods and helpers, e.g., [`Support/Extensions/DataTableHelper.cs`](./Support/Extensions/DataTableHelper.cs).
    - **Support/Models/**: C# classes representing database entities.

## How to Use

1. **Set up the database:**
   - Run the SQL in [`Scripts/createTable.txt`](./Scripts/createTable.txt) to create the required tables in your PostgreSQL instance.
   - Run the SQL in [`Scripts/insertData.txt`](./Scripts/insertData.txt) to insert any initial or sample data needed for your tests.

2. **Configure the connection:**
   - Open your `appsettings.json` file.
   - Update the database name and PostgreSQL password in the connection string to match your environment.

3. **Build and run the tests:**
   - The project already includes Gherkin scenarios in the [`Features/`](./Features/) directory.
   - Use your preferred test runner (e.g., `dotnet test` or the test explorer in your IDE) to build and execute the tests directly.

4. **Extend as needed:**
   - Add new feature files in [`Features/`](./Features/), step definitions in [`StepDefinitions/`](./StepDefinitions/), or models in [`Support/Models/`](./Support/Models/) as your schema or scenarios grow.

## External Resources

- [Reqnroll Documentation](https://reqnroll.net/docs/)
- [Dapper Documentation](https://github.com/DapperLib/Dapper)
- [NUnit Documentation](https://docs.nunit.org/)
- [Npgsql Documentation](https://www.npgsql.org/doc/index.html)

---

For questions or contributions, please open an issue or pull request.
