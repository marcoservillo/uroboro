using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Uroboro.Common.Models;
using Uroboro.DAL.InMemory.Contexts;
using Uroboro.DAL.Repos;

namespace Uroboro.DAL.InMemory.Repos
{
    public static class ReposServiceExtensions
    {
        public static IServiceCollection AddTodoRepo<TEntity>(this IServiceCollection services)
            where TEntity : BaseItem
        {
            services.AddDbContext<TodoItemsContext>(
                options => options.UseInMemoryDatabase("TodoItems"));
            services.AddTransient<IBaseRepo<TEntity>, BaseRepo<TodoItemsContext, TEntity>>();

            return services;
        }
    }
}