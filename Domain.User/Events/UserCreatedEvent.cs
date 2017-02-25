using System;
using Infrastructure.CrossCutting.EventBus.Interfaces;

namespace Domain.User.Events
{
    internal class UserCreatedEvent : IDomainEvent
    {
        public UserCreatedEvent(Guid id, string username, string email)
        {
            Id = id;
            Username = username;
            Email = email;
        }

        public Guid Id { get; }
        public string Username { get; }
        public string Email { get; }
    }
}
