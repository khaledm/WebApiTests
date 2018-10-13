using System.ComponentModel;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Filters;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FluentValidation;
using WebApiTests.Controllers;
using WebApiTests.Filters;
using WebApiTests.Models;
using WebApiTests.Services;
using Component = Castle.MicroKernel.Registration.Component;

namespace WebApiTests.Windsor
{
    public class WebApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Install(FromAssembly.This());

            container.Register(Classes.FromThisAssembly()
                .BasedOn<IHttpController>()
                .LifestylePerWebRequest());

            container.Register(Classes.FromThisAssembly()
                .BasedOn<IRepository>().LifestyleTransient());

            container.Register(Component.For<WebApiValidatorFactory>().ImplementedBy<WebApiValidatorFactory>().DependsOn(container));
            container.Register(Component.For<ISerialiseMessage<PurchaseOrderType>>().ImplementedBy<SerialiseXmlMessage>());
            container.Register(Component.For<ISerialiseMessage<USAddress>>().ImplementedBy<SerialiseXmlMessageUSAddress>());

            container.Register(Classes.FromThisAssembly().BasedOn(typeof(AbstractValidator<>)).WithServiceAllInterfaces());

            //container.Register(Component.For<ValidationFilter>().LifestyleTransient());
            container.Register(Classes.FromThisAssembly().BasedOn(typeof(ActionFilterAttribute)).LifestyleTransient());

            //container.Register(Component.For<IFilterProvider>().ImplementedBy<ConfigurableFilterProvider>());

            //container.Register(Component.For<IHttpControllerActivator>()
            //    .ImplementedBy<CompositionRoot>()
            //    .LifestyleTransient());

            //container.Register(Component.For(typeof(AbstractValidator<PurchaseOrderType>))
            //    .UsingFactoryMethod((kernel, cmModel, ctx) =>
            //        kernel.Resolve<WebApiValidatorFactory>().CreateInstance(ctx.RequestedType)));
            //.UsingFactory((WebApiValidatorFactory fac) =>
            //fac.CreateInstance(typeof(AbstractValidator<>))))
            ;
        }
    }
}