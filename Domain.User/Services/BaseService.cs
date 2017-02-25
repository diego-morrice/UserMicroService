using Domain.User.Interfaces.Aggregate;
using Domain.User.Interfaces.Repositories;
using Domain.User.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Domain.User.Services
{
    public class BaseService<TEntity, TKey> : IService<TEntity, TKey>
        where TEntity : class, IAggregateRoot
    {
        #region Construtor

        public BaseService(IRepository<TEntity, TKey> repository)
        {
            Repository = repository;
        }

        #endregion

        #region Propriedades

        protected IRepository<TEntity, TKey> Repository { get; }

        public void Dispose()
        {
            Repository.Dispose();
        }

        #endregion

        #region Métodos

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Repository.GetAll();
        }

        public TEntity GetById(TKey id)
        {
            return Repository.GetById(id);
        }
      
        public virtual IEnumerable<TEntity> Search(Func<TEntity, bool> expr)
        {
            return Repository.Search(expr);
        }

        #endregion
    }
}