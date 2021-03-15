using System.Collections.Generic;
using System.Threading.Tasks;
using Uroboro.Common.Models;
using Uroboro.DAL.Contexts;

namespace Uroboro.BL.Managers
{
    public interface IBaseManager<TContext, TEntity>
        where TContext : BaseContext
        where TEntity : BaseItem
    {
        Task<IEnumerable<TEntity>> Read();

        Task<TEntity> Details(long? id);

        Task<TEntity> Create(TEntity todoItem);

        Task<TEntity> Update(TEntity todoItem);

        Task<long?> Delete(long id);
    }
}