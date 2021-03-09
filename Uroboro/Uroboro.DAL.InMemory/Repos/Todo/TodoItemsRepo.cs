using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Uroboro.Common.Models;
using Uroboro.DAL.InMemory.Contexts;
using Uroboro.DAL.Repos;

namespace Uroboro.DAL.InMemory.Repos.Todo
{
    public class TodoItemsRepo : IBaseRepo<TodoItem>
    {
        private readonly TodoItemsContext _context;

        public TodoItemsRepo(TodoItemsContext context) => _context = context;

        public async Task<IEnumerable<TodoItem>> Read()
        {
            var todoItems = await _context.TodoItems.AsNoTracking().ToListAsync();
            return todoItems;
        }

        public async Task<TodoItem> Details(long? id)
        {
            var todoItem = await _context.TodoItems.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            return todoItem;
        }

        public async Task<TodoItem> Create(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem> Update(TodoItem todoItem)
        {
            // To Avoid tracking error
            var entityToDetach = await _context.TodoItems.FirstOrDefaultAsync(m => m.Id == todoItem.Id);
            _context.Entry(entityToDetach).State = EntityState.Detached;
            _context.Update(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<long> Delete(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return id;
        }
    }
}