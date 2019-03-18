﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyUserInfoAPI.Services
{
    public interface IService<T>
    {
        Task<T> GetOneAsync(int? id);
        Task<List<T>> GetAllAsync();

        Task<int> AddAsync(T entity);

        Task<int> SaveAsync(T entity);
        Task<int?> SaveAsync(int id, T entity);

        Task<T> DeleteAsync(int id);
        Task<int> DeleteAsync(T entity);
    }
}
