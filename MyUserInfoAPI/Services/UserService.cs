using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyUserInfoAPI.Data;
using MyUserInfoAPI.Models;
using MyUserInfoAPI.Repos;

namespace MyUserInfoAPI.Services
{
    public class UserService
    {
        private readonly IRepo<User> _repo;

        public UserService(IRepo<User> repo)
        {
            _repo = repo;

        }
        public List<User> GetByLastName(string lastName)
        {
            return (from u in _repo.GetAll() where u.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase) select u).ToList();
        }
        public List<User> GetByFirstName(string firstName)
        {
            return (from u in _repo.GetAll() where u.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) select u).ToList();
        }
    }
}
