using System;

namespace Domain.User.Entities
{
    public class AutenticateToken
    {
        public AutenticateToken()
        {

        }

        public AutenticateToken(User user)
        {
            IdUser = user.Id;
            User = user;
            Active = true;
            LastUpdatedDate = DateTime.Now;
            Token = Guid.NewGuid().ToString().Replace("-", "");
        }

        public long Id { get; private set; }
        public Guid IdUser { get; private set; }
        public string Token { get; private set; }
        public bool Active { get; private set; }
        public DateTime CreatedDate { get; private set; }        
        public DateTime LastUpdatedDate { get; set; }
        public DateTime LastUsageDate { get; private set; }
        public User User { get; private set;}
    }
}
