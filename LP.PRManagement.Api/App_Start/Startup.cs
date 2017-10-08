using LP.PRManagement.Api.App_Start;
using LP.PRManagement.OAuth;
using Microsoft.Owin;
using Owin;
using System.Web.Http.Dependencies;

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
            WebApiSetup webApiSetup = WebApiSetup.Initialize(app, IocApi.Instance.Resolve<IDependencyResolver>());
            OathAuthorizationSetup.Initialize(app, IocApi.Instance.Container);
            SwaggerSetup.Initialize(webApiSetup.Configuration);
            webApiSetup.Configuration.EnsureInitialized();
        }
    }
}