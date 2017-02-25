using System;
using System.Collections.Generic;
using Domain.User.Interfaces.Aggregate;

namespace Application.User.Interfaces.Common
{
    public interface IAppService<TEntity, TKey> : IDisposable
        where TEntity : class, IAggregateRoot
    {
        TEntity GetById(TKey id);
        IEnumerable<TEntity> GetAll();
    }
}
