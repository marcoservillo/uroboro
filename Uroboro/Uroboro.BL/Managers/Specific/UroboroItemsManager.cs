using Uroboro.Common.Models;
using Uroboro.DAL.Repos;

namespace Uroboro.BL.Managers.Specific
{
    public class UroboroItemsManager : BaseManager<TodoItem>
    {
        public UroboroItemsManager(IBaseRepo<TodoItem> repo) : base(repo)
        {
        }
    }
}