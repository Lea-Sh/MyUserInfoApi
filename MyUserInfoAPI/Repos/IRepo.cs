using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyUserInfoAPI.Repos
{
    public interface IRepo<T>
    {
        Task<int> AddAsync(T entity);
        Task<int> AddRangeAsync(IList<T> entities);
        Task<int> SaveAsync(T entity);
        Task<int> DeleteAsync(int id);
        Task<int> DeleteAsync(T entity);
        Task<T> GetOneAsync(int? id);
        Task<List<T>> GetAllAsync();
    }
}
