using System;
using System.Runtime.Serialization;


namespace Application.User.Messages
{
    public class UserMessage
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }
        [DataMember(Name = "username")]
        public string Username { get; set; }
        [DataMember(Name = "email")]
        public string email { get;  set; }
        [DataMember(Name = "fullName")]
        public string FullName { get;  set; }
        [DataMember(Name = "birthDate")]
        public DateTime BirthDate { get;  set; }
        [DataMember(Name = "genre")]
        public string Genre { get;  set; }
        [DataMember(Name = "phoneNumber")]
        public string PhoneNumber { get;  set; }
        [DataMember(Name = "countryCode")]
        public string CountryCode { get;  set; }
        [DataMember(Name = "Country")]
        public string Country { get;  set; }
        [DataMember(Name = "state")]
        public string State { get;  set; }
        [DataMember(Name = "city")]
        public string City { get;  set; }
        [DataMember(Name = "addressLineOne")]
        public string AddressLineOne { get;  set; }
        [DataMember(Name = "addressLineTwo")]
        public string AddressLineTwo { get;  set; }
        [DataMember(Name = "number")]
        public string Number { get;  set; }
        [DataMember(Name = "zipCode")]
        public string ZipCode { get;  set; }

    }
}
