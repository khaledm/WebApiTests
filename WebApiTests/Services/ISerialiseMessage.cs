using System.IO;
using System.Xml.Serialization;
using WebApiTests.Models;

namespace WebApiTests.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISerialiseMessage<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        T Serialise(string message);
    }

    /// <summary>
    /// 
    /// </summary>
    public class SerialiseXmlMessage : ISerialiseMessage<PurchaseOrderType>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public PurchaseOrderType Serialise(string message)
        {
            var serializer = new XmlSerializer(typeof(PurchaseOrderType));
            PurchaseOrderType result;

            using (TextReader reader = new StringReader(message))
            {
                result = (PurchaseOrderType) serializer.Deserialize(reader);
            }

            return result;
        }
    }

    public class SerialiseXmlMessageUSAddress : ISerialiseMessage<USAddress>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public USAddress Serialise(string message)
        {
            var serializer = new XmlSerializer(typeof(USAddress));
            USAddress result;

            using (TextReader reader = new StringReader(message))
            {
                result = (USAddress)serializer.Deserialize(reader);
            }

            return result;
        }
    }
}