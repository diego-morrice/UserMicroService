using System;
using System.Collections.Generic;
using Application.User.Interfaces;
using Application.User.Messages;
using Domain.User.Entities;
using Domain.User.Interfaces.Services;
using Infrastructure.CrossCutting.Tools.Extensions;
using Infrastructure.CrossCutting.Validation;
using Infrastructure.Data.User.Interfaces;

namespace Application.User
{
    public class UserAppService : AppServiceBase<IUserContext>, IUserAppService
    {
        private readonly IUserService _userService;

        public UserAppService(IUnitOfWork<IUserContext> uow, IUserService userService)
            : base(uow)
        {
            _userService = userService;
        }

        public void Dispose()
        {
            _userService.Dispose();
        }
        public Domain.User.Entities.User GetById(Guid id)
        {
            return _userService.GetById(id);
        }
        public IEnumerable<Domain.User.Entities.User> GetAll()
        {
            return _userService.GetAll();
        }
        public ValidationResult GetByToken(string token, out UserMessage userMessage)
        {
            userMessage = null;

            try
            {

                Domain.User.Entities.User user = null;
                var result = _userService.GetByToken(token, out user);

                userMessage = Parse(user);

                return result;

            }
            catch(Exception)
            {                
                return ValidationResult.Add("Error", "Lamentamos mas houve um erro.");
            }            
        }
        public ValidationResult SignIn(SignInMessage sign, out string token)
        {
            token = null;

            try
            {
                return _userService.SignIn(Parse(sign), out token); 
            }
            catch(Exception ex)
            {
                return ValidationResult.Add("Error", "Lamentamos mas houve um erro.");
            }            
        }
        private Domain.User.Entities.User Parse(SignInMessage sign)
        {
            if (sign.IsNull())
                return null;

            var address = new Address(sign.City, sign.State, sign.Country);
            var personalData = new PersonalData(sign.FullName, sign.BirthDateFormatted, sign.Genre, null, null);
            return new Domain.User.Entities.User(sign.Username, sign.email, sign.TokenFacebook, sign.TokenGoogle, address, personalData, sign.Guest);
        }
        private UserMessage Parse(Domain.User.Entities.User user)
        {
            if (user.IsNull())
                return null;

            return new UserMessage
            {
                Id = user.Id,
                Username = user.Name,
                email = user.Email,
                FullName = user.PersonalData.FullName,
                Genre = user.PersonalData.Gender,
                BirthDate = user.PersonalData.BirthDate,
                PhoneNumber = user.PersonalData.PhoneNumber,
                Country = user.Address.Country,
                City = user.Address.City,
                CountryCode = user.PersonalData.CountryCode,
                AddressLineOne = user.Address.AddressLineOne,
                AddressLineTwo = user.Address.AddressLineTwo,
                Number = user.Address.Number,
                State = user.Address.State,
                ZipCode = user.Address.ZipCode
            };
        }      
    }
}
