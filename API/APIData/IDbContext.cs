using System;
using System.Data.Entity;

namespace API.Data
{
    public interface IDbContext : IDisposable 
    {
        IDbSet<T> Set<T>() where T : class;
        int SaveChanges();
    }
}
