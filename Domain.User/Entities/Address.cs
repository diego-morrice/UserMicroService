using Domain.User.i18n;
using Infrastructure.CrossCutting.Tools.Extensions;
using Infrastructure.CrossCutting.Validation;
using Infrastructure.CrossCutting.Validation.Interfaces.Validation;

namespace Domain.User.Entities
{
    public class Address : ISelfValidation
    {
        public Address()
        {
            ValidationResult = new ValidationResult();
        }

        public Address(string addressLineOne, string addressLineTwo, string number, string state, string city, string country, string zipCode)
            : this()
        {

            AddressLineOne = addressLineOne;
            AddressLineTwo = addressLineTwo;
            Number = number;
            State = state;
            City = city;
            Country = country;
            ZipCode = zipCode;


            Validate();
        }

        public Address(string state, string city, string country)
            : this()
        {            

            State = state;
            City = city;
            Country = country;

            Validate();
        }

        public string Country { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public string AddressLineOne { get; private set; }
        public string AddressLineTwo { get; private set; }
        public string Number { get; private set; }
        public string ZipCode { get; private set; }

        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid
        {
            get
            {
                return ValidationResult.IsValid;
            }
        }

        public bool IsNotValid => !IsValid;

        public void EditAddress(string addressLineOne, string addressLineTwo, string number, string state, string city, string country, string zipCode)
        {

            AddressLineOne = addressLineOne;
            AddressLineTwo = addressLineTwo;
            Number = number;
            State = state;
            City = city;
            Country = country;
            ZipCode = zipCode;

            Validate();
        }

        private void Validate()
        {
            ValidateAddress(AddressLineOne, ValidationMessages.Address_1);
            ValidateAddress(AddressLineTwo, ValidationMessages.Address_2);
            ValidateNumber(Number);
            ValidateCountry(Country);
            ValidateState(State);
            ValidateCity(City);
            ValidateZipCode(ZipCode);
        }

        private void ValidateAddress(string address, string addressType)
        {
            if (!address.IsNullOrWhiteSpace() && address.Length > 50)
                ValidationResult.Add("Address", string.Format(ValidationMessages.Invalid_Address_Exceed, addressType));
        }

        private void ValidateNumber(string number)
        {
            if (!number.IsNullOrWhiteSpace() && number.Length > 5)
                ValidationResult.Add("Number", ValidationMessages.Invalid_Number_Exceed);
        }

        private void ValidateCountry(string country)
        {
            if (!country.IsNullOrWhiteSpace() && country.Length > 20)
                ValidationResult.Add("Country", ValidationMessages.Invalid_Country_Exceed);
        }

        private void ValidateState(string state)
        {
            if (!state.IsNullOrWhiteSpace() && state.Length > 50)
                ValidationResult.Add("State", ValidationMessages.Invalid_State_Exceed);
        }

        private void ValidateCity(string city)
        {
            if (!city.IsNullOrWhiteSpace() && city.Length > 50)
                ValidationResult.Add("City", ValidationMessages.Invalid_City_Exceed);
        }

        private void ValidateZipCode(string zipCode)
        {
            if (!zipCode.IsNullOrWhiteSpace() && zipCode.Length > 15)
                ValidationResult.Add("Cep", ValidationMessages.Invalid_ZipCode_Exceed);
        }
    }
}
