using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Uroboro.Common.Models;

namespace Uroboro.DAL.Repos
{
    public interface IBaseRepo<TContext, TEntity>
        where TContext : DbContext
        where TEntity : BaseItem
    {
        Task<IEnumerable<TEntity>> Read();

        Task<TEntity> Details(long? id);

        Task<TEntity> Create(TEntity baseItem);

        Task<TEntity> Update(TEntity baseItem);

        Task<long> Delete(long id);
    }
}