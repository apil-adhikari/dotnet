using IMS.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure.Entity_Configuration
{
    public class StoreInfoConfiguration : IEntityTypeConfiguration<StoreInfo>
    {
        public void Configure(EntityTypeBuilder<StoreInfo> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.StoreName)
                .HasMaxLength(200)
                .IsUnicode(true);

            builder.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(true);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(true);

            builder.Property(e => e.RegistrationNumber)
                .HasMaxLength(30)
                .IsUnicode(true);

            builder.Property(e => e.PanNumber)
                .HasMaxLength(30)
                .IsUnicode(true);

            builder.Property(e => e.IsActive);
                //.HasDefaultValue(true);

            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .IsUnicode(true);

            builder.Property(e => e.CreatedDate)
                .IsRequired()
             .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.ModifiedBy)
                //.IsRequired()
                .IsUnicode(true);

            builder.Property(e => e.ModifiedDate)
               // .IsRequired();
                .HasColumnName("DATETIME");
        }

        
    }
}
