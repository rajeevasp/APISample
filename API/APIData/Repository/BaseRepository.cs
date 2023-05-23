using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Data.Configuration;
using API.Domain.Common;
using API.Domain.Repository;

namespace API.Data.Repository
{
    public enum Operations
    {
        Add = 0,
        Update,
        Delete
    }

    /// <summary>
    /// A generic base class to perform all the standard crud 
    /// operations on the database.
    /// </summary>

    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly DomainNameDbContext _dbContext;
        private IDbSet<T> _dbSet;

        protected DataContextCollection DataContexts
        {
            get;
            private set;
        }

        public BaseRepository(DomainNameDbContext context)
        {
            this._dbContext = context;
            this._dbSet = context.Set<T>();
        }

        /// <summary>
        /// Get a list of the requestsed object.
        /// </summary>
        /// <returns>A list of object type t</returns>
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList<T>();
        }

        public async Task<IQueryable<T>> GetAllAync()
        {
            return (await _dbSet.ToListAsync()).AsQueryable();
        }

        /// <summary>
        /// Gets a list of objects matching the predicate argument
        /// </summary>
        /// <param name="predicate">The argument</param>
        /// <returns>A list of object type t</returns>
        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate);
        }

        /// <summary>
        /// Gets a single object.
        /// </summary>
        /// <param name="predicate">The argument</param>
        /// <returns>An object of type T</returns>
        public T Single(Func<T, bool> predicate)
        {
            return _dbSet.Single(predicate);
        }

        /// <summary>
        /// Get the object matching the predicate argument.
        /// </summary>
        /// <param name="predicate">The argument</param>
        /// <returns>An object of type T</returns>
        public T Get(Func<T, bool> predicate)
        {
            return _dbSet.First(predicate);
        }

        /// <summary>
        /// Gets an object via the guid id.
        /// </summary>
        /// <param name="id">The guid of the object</param>
        /// <returns>The object of type T</returns>
        public async Task<T> Get(Guid id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Gets an object via the object reference.
        /// </summary>
        /// <param name="obj">The object to search for</param>
        /// <returns>The object of type T</returns>
        public async Task<T> Get(T obj)
        {
            return _dbSet.Find(obj);
        }

        #region async calls
        /// <summary>
        /// Saves an object to the database.
        /// </summary>
        /// <param name="t">The object to save</param>
        /// <returns></returns>
        public async Task<int> AddAsync(T t)
        {

            _dbContext.Set<T>().Add(t);
            return await _dbContext.SaveChangesAsync();

        }

        /// <summary>
        /// Removes an object from database
        /// </summary>
        /// <param name="t">The object to remove</param>
        /// <returns></returns>
        public async Task<int> RemoveAsync(T t)
        {
            try
            {
                _dbContext.Entry(t).State = EntityState.Deleted;
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Deletes an object from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(Guid id)
        {

            var objToDelete = _dbSet.Find(id);
            if (objToDelete == null)
                return 0;
            _dbSet.Remove(objToDelete);
            return await _dbContext.SaveChangesAsync();

        }
        /// <summary>
        /// Update an object in database
        /// </summary>
        /// <param name="t">The object to update</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(T t)
        {
            try
            {
                _dbContext.Entry(t).State = EntityState.Modified;
                return await _dbContext.SaveChangesAsync();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex) {
                return 0;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(match);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _dbContext.Set<T>().Where(match).ToListAsync();
        }
        #endregion
    }
}
