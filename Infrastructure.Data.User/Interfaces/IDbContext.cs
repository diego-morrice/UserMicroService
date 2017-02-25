using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Domain.User.Interfaces.Aggregate;

namespace Infrastructure.Data.User.Interfaces
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class, IAggregateRoot;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class, IAggregateRoot;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void Dispose();
        DbRawSqlQuery<T> SqlQuery<T>(string sql, params object[] parameters);
    }
}