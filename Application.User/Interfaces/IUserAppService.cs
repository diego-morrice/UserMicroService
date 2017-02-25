using System;
using Application.User.Interfaces.Common;
using Application.User.Messages;
using Infrastructure.CrossCutting.Validation;

namespace Application.User.Interfaces
{
    public interface IUserAppService : IAppService<Domain.User.Entities.User, Guid>
    {
        ValidationResult GetByToken(string token, out UserMessage user);
        ValidationResult SignIn(SignInMessage sign, out string token);
    }
}
