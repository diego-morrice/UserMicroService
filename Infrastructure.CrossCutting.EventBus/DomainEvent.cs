using Infrastructure.CrossCutting.EventBus.Interfaces;

namespace Infrastructure.CrossCutting.EventBus
{
    public static class DomainEvent 
    {
        public static IEventDispatcher Dispatcher { get; set; }

        public static void Raise<T>(T domainEvent) where T : IDomainEvent
        {
            Dispatcher.Dispatch(domainEvent);
        }
    }
}