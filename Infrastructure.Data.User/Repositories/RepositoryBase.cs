using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Domain.User.Interfaces.Aggregate;
using Domain.User.Interfaces.Repositories;
using Infrastructure.Data.User.Interfaces;

namespace Infrastructure.Data.User.Repositories
{
    public class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IAggregateRoot
    {
        public IDbContext Context { get; }
        public DbSet<TEntity> DbSet { get; }

        public RepositoryBase(IDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public TEntity GetById(TKey id)
        {
            return DbSet.Find(id);
        }

        public bool Any(Func<TEntity, bool> expr)
        {
            return DbSet.Any(expr);
        }

        public IEnumerable<TEntity> Search(Func<TEntity, bool> expr)
        {
            return DbSet.Where(expr);
        }      

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            Context?.Dispose();
        }

        #endregion
    }
}