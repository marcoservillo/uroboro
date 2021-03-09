using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uroboro.Common.Models;
using Uroboro.DAL.InMemory.Repos;
using Uroboro.DAL.SQLServer.Repos;

namespace Uroboro.BL.Managers
{
    public static class ManagersServiceExtensions
    {
        public static IServiceCollection AddManagers(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTodoRepo<TodoItem>()
                .AddUroboroRepo<UroboroItem>(configuration);

            return services;
        }
    }
}