using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Uroboro.Common.Models;
using Uroboro.DAL.Extensions;

namespace Uroboro.DAL.Contexts
{
    public abstract class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .AddQueryFilterToAllEntitiesAssignableFrom<IBaseItem>(qf => !qf.IsDeleted);

            var allEntities = modelBuilder.Model.GetEntityTypes();

            foreach (var entity in allEntities)
            {
                entity
                    .AddProperty("CreatedAt", typeof(DateTime?));
                entity
                    .AddProperty("CompletedAt", typeof(DateTime?));
                entity
                    .AddProperty("ModifiedAt", typeof(DateTime?));
                entity
                    .AddProperty("DeletedAt", typeof(DateTime?));
                entity
                    .AddProperty("CreatedBy", typeof(string));
                entity
                    .AddProperty("CompletedBy", typeof(string));
                entity
                    .AddProperty("ModifiedBy", typeof(string));
                entity
                    .AddProperty("DeletedBy", typeof(string));
            }

            modelBuilder
                .Ignore<BaseItem>();

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SeedEntries();
            return base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            SeedEntries();
            return base.SaveChanges();
        }

        private IEnumerable<EntityEntry> SeedEntries()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            bool prevValue, currValue;

            foreach (var entityEntry in entries)
            {
                entityEntry
                    .Property("ModifiedAt")
                    .CurrentValue = DateTime.UtcNow;
                entityEntry
                    .Property("ModifiedBy")
                    .CurrentValue = "system";

                prevValue = (bool)entityEntry.Property("IsCompleted").OriginalValue;
                currValue = (bool)entityEntry.Property("IsCompleted").CurrentValue;

                if (prevValue != currValue)
                {
                    if (currValue)
                    {
                        entityEntry
                            .Property("CompletedAt")
                            .CurrentValue = DateTime.UtcNow;
                        entityEntry
                            .Property("CompletedBy")
                            .CurrentValue = "system";
                    }
                    else
                    {
                        entityEntry
                            .Property("CompletedAt")
                            .CurrentValue = null;
                        entityEntry
                            .Property("CompletedBy")
                            .CurrentValue = null;
                    }
                }

                prevValue = (bool)entityEntry.Property("IsDeleted").OriginalValue;
                currValue = (bool)entityEntry.Property("IsDeleted").CurrentValue;

                if (prevValue != currValue)
                {
                    if (currValue)
                    {
                        entityEntry
                            .Property("DeletedAt")
                            .CurrentValue = DateTime.UtcNow;
                        entityEntry
                            .Property("DeletedBy")
                            .CurrentValue = "system";
                    }
                    else
                    {
                        entityEntry
                           .Property("DeletedAt")
                           .CurrentValue = null;
                        entityEntry
                            .Property("DeletedBy")
                            .CurrentValue = null;
                    }
                }

                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry
                        .Property("CreatedAt")
                        .CurrentValue = DateTime.UtcNow;
                    entityEntry
                        .Property("CreatedBy")
                        .CurrentValue = "system";
                }
            }

            return entries;
        }
    }
}