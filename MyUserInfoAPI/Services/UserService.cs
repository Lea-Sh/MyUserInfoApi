using System.Collections.Generic;
using System.Threading.Tasks;
using MyUserInfoAPI.Models;
using MyUserInfoAPI.Repos;

namespace MyUserInfoAPI.Services
{
    public class UserService : IService<User>
    {
        private readonly IRepo<User> _repo;

        public UserService(IRepo<User> repo)
        {
            _repo = repo;
        }

        public async Task<User> GetOneAsync(int? id)
        {
            return await _repo.GetOneAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
        public List<User> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<List<User>> GetByLastNameAsync(string lastName)
        {
            return await _repo.GetByAsync("LastName", lastName);
        }

        public async Task<List<User>> GetByFirstNameAsync(string firstName)
        {
            return await _repo.GetByAsync("FirstName", firstName);
        }

        public async Task<int> AddAsync(User user)
        {
            return await _repo.AddAsync(user);
        }

        public async Task<int> SaveAsync(User user)
        {
            return await _repo.SaveAsync(user);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<int> DeleteAsync(User user)
        {
            return await _repo.DeleteAsync(user);
        }

    }
}
