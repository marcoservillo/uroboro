using Microsoft.EntityFrameworkCore;
using System;
using Uroboro.Common.Models;
using Uroboro.DAL.Contexts;

namespace Uroboro.DAL.InMemory.Contexts
{
    public class TodoItemsContext : BaseContext
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
            TodoItem todoItem = new()
            {
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
}