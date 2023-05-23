using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalWeather.CMS.API.Domain.Repository;

namespace UniversalWeather.CMS.API.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        //protected readonly DomainNameDbContext _dbContext;
        private IDbSet<T> _dbSet;
        public Repository()
        {

        }

        public Task<T> GetAsync(object id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets an object via the guid id.
        /// </summary>
        /// <param name="id">The guid of the object</param>
        /// <returns>The object of type T</returns>
        public T Get(T obj)
        {
            return _dbSet.Find(obj);
        }

        /// <summary>
        /// Gets an object via the guid id.
        /// </summary>
        /// <param name="id">The guid of the object</param>
        /// <returns>The object of type T</returns>
        //public Task<T> Get(object id)
        //{
        //    return _dbSet.Find(id);
        //}

        /// <summary>
        /// Saves an object to the database.
        /// </summary>
        /// <param name="obj">The object to save</param>
        public void Add(T obj)
        {
            _dbSet.Add(obj);
            //SaveChanges();
        }

        /// <summary>
        /// Delete an object via the guid. Not ideal as it first makes a query to
        /// find the object to delete then deletes it. This will change in EF 5
        /// where crud operations via id will become easier.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            var objToDelete = _dbSet.Find(id);
            _dbSet.Remove(objToDelete);
           // SaveChanges();
        }

        /// <summary>
        /// Update an object
        /// </summary>
        /// <param name="obj">The object to update</param>
        public void Update(T obj)
        {
            _dbSet.Attach(obj);
            //_dbContext.Entry(obj).State = System.Data.EntityState.Modified;
            //SaveChanges();
        }

    }
}
