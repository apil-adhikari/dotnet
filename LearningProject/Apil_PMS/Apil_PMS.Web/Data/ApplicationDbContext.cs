using Apil_PMS.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Apil_PMS.Web.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opitons) : base(opitons)
        {
            
        }

        // Table
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        // ApplicationUser Model Configuration : This is essential for definint how our application's entity types are mapped to the underlying database.(ie. Entity type to db mappping)
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .Property(au => au.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<ApplicationUser>()
                .Property(au => au.MiddleName)
                .HasMaxLength(100);

            builder.Entity<ApplicationUser>()
                .Property(au => au.Address)
                .HasMaxLength(255);

            builder.Entity<ApplicationUser>()
                .Property(au => au.ProfileUrl)
                .HasMaxLength(500);

            builder.Entity<ApplicationUser>()
                .Property(au => au.IsActive)
                .HasDefaultValue(true);

            builder.Entity<ApplicationUser>()
                .Property(au => au.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<IdentityRole>()
                .ToTable("Roles")
                .Property(au => au.Id)
                .HasColumnName("RoleId");
        }
    }


}
