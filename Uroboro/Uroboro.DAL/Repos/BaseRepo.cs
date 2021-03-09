using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Uroboro.Common.Models;

namespace Uroboro.DAL.Repos
{
    public class BaseRepo<TContext, TEntity> : IBaseRepo<TEntity>
        where TContext : DbContext
        where TEntity : BaseItem
    {
        private readonly TContext _context;

        public BaseRepo(TContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> Read()
        {
            var uroboroItems = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
            return uroboroItems;
        }

        public async Task<TEntity> Details(long? id)
        {
            var baseItem = await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            return baseItem;
        }

        public async Task<TEntity> Create(TEntity baseItem)
        {
            _context.Set<TEntity>().Add(baseItem);
            await _context.SaveChangesAsync();
            return baseItem;
        }

        public async Task<TEntity> Update(TEntity baseItem)
        {
            // To Avoid tracking error
            var entityToDetach = await _context.Set<TEntity>().FirstOrDefaultAsync(m => m.Id == baseItem.Id);
            _context.Entry(entityToDetach).State = EntityState.Detached;
            _context.Update(baseItem);
            await _context.SaveChangesAsync();
            return baseItem;
        }

        public async Task<long> Delete(long id)
        {
            var baseItem = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Remove(baseItem);
            await _context.SaveChangesAsync();
            return id;
        }
    }
}