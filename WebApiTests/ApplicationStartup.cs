using System;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(WebApiTests.ApplicationStartup))]
namespace WebApiTests
{
    /// <summary>
    /// Application Startup
    /// </summary>
    public class ApplicationStartup
    {
        /// <summary>
        ///
        /// </summary>
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        /// <summary>
        /// Configuration
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            SwaggerConfig.Register(config);
            WebApiConfig.Register(config);

            OAuthOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString(@"/token"),
                Provider = new OAuthAuthorizationServerProvider()
                {
                    OnValidateClientAuthentication = async (context) => context.Validated(),
                    OnGrantResourceOwnerCredentials = async (context) =>
                    {
                        context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new []{"*"});
                        if (context.UserName == "MK@MK.com" && context.Password == "P@ssw0rd")
                        {
                            ClaimsIdentity o = new ClaimsIdentity(context.Options.AuthenticationType);
                            o.AddClaim(new Claim("sub", context.UserName));
                            context.Validated(o);
                        }
                    }
                },
                AllowInsecureHttp = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(20)
            };

            app.UseOAuthBearerTokens(OAuthOptions); /* The UseOAuthBearerTokens extension method creates both the token server and the middleware to validate tokens for requests in the same application.*/

            app.UseWebApi(config);
        }
    }
}
