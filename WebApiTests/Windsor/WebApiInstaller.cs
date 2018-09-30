using System.Reflection;
using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidation;
using WebApiTests.Controllers;
using WebApiTests.Models;
using WebApiTests.Services;

namespace WebApiTests.Windsor
{
    public class WebApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IHttpController>()
                .LifestylePerWebRequest());

            container.Register(Classes.FromThisAssembly()
                .BasedOn<IRepository>().LifestyleTransient());

            container.Register(Component.For<WebApiValidatorFactory>().ImplementedBy<WebApiValidatorFactory>().DependsOn(container));
            container.Register(Component.For<ISerialiseMessage<PurchaseOrderType>>().ImplementedBy<SerialiseXmlMessage>());
            container.Register(Component.For<ISerialiseMessage<USAddress>>().ImplementedBy<SerialiseXmlMessageUSAddress>());

            container.Register(Classes.FromThisAssembly().BasedOn(typeof(AbstractValidator<>)).WithServiceAllInterfaces());

            //container.Register(Component.For(typeof(AbstractValidator<PurchaseOrderType>))
            //    .UsingFactoryMethod((kernel, cmModel, ctx) =>
            //        kernel.Resolve<WebApiValidatorFactory>().CreateInstance(ctx.RequestedType)));
                //.UsingFactory((WebApiValidatorFactory fac) =>
                //fac.CreateInstance(typeof(AbstractValidator<>))))
                ;
        }
    }
}