using System;
using System.Data.Entity.Migrations;
using Domain.User.Interfaces.Repositories;
using Infrastructure.Data.User.Interfaces;

namespace Infrastructure.Data.User.Repositories
{
    public class UserRepository : RepositoryBase<Domain.User.Entities.User, Guid>, IUserRepository
    {
        public UserRepository(IDbContext context)
            : base(context)
        {

        }             
        

        public void Save(Domain.User.Entities.User user)
        {
            DbSet.AddOrUpdate(user);
            Context.SaveChanges();
        }
    }
}