using Microsoft.EntityFrameworkCore;
using System;
using Uroboro.Common.Models;
using Uroboro.DAL.Extensions;

namespace Uroboro.DAL.InMemory.Contexts
{
    public class TodoItemsContext : DbContext
    {
        public TodoItemsContext(DbContextOptions<TodoItemsContext> options) : base(options)
        {
        }

        // Another way to customize builder options without DI
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        IConfigurationRoot configuration = new ConfigurationBuilder()
        //           .SetBasePath(Directory.GetCurrentDirectory())
        //           .AddJsonFile("appsettings.json")
        //           .Build();
        //        var connectionString = configuration.GetConnectionString("DbCoreConnectionString");
        //        optionsBuilder.UseSqlServer(connectionString);
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddQueryFilterToAllEntitiesAssignableFrom<IBaseItem>(qf => !qf.IsDeleted);

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