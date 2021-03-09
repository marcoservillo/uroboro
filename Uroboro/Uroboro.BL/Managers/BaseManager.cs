using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Uroboro.Common.Models;
using Uroboro.DAL.Repos;

namespace Uroboro.BL.Managers
{
    public class BaseManager<TEntity> : IBaseManager<TEntity>
        where TEntity : BaseItem
    {
        protected readonly IBaseRepo<TEntity> _repo;

        public BaseManager(IBaseRepo<TEntity> repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<IEnumerable<TEntity>> Read()
        {
            var result = await _repo.Read();
            return result;
        }

        public async Task<TEntity> Details(long? id)
        {
            var result = await _repo.Details(id);
            return result;
        }

        public async Task<TEntity> Create(TEntity baseItem)
        {
            var result = await _repo.Create(baseItem);
            return result;
        }

        public async Task<TEntity> Update(TEntity baseItem)
        {
            var itemById = await _repo.Details(baseItem.Id);
            if (itemById == null)
            {
                return itemById;
            }
            var result = await _repo.Update(baseItem);
            return result;
        }

        public async Task<long?> Delete(long id)
        {
            var itemById = await _repo.Details(id);
            if (itemById == null)
            {
                return null;
            }
            var result = await _repo.Delete(id);
            return result;
        }
    }
}