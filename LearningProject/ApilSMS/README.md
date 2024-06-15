# Studenet Management System

## Installed Packages
**Microsoft.AspNetCore.Identity.EntityFrameworkCore:** This package is used to integrate ASP.NET Core Identity with Entity Framework Core. It provides functionality for user and role management, such as creating users, managing passwords, and assigning roles, all within an EF Core data model.

    dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore

**Microsoft.EntityFrameworkCore.SqlServer:** This package is the Entity Framework Core provider for Microsoft SQL Server. It enables the use of EF Core to work with a SQL Server database using .NET objects and Language Integrated Query (LINQ).

    dotnet add package Microsoft.EntityFrameworkCore.SqlServer


**Microsoft.EntityFrameworkCore.Design:** This package is required for design-time activities when using EF Core. It provides features like migrations, which help in updating the database schema without losing data.

    dotnet add package Microsoft.EntityFrameworkCore.Design

**Microsoft.EntityFrameworkCore.Tools:** This package is used for EF Core-related command-line tools within the .NET Core CLI. It provides commands for tasks such as creating migrations, updating the database, and generating code for a DbContext and entity types from an existing database (a process known as reverse engineering).

    dotnet add package Microsoft.EntityFrameworkCore.Tools
---
## Application User 
`ApplicationUser.cs` is the model to represent the user who register and login into the application. They are custom model class that are inherited from `IdentityUser` because when using *ASP.NET Core Identity*, it allows us to extend and customize the defualt user model provided by Identity.

## ApplicationDbContext
`ApplicationDbContext` serves as the bridge between the **application** and the **database**, providing methods to query and save data. It manages the database connections and entity configurations.

### ASP.NET Core Identity Integration: 
By inheriting from `IdentityDbContext<ApplicationUser>`,it extends the Entity Framework Core `DbContext` to include **Identity-specific tables** and **functionality**. Our `ApplicationDbContext` integrates ASP.NET Core Identity functionality and gains all the functionalities required to manage **Identity data** such as **users, roles, claims, logins, and tokens** and also enabling **user authentication and authorization**.

### Constructor:
Constructor initializes the `ApplicationDbContext` with configuration options.

```C#
public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
{
}
```
`DbContextOptions<ApplicationDbContext>`: It is the configuration options for the context, such as the database provider (SQL Server, SQLite, etc.) and connection string.

`base(options)`: It passes the options to the base class (`IdentityDbContext`), which uses these options to configure the database context.

### DbSet
```c#
public DbSet<ApplicationUser> ApplicationUsers { get; set; }
```
`DbSet<ApplicationUser>`: It represents a table of `ApplicationUser` entities in the database. It allows us to *query* and *save* instances of ApplicationUser.

### OnModelCreating
The `OnModelCreating` method is part of the `DbContext` class in **Entity Framework Core**. When we *override* this method, we can configure the EF Core model by using the `ModelBuilder` parameter provided to the method. This is where we define configurations for our entities such as setting up primary keys, relationships, indexes, table mappings, and more.

### base.OnModelCreating(builder):
 This calls the `base` class implementation to ensure the default configurations for Identity entities are applied.

 ### Entity Configuration:
  It uses the `ModelBuilder` to configure the schema for the `ApplicationUser` and `IdentityRole` entities.
#### IdentityRole Entity Configuration
The table name in the database is explicitly set to "Roles" instead of the default "IdentityRole". The `Id` property of the role entity is being mapped to a column named `RoleId` in the "Roles" table.
