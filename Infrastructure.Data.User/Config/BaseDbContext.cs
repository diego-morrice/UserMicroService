using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using Domain.User.Interfaces.Aggregate;
using Infrastructure.Data.User.Interfaces;

namespace Infrastructure.Data.User.Config
{
    public class BaseDbContext : DbContext, IDbContext
    {
        public BaseDbContext(string connectionStringName)
            : base(connectionStringName)
        {
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class, IAggregateRoot
        {
            return base.Set<TEntity>();
        }

        public new DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class, IAggregateRoot
        {
            return base.Entry(entity);
        }

        public DbRawSqlQuery<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<T>(sql, parameters);
        }

        public ObjectContext GetObjectContext()
        {
            var objectContextAdapter = this as IObjectContextAdapter;

            return objectContextAdapter.ObjectContext;
        }
    }
}
