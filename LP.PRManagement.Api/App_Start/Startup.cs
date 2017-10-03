using LP.PRManagement.OAuth;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(LP.PRManagement.Api.Startup))]
namespace LP.PRManagement.Api
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration method
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            OathAuthorizationSetup.Initialize(app);
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            SwaggerSetup.Initialize(config);
            app.UseWebApi(config);
        }
    }
}