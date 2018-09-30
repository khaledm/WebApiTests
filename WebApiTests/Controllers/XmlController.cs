using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Xml.Linq;
using FluentValidation;
using FluentValidation.WebApi;
using Microsoft.Web.Http;
using WebApiTests.Filters;
using WebApiTests.Models;

namespace WebApiTests.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}/xml")]
    public class XmlController : ApiController
    {
        private readonly AbstractValidator<PurchaseOrderType> _validator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validator"></param>
        public XmlController(AbstractValidator<PurchaseOrderType> validator)
        {
            _validator = validator;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [Route("")]
        //[ValidationFilter]
        [HttpPost]
        public XElement Post(XElement request)
        {
            return null;
        }
    }
}
