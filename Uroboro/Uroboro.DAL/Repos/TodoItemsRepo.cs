using Uroboro.Common.Models;
using Uroboro.DAL.Contexts;
using Uroboro.DAL.Repos;

namespace Uroboro.DAL.InMemory.Repos.Todo
{
    public class TodoItemsRepo<TContext> : BaseRepo<TContext, TodoItem>
        where TContext : BaseContext
    {
        public TodoItemsRepo(TContext context) : base(context)
        {
        }
    }
}