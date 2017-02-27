using System;
using System.Linq;
using Domain.User.Events;
using Domain.User.Interfaces.Repositories;
using Domain.User.Interfaces.Services;
using Domain.User.i18n;
using Infrastructure.CrossCutting.EventBus;
using Infrastructure.CrossCutting.Tools.Extensions;
using Infrastructure.CrossCutting.Validation;

namespace Domain.User.Services
{

    public class UserService : ServiceBase<Entities.User, Guid>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public ValidationResult GetByToken(string token, out Entities.User user)
        {
            user = null;
            var result = _userRepository.Search(f => f.AutenticationToken.Any(x => x.Token == token)).FirstOrDefault();

            if (result.IsNull())
                return ValidationResult.Add("Token", ValidationMessages.Invalid_Token);

            var tokenObj = result.AutenticationToken.FirstOrDefault(f => f.Token == token);

            if (tokenObj == null || !tokenObj.Active)
                return ValidationResult.Add("Token", ValidationMessages.Invalid_Token);

            user = result;
            return ValidationResult;
        }
        private ValidationResult Save(Entities.User user)
        {
            if (user == null)
                return ValidationResult.Add("Account", ValidationMessages.Account_Not_Found);

            if (user.IsNotValid)
                return user.ValidationResult;

            var isNewUser = user.Id.Equals(Guid.Empty);

            _userRepository.Save(user);

            //Raise a event that will notify someone that a user has created
            //if (isNewUser) DomainEvent.Raise(new UserCreatedEvent(user.Id, user.Name, user.Email));

            return ValidationResult;
        }
        public ValidationResult SignIn(Entities.User user, out string token)
        {
            if (user.IsFacebookUser() || user.IsGoogleUser())
            {
                var userRead = _userRepository.Search(f =>
                (f.GoogleUser != null && user.GoogleUser != null && f.GoogleUser.Token == user.GoogleUser.Token) ||
                (f.FacebookUser != null && user.FacebookUser != null && f.FacebookUser.Token == user.FacebookUser.Token))
                .FirstOrDefault();

                if (userRead.IsNotNull())
                    user = userRead;
            }

            if (user != null && user.ActiveToken == null)
                user.GenerateAutenticationToken();

            var result = Save(user);
            token = user.ActiveToken;

            return result;

        }
    }
}
