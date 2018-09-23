using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WebApiTests.Models;
using WebApiTests.Services;

namespace WebApiTests.Windsor
{
    public class WebApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISerialiseMessage<PurchaseOrderType>>().ImplementedBy<SerialiseXmlMessage>());
            container.Register(Component.For<ISerialiseMessage<USAddress>>().ImplementedBy<SerialiseXmlMessageUSAddress>());
        }
    }
}