using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using FluentValidation;

namespace WebApiTests.Windsor
{
    internal sealed class WindsorDependencyScope : IDependencyScope
    {
        private readonly IWindsorContainer _container;
        private readonly IDisposable _scope;

        public WindsorDependencyScope(IWindsorContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _scope = container.BeginScope();
        }

        public object GetService(Type t)
        {
            return _container.Kernel.HasComponent(t) ? _container.Resolve(t) : null;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            return _container.ResolveAll(t).Cast<object>().ToArray();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }

    public class WebApiValidatorFactory : ValidatorFactoryBase
    {
        private readonly HttpConfiguration _configuration;

        public WebApiValidatorFactory(HttpConfiguration configuration)
        {
            _configuration = configuration;
            /*
             * or _container = container
             */
        }
        public override IValidator CreateInstance(Type validatorType)
        {
            /*
             * if (_container.Kernel.HasComponent(validatorType))
           return _container.Resolve(validatorType) as IValidator;
        return null;
             */
            return _configuration.DependencyResolver.GetService(validatorType) as IValidator;
        }
    }
}