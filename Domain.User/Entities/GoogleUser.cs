
using System;
using Infrastructure.CrossCutting.Tools.Extensions;

namespace Domain.User.Entities
{
    public class GoogleUser 
    {
        public GoogleUser(){}

        public GoogleUser(string token)
        {
            Token = token;
        }

        public long Id { get; private set; }        
        public string Token { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastUpdatedDate { get; set; }
        public User User { get; private set; }
    }
}
