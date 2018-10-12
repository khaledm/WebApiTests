using WebApiTests.Filters;
using WebApiTests.HttpMessageHandlers;

namespace WebApiTests
{
    using System.Web.Http;
    using System.Web.Http.Routing;
    using Microsoft.Web.Http.Routing;

    /// <summary>
    /// Web Api Config
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                {
                    ["apiVersion"] = typeof( ApiVersionRouteConstraint )
                }
            };

            // Web API routes
            config.MapHttpAttributeRoutes(constraintResolver);
            config.AddApiVersioning();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );

            config.MessageHandlers.Add(new MessageValidationHandler());
        }
    }
}
