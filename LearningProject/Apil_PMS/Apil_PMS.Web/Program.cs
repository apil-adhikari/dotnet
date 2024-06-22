using Apil_PMS.Infrastructure;
using Apil_PMS.Infrastructure.IRepository.ICrudService;
using Apil_PMS.Infrastructure.Repository.CrudService;
using Apil_PMS.Infrastructure.Services;
using Apil_PMS.Web.Data;
using Apil_PMS.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages();

//// Adding ApplicationDbContext
//var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddDbContext<PMSDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
//e => e.MigrationsAssembly("Apil_PMS.Web")));

////builder.Services.AddDbContext<PMSDbContext>(options =>options.UseSqlServer(ConnectionString));

//// Adding Service type for RoleManager
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddTransient(typeof(ICrudService<>), typeof(CrudService<>));
////builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
////.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

//// Adding EmailSender in service
//builder.Services.AddSingleton<IEmailSender, EmailSender>();

//var app = builder.Build();

//using (var Scope = app.Services.CreateScope())
//{
//    var Services = Scope.ServiceProvider;
//    await SeedingData.CreateRoles(Services);
//}

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();
//app.MapRazorPages();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

//Adding ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adding PMSDbContext
builder.Services.AddDbContext<PMSDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
e => e.MigrationsAssembly("Apil_PMS.web")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<ApplicationDbContext>()
       .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddTransient(typeof(ICrudService<>), typeof(CrudService<>));
//builder.Services.AddTransient<IRawSqlRepository, RawSqlRepository>();

builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = $"/Identity/Account/Login";
    option.LogoutPath = $"/Identity/Account/Logout";
    option.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

//builder.Services.AddDefaultIdentity<>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddIdentity<, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedingData.CreateRoles(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication(); // Ensure this is added
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");
app.Run();