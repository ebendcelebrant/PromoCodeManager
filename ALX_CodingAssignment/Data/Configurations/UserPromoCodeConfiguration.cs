using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ALX_CodingAssignment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALX_CodingAssignment.Data.Configurations
{
    public class UserPromoCodeConfiguration : IEntityTypeConfiguration<UserPromoCode>
    {
        public void Configure(EntityTypeBuilder<UserPromoCode> modelBuilder)
        {

            modelBuilder.HasOne(x => x.ServicePromoCode)
                .WithMany(x => x.UserPromoCodes)
                .HasForeignKey(x => x.ServicePromoCodeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.HasOne(x => x.User)
                .WithMany(x => x.UserPromoCodes)
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Property(x => x.DateAdded)
                .HasColumnType("DATETIME")
                .IsRequired();

            modelBuilder.Property(x => x.LastModifiedDate)
                .HasColumnType("DATETIME")
                .IsRequired();
        }
    }
}
