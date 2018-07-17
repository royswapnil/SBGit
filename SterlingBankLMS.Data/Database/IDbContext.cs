using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SterlingBankLMS.Data.Database
{
    public interface IDbContext : IDisposable
    {
        IDbSet<T> Set<T>() where T : class;
        int SaveChanges();
        void BeginTransaction();
        int Commit();
        void Rollback();
        IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters);
        int ExecuteSqlCommand(string sql, params object[] parameters);
    }
}
