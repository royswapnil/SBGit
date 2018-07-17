using SterlingBankLMS.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SterlingBankLMS.Data.Service
{
    /// <summary>
    /// Generic Service thats shields the Generic Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IService<TEntity> : IDisposable where TEntity : class
    {
        IUnitOfWork UnitOfWork { get; }
        void Add(TEntity entity);
        void Add(IEnumerable<TEntity> entities);
        void Update(params TEntity[] entities);

        int Total(Expression<Func<TEntity, bool>> predicate);

        void Delete(IEnumerable<TEntity> entities);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool track = false);
        List<TEntity> GetAll(bool track = false);
        TEntity GetById(int id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate, bool track = false);
        IEnumerable<TEntity> ExecuteProcedure(string procedure, params object[] @params);
        TEntity GetIncluding(Expression<Func<TEntity, bool>> predicate, bool track = false, params Expression<Func<TEntity, object>>[] properties);
        IQueryable<TEntity> GetAllIncluding(Expression<Func<TEntity, bool>> predicate, bool track, params Expression<Func<TEntity, object>>[] properties);
        IQueryable<TEntity> IncludeFilter(Expression<Func<TEntity, object>>[] predicate);
       


    }
}