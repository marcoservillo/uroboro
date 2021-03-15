using System.Collections.Generic;
using Uroboro.Common.Models;

namespace Uroboro.BL.Managers
{
    public class UsersManager
    {
        public static IEnumerable<User> GetUsers()
        {
            string fakeName = "John";
            string fakeSurname = "Doe";
            return new List<User>()
            {
                new User() { Username = "admin", Password = "password", Name = fakeName, Surname = fakeSurname, FullName = UsersManager.GetFullName(fakeName, fakeSurname), Email = "admin@domain.com" }
            };
        }

        private static string GetFullName(string name, string surname)
        {
            return $"{name} {surname}";
        }
    }
}