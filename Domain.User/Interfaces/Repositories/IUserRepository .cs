using System;
using Domain.User.Entities;

namespace Domain.User.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<Entities.User, Guid>
    {
        void Save(Entities.User user);        
    }
}