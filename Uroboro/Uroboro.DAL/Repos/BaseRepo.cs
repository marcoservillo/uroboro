using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Uroboro.Common.Models;
using Uroboro.DAL.Contexts;

namespace Uroboro.DAL.Repos
{
    public class BaseRepo<TContext, TEntity> : IBaseRepo<TContext, TEntity>
        where TContext : BaseContext
        where TEntity : BaseItem
    {
        protected readonly TContext _context;

        public BaseRepo(TContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> Read()
        {
            var baseItem = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
            return baseItem;
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
            var itemToUpdate = await _context.Set<TEntity>().FindAsync(baseItem.Id);
            if(itemToUpdate != null)
            { 
                foreach (var prop in itemToUpdate.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    prop.SetValue(itemToUpdate, baseItem.GetType().GetProperty(prop.Name).GetValue(baseItem));
                }
                _context.Update(itemToUpdate);
                await _context.SaveChangesAsync();
            }
            return itemToUpdate;
        }

        public async Task<long?> Delete(long id)
        {
            var baseItem = await _context.Set<TEntity>().FindAsync(id);
            if(baseItem != null)
            {
                baseItem.IsDeleted = true;
                _context.Update(baseItem);
                await _context.SaveChangesAsync();
            }
            return baseItem?.Id;
        }
    }
}