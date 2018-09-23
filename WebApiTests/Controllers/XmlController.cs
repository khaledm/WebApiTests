using System.Web.Http;
using System.Xml.Linq;
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
        public XElement Post([FromBody]PurchaseOrderType request)
        {
            return null;
        }
    }
}
