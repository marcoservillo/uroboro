using System.Collections.Generic;
using System.Threading.Tasks;
using Uroboro.Common.Models;

namespace Uroboro.DAL.Repos
{
    public interface IBaseRepo<T>
        where T : BaseItem
    {
        Task<IEnumerable<T>> Read();

        Task<T> Details(long? id);

        Task<T> Create(T baseItem);

        Task<T> Update(T baseItem);

        Task<long> Delete(long id);
    }
}