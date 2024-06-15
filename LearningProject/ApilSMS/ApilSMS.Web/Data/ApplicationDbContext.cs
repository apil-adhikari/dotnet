using ApilSMS.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApilSMS.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().Property(e => e.FristName).HasMaxLength(100).IsRequired();

            builder.Entity<ApplicationUser>().Property(e => e.MiddleName).HasMaxLength(100);

            builder.Entity<ApplicationUser>().Property(e => e.LastName).HasMaxLength(100).IsRequired();

            builder.Entity<ApplicationUser>().Property(e => e.IsActive).HasDefaultValue(true);

            builder.Entity<ApplicationUser>().Property(e => e.CreatedDate).IsRequired().HasDefaultValueSql("GETDATE()");

            builder.Entity<IdentityRole>().ToTable("Roles").Property(r => r.Id).HasColumnName("RoleId");
        }
    }
}
