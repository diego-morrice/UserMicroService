using System;
using Infrastructure.CrossCutting.Validation;

namespace Domain.User.Interfaces.Services
{
    public interface IUserService : IService<Entities.User, Guid>
    {        
        ValidationResult GetByToken(string token, out Entities.User user);
        ValidationResult SignIn(Entities.User userMessage, out string token);
    }
}