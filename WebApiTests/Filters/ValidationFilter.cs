using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using FluentValidation;
using WebApiTests.Models;
using WebApiTests.Services;

namespace WebApiTests.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class ValidationFilter : ActionFilterAttribute
    {
        /// <summary>
        ///
        /// </summary>
        public StudentRespository Repository { get; set; }

        public IValidator<PurchaseOrderType> Validator { get; set; }

        /// <inheritdoc />
        ///  <summary>
        ///  </summary>
        ///  <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //var validator =
            //    actionContext.Request.GetDependencyScope().GetService(typeof(IValidator<PurchaseOrderType>)) as
            //        IValidator<PurchaseOrderType>;

            var responseObj = new Builder().CreateNew<PurchaseOrderType>()
                .SetPropertyWith(p => p.confirmDate = DateTime.UtcNow)
                .SetPropertyWith(p=> p.billTo = new USAddress() { city = "City"})
                .SetPropertyWith(p=> p.orderDate = DateTime.UtcNow)
                .Build();

           var validationResult = Validator.Validate(responseObj);

            if (!validationResult.IsValid)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            //var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            //{
            //    Content = new StringContent(Serialize(new XmlMediaTypeFormatter(), responseObj), Encoding.UTF8, @"applicaton/xml")
            //};

            //actionContext.Response = response;
        }

        private string Serialize<T>(MediaTypeFormatter formatter, T value)
        {
            // Create a dummy HTTP Content.
            Stream stream = new MemoryStream();
            var content = new StreamContent(stream);
            //// Serialize the object.
            formatter.WriteToStreamAsync(typeof(T), value, stream, content, null).Wait();
            // Read the serialized string.
            stream.Position = 0;
            return content.ReadAsStringAsync().Result;
        }
    }
}