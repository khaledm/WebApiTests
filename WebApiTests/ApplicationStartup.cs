using System;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Xml.Serialization;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FluentValidation.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using WebApiTests.Models;
using WebApiTests.Windsor;

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
        // public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        private static IWindsorContainer _container;
        /// <summary>
        /// Configuration
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            SwaggerConfig.Register(config);
            WebApiConfig.Register(config);

            var xml = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
            xml.SetSerializer<PurchaseOrderType>(new XmlSerializer(typeof(PurchaseOrderType)));

            /*
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
            }; */
            /*
            app.UseOAuthBearerTokens(OAuthOptions); /* The UseOAuthBearerTokens extension method creates both the token server and the middleware to validate tokens for requests in the same application.*/
            ConfigureWindsor(config);
            var provider = new SimpleModelBinderProvider(
                typeof(PurchaseOrderType), new PurchaseOrderTypeModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, provider);
            app.UseWebApi(config);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public static void ConfigureWindsor(HttpConfiguration configuration)
        {
            _container = new WindsorContainer();
            _container.Install(new WebApiInstaller());
            
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel, true));
            var dependencyResolver = new WindsorDependencyResolver(_container);
            configuration.DependencyResolver = dependencyResolver;

            FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration,
                config => { config.ValidatorFactory = new WebApiValidatorFactory(GlobalConfiguration.Configuration); });
        }
    }
}
