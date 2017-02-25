using Domain.User.Interfaces.Aggregate;
using System;
using System.Collections.Generic;

namespace Domain.User.Interfaces.Services
{
    public interface IService<TEntity, TKey> : IDisposable
       where TEntity : class, IAggregateRoot
    {
        TEntity GetById(TKey id);
        IEnumerable<TEntity> Search(Func<TEntity, bool> expr);
        IEnumerable<TEntity> GetAll();
    }
}