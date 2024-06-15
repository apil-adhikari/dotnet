# Studenet Management System

### 1) Installed Packages
**Microsoft.AspNetCore.Identity.EntityFrameworkCore:** This package is used to integrate ASP.NET Core Identity with Entity Framework Core. It provides functionality for user and role management, such as creating users, managing passwords, and assigning roles, all within an EF Core data model.

    dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore

**Microsoft.EntityFrameworkCore.SqlServer:** This package is the Entity Framework Core provider for Microsoft SQL Server. It enables the use of EF Core to work with a SQL Server database using .NET objects and Language Integrated Query (LINQ).

    dotnet add package Microsoft.EntityFrameworkCore.SqlServer


**Microsoft.EntityFrameworkCore.Design:** This package is required for design-time activities when using EF Core. It provides features like migrations, which help in updating the database schema without losing data.

    dotnet add package Microsoft.EntityFrameworkCore.Design

**Microsoft.EntityFrameworkCore.Tools:** This package is used for EF Core-related command-line tools within the .NET Core CLI. It provides commands for tasks such as creating migrations, updating the database, and generating code for a DbContext and entity types from an existing database (a process known as reverse engineering).

    dotnet add package Microsoft.EntityFrameworkCore.Tools
---
### Application User 
`ApplicationUser.cs` is the model to represent the user who register and login into the application. They are custom model class that are inherited from `IdentityUser` because when using *ASP.NET Core Identity*, it allows us to extend and customize the defualt user model provided by Identity.

### 