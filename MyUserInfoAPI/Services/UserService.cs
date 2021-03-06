﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MyUserInfoAPI.Models;
using MyUserInfoAPI.Repos;
using MyUserInfoAPI.Validators;

namespace MyUserInfoAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;

        public UserService(IUserRepo repo)
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
            return await _repo.GetByLastNameAsync(lastName);
        }

        public async Task<List<User>> GetByFirstNameAsync(string firstName)
        {
            return await _repo.GetByFirstNameAsync(firstName);
        }

        public async Task<Result<User>> AddAsync(User user)
        {
            var result = new Result<User>();
            result.Entity = user;

            if (!(new UserValidator().Validate(user)))
            {
                result.Status = Status.ValidationError;
            }
            else
            {
                result.Status = await _repo.AddAsync(user) == 1 ? Status.Ok : Status.Failed;
            }

            return result;
        }

        public async Task<Result<User>> SaveAsync(int id, User user)
        {
            var result = new Result<User>();
            if (id != user.UserId)
            {
                result.Status = Status.Failed;
                result.Entity = null;
                return result;
            }
            if (!(new UserValidator().Validate(user)))
            {
                result.Entity = user;
                result.Status = Status.ValidationError;
                return result;
            }

            result.Entity = user;
            result.Status = await _repo.SaveAsync(user) == 1 ? Status.Ok : Status.Failed;

            return result;
        }

        public async Task<Result<User>> DeleteAsync(int id)
        {
            var result = new Result<User>();

            result.Entity = await _repo.GetOneAsync(id);

            if (result.Entity != null)
            {
                result.Status = await _repo.DeleteAsync(result.Entity) == 1 ? Status.Ok : Status.Failed;
            }
            else
            {
                result.Status = Status.NotFound;
            }

            return result;
        }

    }
}
