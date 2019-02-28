using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyUserInfoAPI.Data;
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

        public async Task<List<User>> GetByLastNameAsync(string lastName)
        {
            var users = await _repo.GetAllAsync();
            return (from u in users
                where u.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)
                select u).ToList();
        }

        public async Task<List<User>> GetByFirstNameAsync(string firstName)
        {
            var users = await _repo.GetAllAsync();
            return (from u in users
                    where u.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)
                select u).ToList();
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

        /* public List<User> GetByLastName(string lastName)
         {
             return (from u in _repo.GetAll()
                 where u.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)
                 select u).ToList();
         }

         public List<User> GetByFirstName(string firstName)
         {
             return (from u in _repo.GetAll()
                 where u.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)
                 select u).ToList();
         }*/
    }
}
