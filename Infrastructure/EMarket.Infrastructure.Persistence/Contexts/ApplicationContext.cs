using Microsoft.EntityFrameworkCore;
using EMarket.Core.Domain.Common;
using EMarket.Core.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EMarket.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API

            #region tables

            modelBuilder.Entity<Advertisement>()
                .ToTable("Advertisement");

            modelBuilder.Entity<Category>()
                .ToTable("Categories");

            modelBuilder.Entity<User>()
                .ToTable("Users");

            #endregion

            #region "primary keys"
            modelBuilder.Entity<Advertisement>()
                .HasKey(product => product.Id);

            modelBuilder.Entity<Category>()
                .HasKey(category => category.Id);

            modelBuilder.Entity<User>()
              .HasKey(user => user.Id);
            #endregion

            #region "Relationships"

            modelBuilder.Entity<Category>()
                .HasMany(category => category.Advertisements)
                .WithOne(product => product.Category)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
             .HasMany(user => user.Advertisements)
             .WithOne(product => product.User)
             .HasForeignKey(product => product.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region "Property configurations"

            #region Advertisements

            modelBuilder.Entity<Advertisement>().
                Property(product => product.Name)
                .IsRequired();

            modelBuilder.Entity<Advertisement>().
               Property(product => product.Price)
               .IsRequired();

            modelBuilder.Entity<Advertisement>().
               Property(product => product.Description)
               .IsRequired();

            modelBuilder.Entity<Advertisement>().
               Property(product => product.CategoryId)
               .IsRequired();

            #endregion

            #region Categories
            modelBuilder.Entity<Category>().
              Property(category => category.Name)
              .IsRequired()
              .HasMaxLength(100);

            modelBuilder.Entity<Category>().
              Property(category => category.Description)
              .IsRequired();
            #endregion

            #region Users

            modelBuilder.Entity<User>().
                Property(user => user.Name)
                .IsRequired();

            modelBuilder.Entity<User>().
               Property(user => user.Username)
               .IsRequired();

            modelBuilder.Entity<User>().
              Property(user => user.Password)
              .IsRequired();

            modelBuilder.Entity<User>().
              Property(user => user.Email)
              .IsRequired();

            modelBuilder.Entity<User>().
               Property(user => user.Phone)
               .IsRequired();

            #endregion

            #endregion
        }

    }
}
