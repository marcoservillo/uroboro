using Uroboro.Common.Models;
using Uroboro.DAL.Repos;

namespace Uroboro.BL.Managers.Specific
{
    public class TodoItemsManager : BaseManager<TodoItem>
    {
        public TodoItemsManager(IBaseRepo<TodoItem> repo) : base(repo)
        {
        }
    }
}