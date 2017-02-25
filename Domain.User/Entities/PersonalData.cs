using System;
using Infrastructure.CrossCutting.Validation;
using Infrastructure.CrossCutting.Validation.Interfaces.Validation;
using Infrastructure.CrossCutting.Tools.Extensions;
using Domain.User.i18n;

namespace Domain.User.Entities
{
    public class PersonalData : ISelfValidation
    {
        public PersonalData()
        {
            ValidationResult = new ValidationResult();
        }

        public PersonalData(string fullName, DateTime birthDate, string genre, string phoneNumber, string countryCode)
            : this()
        {
            ValidationResult = new ValidationResult();

            FullName = fullName;
            BirthDate = birthDate;
            Genre = genre;
            PhoneNumber = phoneNumber;
            CountryCode = countryCode;

            Validate();
        }

        public string FullName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Genre { get; private set; }
        public string PhoneNumber { get; private set; }
        public string CountryCode { get; private set; }

        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid
        {
            get
            {
                return ValidationResult.IsValid;
            }
        }
        public bool IsNotValid => !IsValid;
        public void EditPersonalData(string fullName, DateTime birthDate, string genre, string phoneNumber, string ddiPhoneNumber)
        {
            FullName = fullName;
            BirthDate = birthDate;
            Genre = Genre;
            PhoneNumber = phoneNumber;
            CountryCode = ddiPhoneNumber;

            Validate();
        }
        private void Validate()
        {
            ValidatePhoneNumber(PhoneNumber);
            ValidateCountryCode(CountryCode);
            ValidateGenre(Genre);
            ValidateFullName(FullName);
        }
        private void ValidatePhoneNumber(string phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber) && (phoneNumber.Length > 15 || phoneNumber.Length < 3))
                ValidationResult.Add("Phone", ValidationMessages.Invalid_Phone);
        }
        private void ValidateCountryCode(string countryCode)
        {
            if (!string.IsNullOrWhiteSpace(countryCode) && (countryCode.Length > 1 || countryCode.Length < 3))
                ValidationResult.Add("CountryCode", ValidationMessages.Invalid_Country_Code);
        }
        private void ValidateGenre(string genre)
        {
            if (genre.IsNotNull())
            {
                var newGenre = Convert.ToString(genre).ToLower();

                if (newGenre.IsNullOrWhiteSpace() || (newGenre != "m" && newGenre != "f"))
                    ValidationResult.Add("Genre", ValidationMessages.Genre_Not_Found);
            }
        }
        private void ValidateFullName(string fullName)
        {
            if (!fullName.IsNullOrWhiteSpace() && ((fullName.Length < 2) || fullName.Length > 150))
                ValidationResult.Add("Name", ValidationMessages.Invalid_Name);
        }
    }
}
