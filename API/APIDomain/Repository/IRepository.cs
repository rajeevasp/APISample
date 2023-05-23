using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Domain.Repository
{
    /// <summary>
    /// Generic base methods that will be available to all aggregate objects
    /// </summary>
    /// <typeparam name="T">The type of aggregate object</typeparam>
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IQueryable<T>> GetAllAync();
        IEnumerable<T> Find(Func<T, bool> predicate);
        T Single(Func<T, bool> predicate);
        T Get(Func<T, bool> predicate);
        Task<T> Get(Guid id);
        Task<T> Get(T obj);
        // Task<int> Update(T obj);
        Task<int> Delete(Guid id);
        //Task<int> Add(T obj);
        //void SaveChanges();

        Task<int> AddAsync(T t);
        Task<int> RemoveAsync(T t);
        Task<List<T>> GetAllAsync();
        Task<int> UpdateAsync(T t);
        Task<int> CountAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match);
    }
}
