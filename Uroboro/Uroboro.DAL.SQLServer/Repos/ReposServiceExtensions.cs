using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Uroboro.Common.Models;
using Uroboro.DAL.Repos;
using Uroboro.DAL.SQLServer.Contexts;

namespace Uroboro.DAL.SQLServer.Repos
{
    public static class ReposServiceExtensions
    {
        public static IServiceCollection AddUroboroRepo<TEntity>(this IServiceCollection services, IConfiguration configuration)
            where TEntity : BaseItem
        {
            services.AddDbContext<UroboroContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DB"), providerOptions =>
                {
                    providerOptions.EnableRetryOnFailure();
                    providerOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
            });
            services.AddTransient<IBaseRepo<UroboroContext, TEntity>, BaseRepo<UroboroContext, TEntity>>();

            return services;
        }
    }
}