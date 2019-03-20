using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyUserInfoAPI.Models;

namespace MyUserInfoAPI.Services
{
    public interface IService<T>
    {
        Task<T> GetOneAsync(int? id);
        Task<List<T>> GetAllAsync();

        Task<Result<T>> AddAsync(T entity);

        Task<Result<T>> SaveAsync(int id, T entity);

        Task<Result<T>> DeleteAsync(int id);
    }
}
