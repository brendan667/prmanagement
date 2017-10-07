using LP.PRManagement.Api.App_Start;
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
            WebApiSetup webApiSetup = WebApiSetup.Initialize(app, IocApi.Instance.Resolve<IDependencyResolver>());
            SwaggerSetup.Initialize(webApiSetup.Configuration);
            //app.UseWebApi(config);
        }
    }
}