using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyUserInfoAPI.Models;

namespace MyUserInfoAPI.Repos
{
    public interface IUserRepo : IRepo<User>
    {
        Task<List<User>> GetByLastNameAsync(string lastName);
        Task<List<User>> GetByFirstNameAsync(string firstName);
    }
}
