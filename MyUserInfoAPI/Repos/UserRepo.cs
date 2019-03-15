using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyUserInfoAPI.Data;
using MyUserInfoAPI.Models;

namespace MyUserInfoAPI.Repos
{
    public class UserRepo: BaseRepo<User>, IRepo<User>
    {
        public UserRepo(UserContext context) : base(context)
        {
            Table = Context.Users;
        }

        public int Delete(int id)
        {
            Context.Entry(new User() { UserId = id }).State = EntityState.Deleted;
            return SaveChanges();
        }
        public Task<int> DeleteAsync(int id)
        {
            Context.Entry(new User() { UserId = id }).State = EntityState.Deleted;
            return SaveChangesAsync();
        }
        public List<User> GetBy(string property, string value)
        {
            switch (property)
            {
                case "LastName": return GetByLastName(value);
                case "FirstName": return GetByFirstName(value);
                default: return null;
            }
        }
        public Task<List<User>> GetByAsync(string property, string value)
        {
            switch (property)
            {
                case "LastName": return GetByLastNameAsync(value); 
                case "FirstName": return GetByFirstNameAsync(value);
                default: return null;
            }
        }
        private List<User> GetByLastName(string lastName)
        {
            return (from u in Table where u.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase) select u).ToList();
        }
        private Task<List<User>> GetByLastNameAsync(string lastName)
        {
            return (from u in Table where u.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase) select u).ToListAsync();
        }

        private List<User> GetByFirstName(string firstName)
        {
            return (from u in Table where u.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) select u).ToList();
        }
        private Task<List<User>> GetByFirstNameAsync(string firstName)
        {
            return (from u in Table where u.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) select u).ToListAsync();
        }
    }
}
