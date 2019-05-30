using ExciApi.Models;
using ExciApi.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExciApi
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    using LogFunc = Action<object, IDictionary<string, object>>;

    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {

            //var options = new WrappingOptions()
            //{
            //    BeforeNext = (middleware, environment) =>
            //        Debug.WriteLine("Calling into: " + middleware),
            //    AfterNext = (middleware, environment) =>
            //        Debug.WriteLine("Coming back from: " + middleware),
            //};

            //app = new WrappingAppBuilder<WrappingLogger>(app, options);


            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }



    public class WrappingAppBuilder<TWrapper> : IAppBuilder
    {
        private readonly IAppBuilder _inner;
        private readonly object _wrappingOptions;

        public WrappingAppBuilder(IAppBuilder inner, object wrappingOptions)
        {
            _inner = inner;
            _wrappingOptions = wrappingOptions;
        }

        public IAppBuilder Use(object middleware, params object[] args)
        {
            _inner.Use(typeof(TWrapper), _wrappingOptions, GetDescription(middleware));
            return _inner.Use(middleware, args);
        }

        private string GetDescription(object middleware)
        {
            var type = middleware as Type ?? middleware.GetType();
            return type.Name;
        }

        public object Build(Type returnType)
        {
            return _inner.Build(returnType);
        }

        public IAppBuilder New()
        {
            return _inner.New();
        }

        public IDictionary<string, object> Properties => _inner.Properties;
    }

    public class WrappingLogger
    {
        private readonly AppFunc _next;
        private readonly WrappingOptions _options;
        private readonly string _middlewareDescription;

        public WrappingLogger(
            AppFunc next,
            WrappingOptions options,
            string middlewareDescription)
        {
            _next = next;
            _options = options;
            _middlewareDescription = middlewareDescription;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            _options.BeforeNext(_middlewareDescription, environment);
            await _next(environment);
            _options.AfterNext(_middlewareDescription, environment);
        }
    }

    public class WrappingOptions
    {
        public LogFunc BeforeNext { get; set; }
        public LogFunc AfterNext { get; set; }
    }
}
