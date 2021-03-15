using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Uroboro.Common.Models;
using Uroboro.DAL.Contexts;
using Uroboro.DAL.Repos;

namespace Uroboro.BL.Managers
{
    public class BaseManager<TContext, TEntity> : IBaseManager<TContext, TEntity>
        where TContext : BaseContext
        where TEntity : BaseItem
    {
        protected readonly IBaseRepo<TContext, TEntity> _repo;

        public BaseManager(IBaseRepo<TContext, TEntity> repo)
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
            var result = await _repo.Update(baseItem);
            return result;
        }

        public async Task<long?> Delete(long id)
        {
            var result = await _repo.Delete(id);
            return result;
        }
    }
}