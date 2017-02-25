using System.Data.Entity;

namespace Infrastructure.Data.User.Interfaces
{
    public interface IUserContext : IDbContext
    {
        DbSet<Domain.User.Entities.User> Users { get; }
    }
}
