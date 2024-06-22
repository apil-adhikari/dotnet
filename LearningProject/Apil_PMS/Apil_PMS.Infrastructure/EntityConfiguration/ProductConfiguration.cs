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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);
            builder.Property(e => e.ProductDescription)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode(true);

            builder.Property(e => e.Rate)
                .IsRequired();

            builder.Property(e => e.Quantity)
                .IsRequired();
            builder.Property(e => e.BatchNo)
                .IsRequired()
                .IsUnicode(true);

            builder.Property(e => e.ProductionDate)
              .IsRequired();
            //.HasColumnType("datetime");

            builder.Property(e => e.ExpiryDate);
                 //.HasColumnType("datetime");

            builder.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .IsUnicode(true);

            builder.Property(e => e.IsAvaliable);

            //relationship
            builder.HasOne(e => e.Category)
                .WithMany(pt => pt.Products)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
