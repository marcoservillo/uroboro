using Microsoft.EntityFrameworkCore;
using Uroboro.Common.Models;
using Uroboro.DAL.Repos;

namespace Uroboro.DAL.InMemory.Repos.Todo
{
    public class TodoItemsRepo<TContext> : BaseRepo<TContext, TodoItem>
        where TContext : DbContext
    {
        public TodoItemsRepo(TContext context) : base(context)
        {
        }
    }
}