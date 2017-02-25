namespace Infrastructure.CrossCutting.EventBus.Interfaces
{
    public interface IDomainEventHandler<in T> where T : IDomainEvent
    {
        void Handle(T @event);
    }
}