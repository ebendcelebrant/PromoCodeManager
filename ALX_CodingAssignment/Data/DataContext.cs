using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ALX_CodingAssignment.Domain.Models;
using ALX_CodingAssignment.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ALX_CodingAssignment.Data
{
    public class DataContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options)
              : base(options)
        {
        }
        public DbSet<ServicePromoCode> ServicePromoCodes { get; set; }
        public DbSet<UserPromoCode> UserPromoCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<User>().Property(p => p.Id).UseIdentityColumn<int>();

            builder.ApplyConfiguration(new ServicePromoCodeConfiguration());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=LocalDatabase.db");
            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            var entities = (from entry in ChangeTracker.Entries()
                            where entry.State == EntityState.Modified || entry.State == EntityState.Added
                            select entry.Entity);

            var validationResults = new List<ValidationResult>();
            foreach (var entity in entities)
            {
                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults, validateAllProperties: true))
                {
                    throw new ValidationException();
                }
            }
            return base.SaveChanges();
        }
    }
}
