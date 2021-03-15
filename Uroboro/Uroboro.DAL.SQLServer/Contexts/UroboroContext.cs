using Microsoft.EntityFrameworkCore;
using System;
using Uroboro.Common.Models;
using Uroboro.DAL.Contexts;
using Uroboro.DAL.Extensions;

namespace Uroboro.DAL.SQLServer.Contexts
{
    public class UroboroContext : BaseContext
    {
        // Use for application execution
        public UroboroContext(DbContextOptions<UroboroContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            TodoItem todoItem = new()
            {
                Id = 1,
                Name = "UroboroItem"
            };

            modelBuilder
                .Entity<TodoItem>()
                .HasData(new
                {
                    todoItem.Id,
                    todoItem.Name,
                    IsDeleted = false,
                    IsCompleted = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "system",
                    ModifiedAt = DateTime.UtcNow,
                    ModifiedBy = "system"
                });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }

    #region USE FOR MIGRATION

    public class UroboroMigrationContext : DbContext
    {
        // Another way to customize builder options without DI.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=Uroboro;");

        /**
         * dotnet ef migrations add nome_migration --project Uroboro.DAL.SQLServer --context UroboroMigrationContext
         * dotnet ef database update --project Uroboro.DAL.SQLServer --context UroboroMigrationContext
         * ...
         **/

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

            TodoItem todoItem = new()
            {
                Id = 1,
                Name = "UroboroItem"
            };

            modelBuilder
                .Entity<TodoItem>()
                .HasData(new
                {
                    todoItem.Id,
                    todoItem.Name,
                    IsDeleted = false,
                    IsCompleted = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "system",
                    ModifiedAt = DateTime.UtcNow,
                    ModifiedBy = "system"
                });

            modelBuilder
                .Ignore<BaseItem>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }

    #endregion USE FOR MIGRATION
}