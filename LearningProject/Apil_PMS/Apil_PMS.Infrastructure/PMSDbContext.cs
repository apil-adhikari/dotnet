using Apil_PMS.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apil_PMS.Infrastructure
{
    public class PMSDbContext: DbContext
    {
        public PMSDbContext(DbContextOptions<PMSDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());           
        }
    }
}
