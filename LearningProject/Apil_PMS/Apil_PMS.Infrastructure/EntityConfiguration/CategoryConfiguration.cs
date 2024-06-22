using Apil_PMS.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apil_PMS.Infrastructure.EntityConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode();

            builder.Property(e => e.CategoryDescription)
                .HasMaxLength(500)
                .IsRequired()
                .IsUnicode();

            builder.HasMany(e => e.Products)
                 .WithOne(pt => pt.Category)
                 .HasForeignKey(e => e.CategoryId);
                


        }
    }
}
