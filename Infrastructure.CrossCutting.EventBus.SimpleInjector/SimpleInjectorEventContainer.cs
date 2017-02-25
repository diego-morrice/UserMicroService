using Infrastructure.CrossCutting.EventBus.Interfaces;
using SimpleInjector;

namespace Infrastructure.CrossCutting.EventBus.SimpleInjector
{
    public class SimpleInjectorEventContainer : IEventDispatcher
    {
        private readonly Container _container;

        public SimpleInjectorEventContainer(Container container)
        {
            _container = container;
        }

        public void Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent
        {
            foreach (var handler in _container.GetAllInstances<IDomainEventHandler<TEvent>>())
            {
                handler.Handle(eventToDispatch);
            }
        }
    }
}