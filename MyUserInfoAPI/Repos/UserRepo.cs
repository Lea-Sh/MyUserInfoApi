using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyUserInfoAPI.Data;
using MyUserInfoAPI.Models;

namespace MyUserInfoAPI.Repos
{
    public class UserRepo: BaseRepo<User>, IUserRepo
    {
        public UserRepo()
        {
            Table = Context.Users;
        }

        public Task<int> DeleteAsync(int id)
        {
            Context.Entry(new User() { UserId = id }).State = EntityState.Deleted;
            return SaveChangesAsync();
        }

        public Task<List<User>> GetByLastNameAsync(string lastName)
        {
            return SearchByPropertyAsync(lastName, u => u.LastName);
        }
        public Task<List<User>> GetByFirstNameAsync(string firstName)
        {
            return SearchByPropertyAsync(firstName, u => u.FirstName);
        }

        private Task<List<User>> SearchByPropertyAsync(string propertyValue, Func<User, string> propertyGetter)
        {
            return Table.Where(x => propertyGetter(x).Equals(propertyValue, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }
    }
}
