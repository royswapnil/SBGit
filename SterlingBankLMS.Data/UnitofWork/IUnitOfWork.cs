using SterlingBankLMS.Data.Database;
using SterlingBankLMS.Data.Repository;
using System;

namespace SterlingBankLMS.Data.UnitofWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDbContext Context { get;  }
        int SaveChanges();
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        void BeginTransaction();
        int Commit();
        void Rollback();
    }
}
