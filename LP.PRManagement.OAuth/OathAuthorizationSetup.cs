using Autofac;
using LP.PRManagement.OAuth.Default;
using LP.PRManagement.OAuth.Interfaces;
using LP.PRManagement.OAuth.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;

namespace LP.PRManagement.OAuth
{
    public static class OathAuthorizationSetup
    {
        private static bool _isInitialized;

        public static void Initialize(IAppBuilder app, IContainer container)
        {
            Initialize(app, new OAuthDataManager(), new SHA256Security(), container);
        }

        public static void Initialize(IAppBuilder app, IOAuthDataManager oauthDataManager, IOAuthSecurity oauthSecurity, IContainer container)
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
                OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
                {
                    AllowInsecureHttp = true,
                    TokenEndpointPath = new PathString("/token"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                    Provider = new SimpleAuthorizationServerProvider(container),
                    RefreshTokenProvider = new ApplicationRefreshTokenProvider(oauthDataManager, oauthSecurity)
                };

                // Token Generation
                app.UseOAuthAuthorizationServer(OAuthServerOptions);
                app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            }
        }
    }
}
