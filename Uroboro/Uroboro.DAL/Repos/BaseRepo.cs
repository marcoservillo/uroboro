using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Uroboro.Common.Models;

namespace Uroboro.DAL.Repos
{
    public class BaseRepo<TContext, TEntity> : IBaseRepo<TContext, TEntity>
        where TContext : DbContext
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
            baseItem.CreatedAt = DateTime.UtcNow;
            // When there will be a claim I take User Name
            //baseItem.CreatedBy = ClaimsPrincipal.Current.Identity.Name;
            baseItem.CreatedBy = "system";
            _context.Set<TEntity>().Add(baseItem);
            await _context.SaveChangesAsync();
            return baseItem;
        }

        public async Task<TEntity> Update(TEntity baseItem)
        {
            // To Avoid tracking error
            baseItem.ModifiedAt = DateTime.UtcNow;
            // When there will be a claim I take User Name
            //baseItem.ModifiedBy = ClaimsPrincipal.Current.Identity.Name;
            baseItem.ModifiedBy = "system";
            var entityToDetach = await _context.Set<TEntity>().FirstOrDefaultAsync(m => m.Id == baseItem.Id);
            _context.Entry(entityToDetach).State = EntityState.Detached;
            _context.Update(baseItem);
            await _context.SaveChangesAsync();
            return baseItem;
        }

        public async Task<long> Delete(long id)
        {
            var baseItem = await _context.Set<TEntity>().FindAsync(id);
            baseItem.IsDeleted = true;
            baseItem.DeletedAt = DateTime.UtcNow;
            // When there will be a claim I take User Name
            //baseItem.DeletedBy = ClaimsPrincipal.Current.Identity.Name;
            baseItem.DeletedBy = "system";
            _context.Update(baseItem);
            await _context.SaveChangesAsync();
            return id;
        }
    }
}