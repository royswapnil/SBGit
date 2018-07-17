using SterlingBankLMS.Data.Database;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

public class BaseContext : DbContext, IDbContext
{
    private IDbTransaction _transaction;

    public BaseContext()
        : this("DefaultConnection")
    {

    }

    public BaseContext(string nameOrConnectionString)
        : base(nameOrConnectionString)
    {

    }

    public new IDbSet<T> Set<T>() where T : class
    {
        return base.Set<T>();
    }

   
    void IDbContext.BeginTransaction()
    {
        Configuration.AutoDetectChangesEnabled = false;
        if (Database.Connection.State != ConnectionState.Open)
            Database.Connection.Open();

        _transaction = Database.BeginTransaction().UnderlyingTransaction;
    }

    int IDbContext.Commit()
    {
        ChangeTracker.DetectChanges();
        var result = SaveChanges();
        _transaction.Commit();
        
        return result;
    }

    void IDbContext.Rollback()
    {
        if (_transaction != null)
        {
            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }
    }

    public IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters)
    {
        return Database.SqlQuery<T>(sql, parameters);
    }

    public int ExecuteSqlCommand(string sql, params object[] parameters)
    {
        return Database.ExecuteSqlCommand(sql, parameters);
    }
}