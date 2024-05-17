using CrudStudentInfo.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudStudentInfo.Services
{
    public class ApplicationDbContext : DbContext
    {
        // Using constructor with parameter type of DbContextOptions & pass it to the base class
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        // Adding property so that we can use this to create a table in our db
        public DbSet<StudentInfo> Students { get; set; }
    }
}
