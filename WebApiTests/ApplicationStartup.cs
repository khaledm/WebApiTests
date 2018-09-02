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
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

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
                        if (context.UserName == "MK@MK.com" && context.Password == "P@ssw0rd")
                        {
                            ClaimsIdentity o = new ClaimsIdentity(context.Options.AuthenticationType);
                            context.Validated(o);
                        }
                    }
                },
                AllowInsecureHttp = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(20)
            };

            app.UseOAuthBearerTokens(OAuthOptions);

            app.UseWebApi(config);
        }
    }
}
