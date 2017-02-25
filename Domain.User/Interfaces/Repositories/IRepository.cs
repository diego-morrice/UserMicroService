using Domain.User.Interfaces.Aggregate;
using System;
using System.Collections.Generic;

namespace Domain.User.Interfaces.Repositories
{
    public interface IRepository<TEntity, TKey> : IDisposable
        where TEntity : class, IAggregateRoot
    {
        TEntity GetById(TKey id);
        bool Any(Func<TEntity, bool> expr);
        IEnumerable<TEntity> Search(Func<TEntity, bool> expr);
        IEnumerable<TEntity> GetAll();
    }
}