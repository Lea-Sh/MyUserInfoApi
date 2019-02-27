using System.Collections.Generic;
using System.Linq;
using MyUserInfoAPI.Models;

namespace MyUserInfoAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(UserContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new List<User>
            {
                new User {FirstName = "Dave", LastName = "Brenner"},
                new User {FirstName = "Matt", LastName = "Walton"},
                new User {FirstName = "Steve", LastName = "Hagen"},
                new User {FirstName = "Pat", LastName = "Walton"},
                new User {FirstName = "Bad", LastName = "User"},
            };
            users.ForEach(x => context.Users.Add(x));
            context.SaveChanges();
        }
    }
}
