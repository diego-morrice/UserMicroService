using System;
using System.Runtime.Serialization;

namespace Application.User.Messages
{
    [DataContract]
    public class SignInMessage
    {
        [DataMember(Name = "token")]
        public Guid Token { get; set; }
        [DataMember(Name = "tokenFacebook")]
        public string TokenFacebook { get; set; }
        [DataMember(Name = "tokenGoogle")]
        public string TokenGoogle { get; set; }
        [DataMember(Name = "guest")]
        public bool Guest { get; set; }
        [DataMember(Name = "id")]
        public Guid Id { get; set; }
        [DataMember(Name = "username")]
        public string Username { get; set; }
        [DataMember(Name = "email")]
        public string email { get; set; }
        [DataMember(Name = "fullName")]
        public string FullName { get; set; }
        [DataMember(Name = "birthDate")]
        public string BirthDate { get; set; }
        [DataMember(Name = "genre")]
        public string Genre { get; set; }
        [DataMember(Name = "country")]
        public string Country { get; set; }
        [DataMember(Name = "state")]
        public string State { get; set; }
        [DataMember(Name = "city")]
        public string City { get; set; }

        public DateTime BirthDateFormatted => BirthDate != null ? Convert.ToDateTime(BirthDate) : DateTime.MinValue;
    }
}

