using Uroboro.Common.Models;
using Uroboro.DAL.Contexts;
using Uroboro.DAL.Repos;

namespace Uroboro.BL.Managers.Specific
{
    public class TodoItemsManager<TContext> : BaseManager<TContext, TodoItem>
        where TContext : BaseContext
    {
        public TodoItemsManager(IBaseRepo<TContext, TodoItem> repo) : base(repo)
        {
        }
    }
}