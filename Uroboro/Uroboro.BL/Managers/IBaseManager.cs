using System.Collections.Generic;
using System.Threading.Tasks;

namespace Uroboro.BL.Managers
{
    public interface IBaseManager<T>
    {
        Task<IEnumerable<T>> Read();

        Task<T> Details(long? id);

        Task<T> Create(T todoItem);

        Task<T> Update(T todoItem);

        Task<long?> Delete(long id);
    }
}