using System;
using Castle.Windsor;
using FluentValidation;

namespace WebApiTests.Windsor
{
    public class WebApiValidatorFactory : ValidatorFactoryBase
    {
        private readonly IWindsorContainer _container;
        // private readonly HttpConfiguration _configuration;

        public WebApiValidatorFactory(IWindsorContainer container)
        {
            /*_configuration = configuration;*/

            _container = container;
            /*
             * or _container = container
             */
        }
        public override IValidator CreateInstance(Type validatorType)
        {

            if (_container.Kernel.HasComponent(validatorType))
                return _container.Resolve(validatorType) as IValidator;
            return null;

            //return _configuration.DependencyResolver.GetService(validatorType) as IValidator;
        }
    }
}