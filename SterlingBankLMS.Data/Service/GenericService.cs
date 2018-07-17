using SterlingBankLMS.Data.Database;
using SterlingBankLMS.Data.Repository;
using SterlingBankLMS.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SterlingBankLMS.Data.Service
{
    public class GenericService<TEntity> : IService<TEntity> where TEntity : class
    {

        private readonly IRepository<TEntity> _repository;
        public IUnitOfWork UnitOfWork { get; private set; }

        private bool _disposed;

        public GenericService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _repository = UnitOfWork.Repository<TEntity>();
        }

        public IDbContext GetContext()
        {
            return UnitOfWork.Repository<TEntity>().GetContext();
        }

        int IService<TEntity>.Total(Expression<Func<TEntity, bool>> predicate)
        {

            return Count(predicate);
        }

        public virtual void Add(TEntity entity)
        {
            _repository.Create(entity);
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            _repository.Create(entities);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {

            _repository.Delete(entities);
        }

        public virtual void Update(params TEntity[] entities)
        {
            _repository.Update(entities);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate, bool track)
        {
            return _repository.Get(predicate, track);
        }

        public virtual TEntity Find(int id)
        {
            return _repository.Get(id);
        }

        public virtual List<TEntity> All(Expression<Func<TEntity, bool>> predicate, bool track)
        {

            return
                _repository.Fetch(predicate, track).ToList();
        }

        public virtual List<TEntity> All(bool track)
        {
            return
                _repository.Fetch(track).ToList();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {

            return _repository.Count(predicate);
        }

        public virtual TEntity GetIncluding(Expression<Func<TEntity, bool>> predicate, bool track, params Expression<Func<TEntity, object>>[] properties)
        {
            return _repository.GetAllIncluding(predicate, track, properties).FirstOrDefault();
        }

        public virtual IQueryable<TEntity> GetAllIncluding(Expression<Func<TEntity, bool>> predicate, bool track, params Expression<Func<TEntity, object>>[] properties)
        {
            return _repository.GetAllIncluding(predicate, track, properties);
        }

        void IService<TEntity>.Add(TEntity entity)
        {
            Add(entity);
        }

        void IService<TEntity>.Add(IEnumerable<TEntity> entities)
        {
            Add(entities);
        }

        void IService<TEntity>.Delete(IEnumerable<TEntity> entities)
        {
            Delete(entities);
        }

        List<TEntity> IService<TEntity>.GetAll(Expression<Func<TEntity, bool>> predicate, bool track)
        {
            return All(predicate, track);
        }
        List<TEntity> IService<TEntity>.GetAll(bool track)
        {
            return All(track);
        }
        TEntity IService<TEntity>.GetById(int id)
        {
            return Find(id);
        }

        TEntity IService<TEntity>.Get(Expression<Func<TEntity, bool>> predicate, bool track)
        {
            return Find(predicate, track);
        }

        void IService<TEntity>.Update(params TEntity[] entities)
        {
            Update(entities);
        }

        public virtual IEnumerable<T> ExecuteProcedure<T>(string procedureName, params object[] extraQueries) where T : class
        {
            var sb = new StringBuilder();
            sb.Append(string.Format("Exec {0} ", procedureName));

            var parameters = new List<object> { };

            foreach (var item in extraQueries)
                parameters.Add(item);

            var counter = parameters.Count();
            for (int i = 0; i < counter; i++) {
                sb.Append("@p" + i);
                if (i < counter - 1)
                    sb.Append(", ");
            }

            return UnitOfWork.Repository<T>().SqlQuery(sb.ToString(), parameters.ToArray());
        }

        IEnumerable<TEntity> IService<TEntity>.ExecuteProcedure(string procedure, params object[] @params)
        {
            return ExecuteProcedure<TEntity>(procedure, @params);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing) {
                UnitOfWork.Dispose();
            }
            _disposed = true;
        }

        TEntity IService<TEntity>.GetIncluding(Expression<Func<TEntity, bool>> predicate, bool track, params Expression<Func<TEntity, object>>[] properties)
        {
            return GetIncluding(predicate, track, properties);
        }

        IQueryable<TEntity> IService<TEntity>.GetAllIncluding(Expression<Func<TEntity, bool>> predicate, bool track, params Expression<Func<TEntity, object>>[] properties)
        {
            return GetAllIncluding(predicate, track, properties);
        }

        public IQueryable<TEntity> IncludeFilter(params Expression<Func<TEntity, object>>[] predicate)
        {
            return _repository.IncludeFilter(predicate);
        }
    }
}