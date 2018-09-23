using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Linq;
using WebApiTests.Models;
using WebApiTests.Services;

namespace WebApiTests.HttpMessageHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageValidationHandler : DelegatingHandler
    {
        private ISerialiseMessage<USAddress> _serialiser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _serialiser =
                request.GetDependencyScope().GetService(typeof(ISerialiseMessage<USAddress>)) as
                    ISerialiseMessage<USAddress>;

            if (request.Method == HttpMethod.Post && request.RequestUri.Segments.Contains("xml"))
            {
                var requestContent = request.Content.ReadAsStringAsync().Result;
                var requestObject = Deserialize<PurchaseOrderType>(new XmlMediaTypeFormatter(),  requestContent) as PurchaseOrderType;
                if (requestObject.confirmDate.Date != DateTime.Today.Date)
                {
                    var xml = new XElement("root", @"<validation_errors>confirmation date wrong</validation_errors>").ToString();
                    var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(xml, Encoding.UTF8, @"applicaton/xml")
                    };
                    var tsc = new TaskCompletionSource<HttpResponseMessage>();
                    tsc.SetResult(response);
                    return tsc.Task;
                }
            }

            return base.SendAsync(request, cancellationToken);
        }

        T Deserialize<T>(MediaTypeFormatter formatter, string str) where T : class
        {
            // Write the serialized string to a memory stream.
            Stream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            // Deserialize to an object of type T
            return formatter.ReadFromStreamAsync(typeof(T), stream, null, null).Result as T;
        }
    }
}