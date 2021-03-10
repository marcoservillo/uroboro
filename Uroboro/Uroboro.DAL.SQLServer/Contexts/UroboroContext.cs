using Microsoft.EntityFrameworkCore;
using System;
using Uroboro.Common.Models;
using Uroboro.DAL.Extensions;

namespace Uroboro.DAL.SQLServer.Contexts
{
    public class UroboroContext : DbContext
    {
        // Use for application execution
        public UroboroContext(DbContextOptions<UroboroContext> options) : base(options)
        {
        }

        // Another way to customize builder options without DI.

        #region USE FOR MIGRATION

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlServer("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=Uroboro;");

        /**
         * dotnet ef migrations add InitialCrate --project Uroboro.DAL.SQLServer
         * dotnet ef database update --project Uroboro.DAL.SQLServer
         * dotnet ef migrations add nome_migration --project Uroboro.DAL.SQLServer
         * dotnet ef database update --project Uroboro.DAL.SQLServer
         * ...
         **/

        #endregion USE FOR MIGRATION

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddQueryFilterToAllEntitiesAssignableFrom<IBaseItem>(qf => !qf.IsDeleted);
            modelBuilder.Entity<BaseItem>()
                .Property(p => p.Id).UseIdentityColumn();

            TodoItem todoItem = new()
            {
                Id = 1,
                Name = "UroboroItem",
                IsDeleted = false,
                IsCompleted = true,
                CreatedBy = "system",
                CreatedAt = DateTime.UtcNow
            };

            modelBuilder.Entity<TodoItem>().HasData(todoItem);
            modelBuilder.Ignore<BaseItem>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}