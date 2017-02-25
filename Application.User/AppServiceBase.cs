using System.Threading.Tasks;
using Application.User.Interfaces.Common;
using Infrastructure.CrossCutting.Validation;
using Infrastructure.Data.User.Interfaces;

namespace Application.User
{
    public class AppServiceBase<TContext> : ITransactionAppService<TContext>
        where TContext : IDbContext
    {
        private readonly IUnitOfWork<TContext> _uow;

        protected ValidationResult ValidationResult { get; }

        public AppServiceBase(IUnitOfWork<TContext> uow)
        {
            _uow = uow;
            ValidationResult = new ValidationResult();
        }

        public void BeginTransaction()
        {
            _uow.BeginTransaction();
        }

        public void Commit()
        {
            _uow.SaveChanges();
        }

        public Task CommitAsync()
        {
            return _uow.SaveChangesAsync();
        }
    }
}
