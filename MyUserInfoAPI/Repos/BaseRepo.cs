using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyUserInfoAPI.Data;

namespace MyUserInfoAPI.Repos
{
    public abstract class BaseRepo<T> where T : class, new()
    {
        protected UserContext Context { get; }
        protected DbSet<T> Table;

        protected BaseRepo()
        {
            Context = new UserContext();
        }
        public void Dispose()
        {
            Context?.Dispose();
        }

        internal async Task<int> SaveChangesAsync()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //Thrown when there is a concurrency error
                //for now, just rethrow the exception
                throw;
            }
            catch (DbUpdateException ex)
            {
                //Thrown when database update fails
                //Examine the inner exception(s) for additional
                //details and affected objects
                //for now, just rethrow the exception
                throw;
            }
            catch (Exception ex)
            {
                //some other exception happened and should be handled
                throw;
            }
        }

        public Task<T> GetOneAsync(int? id) => Table.FindAsync(id);
        public Task<List<T>> GetAllAsync() => Table.ToListAsync();


        public Task<int> AddAsync(T entity)
        {
            Table.Add(entity);
            return SaveChangesAsync();
        }

        public Task<int> AddRangeAsync(IList<T> entities)
        {
            Table.AddRange(entities);
            return SaveChangesAsync();
        }

        public Task<int> SaveAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return SaveChangesAsync();
        }

        public Task<int> DeleteAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            return SaveChangesAsync();
        }

//        public Task<List<T>> ExecuteQueryAsync(string sql)
//            => Table.FromSql(sql).ToListAsync();
    }
}
