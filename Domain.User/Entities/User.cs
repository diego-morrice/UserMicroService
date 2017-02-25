using System;
using System.Net.Mail;
using Domain.User.i18n;
using Domain.User.Interfaces.Aggregate;
using Domain.User.Interfaces.Common;
using Infrastructure.CrossCutting.Tools.Extensions;
using Infrastructure.CrossCutting.Validation;
using Infrastructure.CrossCutting.Validation.Interfaces.Validation;
using Infrastructure.CrossCutting.Security;
using System.Collections.Generic;
using System.Linq;

namespace Domain.User.Entities
{
    public class User : IAggregateRoot, ISelfValidation, IAuditableEntity
    {

        #region Private fields and Constructors

        private ValidationResult _validateResult;
        public User() { }
        public User(string name, string email, string tokenFacebook, string tokenGoogle, Address address, PersonalData personalData, bool guest = false)
        {
            Guest = guest;
            Active = true;
            _validateResult = new ValidationResult();

            if (!guest)
            {

                ValidateName(name);
                ValidateEmail(email);
                Name = name;
                Email = email;           
            }
            else
            {
                Name = "guest_" + Guid.NewGuid().ToString().Replace("-", "");
                Email = Name + "@fourohfour.com";
            }           

            AddFacebookToken(tokenFacebook);
            AddGoogleToken(tokenGoogle);
            AddPersonalData(personalData);
            AddAddress(address);
        }

        public User(string name, string email, string password)
        {

            _validateResult = new ValidationResult();

            ValidateName(name);
            ValidateEmail(email);
            ValidatePassword(password);

            Name = name;
            Email = email;
            Password = Cryptography.EncryptSHA1(password);
            Active = false; //this can be true if we will need to test for development
            Guest = false;
        }

        public User(string name, string email, string password, Address address, PersonalData personalData)
            : this(name, email, password)
        {
            _validateResult = new ValidationResult();
            AddPersonalData(personalData);
            AddAddress(address);
        }
        #endregion

        #region Public fields
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public bool Active { get; private set; }
        public string Password { get; private set; }
        public bool Guest { get; private set; }
        public Address Address { get; private set; }
        public PersonalData PersonalData { get; private set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; }

        public string ActiveToken
        {
            get
            {
                if (AutenticationToken != null && AutenticationToken.Any(f => f.Active))
                    return AutenticationToken.First(f => f.Active).Token;

                return null;
            }
        }

        public ValidationResult ValidationResult =>
            new ValidationResult().Add(_validateResult, PersonalData.ValidationResult, Address.ValidationResult);

        public List<AutenticateToken> AutenticationToken { get; private set; }
        public virtual FacebookUser FacebookUser { get; private set; }
        public virtual GoogleUser GoogleUser { get; private set; }
        #endregion

        #region Behaviors
        public bool IsValid
        {
            get
            {
                return ValidationResult.IsValid && Address.IsValid && PersonalData.IsValid;
            }
        }
        public bool IsNotValid => !IsValid;
        public void EditUser(string name, string email)
        {
            ValidateName(name);
            ValidateEmail(email);

            Name = name;
            Email = email;
        }
        public void ChangeActive(bool active)
        {
            Active = active;
        }
        public bool IsFacebookUser()
        {
            return FacebookUser.IsNotNull() && FacebookUser.Token.IsNotNull();
        }
        public bool IsGoogleUser()
        {
            return GoogleUser.IsNotNull() && GoogleUser.Token.IsNotNull();
        }
        public void ChangePassword(string activePassword, string newPassword, string confirmPassword)
        {
            if (Password != Cryptography.EncryptSHA1(activePassword) || newPassword != confirmPassword)
            {
                ValidationResult.Add("Password", ValidationMessages.Invalid_Password_Match);
                return;
            }

            Password = newPassword;

        }
        public void AddAddress(Address address)
        {
            if (address.IsNull())
                 address =  new Address();

            Address = address;
        }
        public void EditAddress(Address address)
        {
            if (Address.IsNull())
                return;

            Address = Address;
        }
        public void AddFacebookToken(string token)
        {
            if (token.IsNull())
                return;

            FacebookUser = new FacebookUser(token);
        }
        public void AddGoogleToken(string token)
        {
            if (token.IsNull())
                return;

            GoogleUser = new GoogleUser(token);
        }
        public void AddPersonalData(PersonalData personalData)
        {
            if (personalData.IsNull())
                personalData = new PersonalData();

            PersonalData = personalData;
        }
        public void EditPersonalData(PersonalData personalData)
        {
            if (personalData.IsNull())
                return;

            PersonalData = personalData;
        }

        public AutenticateToken GenerateAutenticationToken()
        {
            if (AutenticationToken == null)
                AutenticationToken = new List<AutenticateToken>();

            var aut = new AutenticateToken(this);
            AutenticationToken.Add(aut);
            return aut;
        }

        #endregion

        #region Validates
        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
                ValidationResult.Add("Name", ValidationMessages.Invalid_Name);

            if (name.Length > 50)
                ValidationResult.Add("Name", ValidationMessages.Invalid_Name_Exceed);
        }
        private void ValidateEmail(string email)
        {
            try
            {
                var m = new MailAddress(email);

                if (email.Length > 150)
                    ValidationResult.Add("Email", ValidationMessages.Invalid_Email_Exceed);
            }
            catch (FormatException)
            {
                ValidationResult.Add("Email", ValidationMessages.Invalid_Email);
            }
        }
        private void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                ValidationResult.Add("Password", ValidationMessages.Invalid_Password);

            if (password.Length > 20)
                ValidationResult.Add("Password", ValidationMessages.Invalid_Password_Exceed);
        }
        #endregion
    }
}