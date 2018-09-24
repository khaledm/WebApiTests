using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Xml.Linq;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [Route("")]
        [ValidationFilter]
        [HttpPost]
        public XElement Post([ModelBinder]PurchaseOrderType request)
        {
            return null;
        }
    }
}
