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
            var sql = $"SELECT * FROM Users WHERE {property} LIKE '{value}'";

            return ExecuteQuery(sql);
        }

        public Task<List<User>> GetByAsync(string property, string value)
        {
            var sql = $"SELECT * FROM Users WHERE {property} LIKE '{value}'";
            return ExecuteQueryAsync(sql);
        }
    }
}
