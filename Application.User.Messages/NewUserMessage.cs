using System;
using System.Runtime.Serialization;

namespace Application.User.Messages
{
    [DataContract]
    public class NewUserMessage
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }
        [DataMember(Name = "username")]
        public string Username { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "password")]
        public string Password { get; set; }
        [DataMember(Name = "confirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
