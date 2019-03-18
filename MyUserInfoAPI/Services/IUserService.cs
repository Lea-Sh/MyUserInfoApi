using System.Collections.Generic;
using System.Threading.Tasks;
using MyUserInfoAPI.Models;

namespace MyUserInfoAPI.Services
{
    public interface IUserService : IService<User>
    {
        Task<List<User>> GetByFirstNameAsync(string firstName);
        Task<List<User>> GetByLastNameAsync(string lastName);
    }
}
