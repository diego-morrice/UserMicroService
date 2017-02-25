using System.Threading.Tasks;
using Infrastructure.Data.User.Interfaces;

namespace Application.User.Interfaces.Common
{
    public interface ITransactionAppService<TContext>
        where TContext : IDbContext
    {
        void BeginTransaction();
        void Commit();
        Task CommitAsync();
    }
}
