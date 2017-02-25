namespace Infrastructure.CrossCutting.EventBus.Interfaces
{
    public interface IEventDispatcher
    {
        void Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent;
    }
}