using IMS.Infrastructure.Entity_Configuration;
using IMS.Models.Entity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure
{
    public class IMSDbContext:DbContext
    {
        public IMSDbContext(DbContextOptions<IMSDbContext> Options) : base(Options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           // builder.ApplyConfiguration<StoreInfo> (new StoreInfoConfiguration());
            builder.ApplyConfiguration (new StoreInfoConfiguration());
        }


    }
}
