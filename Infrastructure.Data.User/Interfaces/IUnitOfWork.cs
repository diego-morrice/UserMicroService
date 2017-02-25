using System.Threading.Tasks;

namespace Infrastructure.Data.User.Interfaces
{
    public interface IUnitOfWork<TContext>
        where TContext : IDbContext
    {
        void BeginTransaction();
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}