using System.Collections.Generic;
using Uroboro.BL.Managers;
using Uroboro.Common.Models;

namespace Uroboro.SL.WebAPI.Services
{
    public class UsersService : IUsersService
    {
        private UsersManager _usersManager = null;

        public UsersService()
        {
            this._usersManager = new UsersManager();
        }
        public IEnumerable<User> GetUsers()
        {
            return UsersManager.GetUsers();
        }
    }
}
