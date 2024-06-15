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

## appsettings.json
Adding `ConnectionString` to our app settings.
```C#
"ConnectionStrings": {
  "DefaultConnection": "Server:DESKTOP-7OJEPMF\\SQLEXPRESS;Database=ApilSMS;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
}
```

Updating `Program.cs`: Configure the connection string for Entity Framework Core
```C#
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefautlConnection")));
```

## SeedingData
`SeedingData` class is used to create roles for our application. It contains a method `SeedRolesAndAdminAsync` that is responsible for initializing **roles** and an **admin user** in our application’s database. 

### RoleManager and UserManager: 
The method starts by retrieving the `RoleManager` and `UserManager` services from the provided `IServiceProvider`. These services are used to manage roles and users in the Identity system of our application.
```C#
var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
```

### Define Roles:
 An array of role names is defined, which includes “`SUPERADMIN`”, “`ADMIN`”, and “`STUDENT`”. These are the roles that will be seeded into the database.

```C#
string[] roleNames = { "SUPERADMIN", "ADMIN", "STUDENT" };
```

 ### Seed Roles: 
 The method then iterates over the array of role names, checking if each role already exists in the database using `roleManager.RoleExistsAsync`. If a role does not exist, it is created using `roleManager.CreateAsync`.
```C#
foreach (var roleName in roleNames)
{
    var roleExist = await roleManager.RoleExistsAsync(roleName);
    if (!roleExist)
    {
        await roleManager.CreateAsync(new IdentityRole(roleName));
    }
}
```

### Assign Role to User: 
If the creation of the super admin user is successful (`result.Succeeded`), the method assigns the “`SUPERADMIN`” role to this user using `userManager.AddToRoleAsync`.

```C#
if (await userManager.FindByEmailAsync("superadmin@sms.com") == null)
{

    var role = roleManager.FindByNameAsync("SUPERADMIN").Result;
    var superAdminUser = new ApplicationUser
    {
        Email = "superadmin@sms.com",
        UserName = "superadmin@sms.com",
        FirstName = "Super",
        LastName = "Admin",
        UserRoleId = role.Id,
        EmailConfirmed = true,
        IsActive = true,
        CreatedDate = DateTime.Now
    };

    // Creating the super admin with password
    var result = await userManager.CreateAsync(superAdminUser, "SuperAdmin@123");
    await userManager.SetLockoutEnabledAsync(superAdminUser, false);

    // Assign the "SUPERADMIN" role to the super admin user
    if (result.Succeeded)
    {
        await userManager.AddToRoleAsync(superAdminUser, "SUPERADMIN");
    }
}
```

This seeding process ensures that when our application starts, it has at least one super admin user with the highest privileges and predefined roles that can be assigned to other users.

Updating `Program.cs`:
 Adding Identity services and configure the identity options before the build. It registers the Identity services, which include user and role management, token generation, and Entity Framework stores for managing these entities.

```C#
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
```

**SeedingData** creates a service scope to access the `RoleManager` and `UserManager` services and calls the `SeedRolesAndAdminAsync` method to ensure **roles** and the **super admin user** are created.

```C#
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedingData.SeedRolesAndAdminAsync(services);
}
```

Adding `app.UseAuthentication()` below the build. These enable **authentication and authorization middleware** in the application.

```C#
app.UseAuthentication();
app.UseAuthorization();
```

### Manage DataBase Migration
In Entity Framework Core, migrations are used to manage changes to our database schema.
 
**Add-Migration InitialCreate **
This command creates a new migration named “InitialCreate”. Migrations are like version control for our database, allowing us to update the database schema in a controlled way. The “InitialCreate” migration will contain the necessary code to create the database schema based on your current model definitions.

    Add-Migration InitialCreate
    
**Update-Database:** This command applies the pending migrations to the database. In this case, it will apply the “InitialCreate” migration our just created, which will create the database tables and relationships as defined in our models and DbContext.

    Update-Database

***For multiple DbContexts:***

Adding Migration:

    Add-Migration -Context DbContextName MigrationName

Updating Database:

    Update-Database -Context DbContextName