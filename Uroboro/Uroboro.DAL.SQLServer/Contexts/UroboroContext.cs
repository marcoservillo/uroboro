using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using Uroboro.Common.Models;

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

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseItem>().HasQueryFilter(qf => !qf.IsDeleted);

            UroboroItem uroboroItem = new()
            {
                Id = 1,
                Name = "UroboroItem",
                IsDeleted = false,
                IsCompleted = true,
                CreateBy = "system",
                CreateAt = DateTime.UtcNow
            };

            modelBuilder.Entity<UroboroItem>().HasData(uroboroItem);
            modelBuilder.Ignore<BaseItem>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UroboroItem> UroboroItems { get; set; }
    }
}