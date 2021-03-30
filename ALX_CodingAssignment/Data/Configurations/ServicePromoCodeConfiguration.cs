using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ALX_CodingAssignment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALX_CodingAssignment.Data.Configurations
{
    public class ServicePromoCodeConfiguration : IEntityTypeConfiguration<ServicePromoCode>
    {
        public void Configure(EntityTypeBuilder<ServicePromoCode> modelBuilder)
        {
            modelBuilder.Property(x => x.PromoCode)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Property(x => x.ServiceName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Property(x => x.Description)
                .IsUnicode(false)
                .IsRequired(false);

            modelBuilder.Property(x => x.DateAdded)
                .HasColumnType("DATETIME")
                .IsRequired();

            modelBuilder.Property(x => x.LastModifiedDate)
                .HasColumnType("DATETIME")
                .IsRequired();
        }
    }
}
