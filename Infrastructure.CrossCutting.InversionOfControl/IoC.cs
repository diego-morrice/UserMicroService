using System;
using System.Linq;
using System.Web.Http;
using Domain.User.Interfaces.Repositories;
using Domain.User.Interfaces.Services;
using Domain.User.Services;
using Infrastructure.CrossCutting.EventBus;
using Infrastructure.CrossCutting.EventBus.Interfaces;
using Infrastructure.CrossCutting.EventBus.SimpleInjector;
using Infrastructure.Data.User;
using Infrastructure.Data.User.Context;
using Infrastructure.Data.User.Interfaces;
using Infrastructure.Data.User.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using Application.User.Interfaces;
using Application.User;
using SimpleInjector.Extensions.LifetimeScoping;

namespace Infrastructure.CrossCutting.InversionOfControl
{
    public class IoC
    {

        private static Container _container;

        public static Container Container
        {
            get
            {
                if ((_container == null)) _container = SetTestUnitContainer();
                return _container;
            }
        }

        public static T GetInstance<T>() where T : class
        {
            return Container.GetInstance<T>();
        }

        public static SimpleInjectorWebApiDependencyResolver GetConfigurationForWebApi(HttpConfiguration httpConfig)
        {
            var container = new Container();

            RegisterComponents(container, httpConfig);

            return new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void RegisterComponents(Container container, HttpConfiguration httpConfig)
        {
            //Configuration for WebApi
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            container.RegisterWebApiControllers(httpConfig);

            //Register event handlers
            DomainEvent.Dispatcher = new SimpleInjectorEventContainer(container);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.ToLowerInvariant().Contains("Domain.User"));
            container.RegisterCollection(typeof(IDomainEventHandler<>), assemblies);

            //Register base dependencies
            var userContextRegistration = Lifestyle.Scoped.CreateRegistration<UserContext>(container);

            container.AddRegistration(typeof(IDbContext), userContextRegistration);
            container.AddRegistration(typeof(IUserContext), userContextRegistration);
            container.Register(typeof(IUnitOfWork<IUserContext>), typeof(UnitOfWork<IUserContext>), Lifestyle.Scoped);

            //Register repositories
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);

            //Register domain services
            container.Register<IUserService, UserService>(Lifestyle.Scoped);

            //Register application services
            container.Register<IUserAppService, UserAppService>(Lifestyle.Scoped);

            container.Verify();
        }

        private static Container SetTestUnitContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new LifetimeScopeLifestyle();

            //Register base dependencies
            var userContextRegistration = Lifestyle.Scoped.CreateRegistration<UserContext>(container);

            container.AddRegistration(typeof(IDbContext), userContextRegistration);
            container.AddRegistration(typeof(IUserContext), userContextRegistration);
            container.Register(typeof(IUnitOfWork<IUserContext>), typeof(UnitOfWork<IUserContext>), Lifestyle.Scoped);

            //Register repositories
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);

            //Register domain services
            container.Register<IUserService, UserService>(Lifestyle.Scoped);

            //Register application services
            container.Register<IUserAppService, UserAppService>(Lifestyle.Scoped);

            container.Verify();

            return container;

        }
    }
}
