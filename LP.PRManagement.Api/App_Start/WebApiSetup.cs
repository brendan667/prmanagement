using LP.PRManagement.Api.Filters;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace LP.PRManagement.Api.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiSetup
    {
        private static bool _isInitialized;
        private static readonly object Locker = new object();
        private static WebApiSetup _instance;
        private readonly HttpConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appBuilder"></param>
        /// <param name="dependencyResolver"></param>
        protected WebApiSetup(IAppBuilder appBuilder, IDependencyResolver dependencyResolver)
        {
            var configuration = new HttpConfiguration();

            SetupRoutes(configuration);
            SetupGlobalFilters(configuration);
            SetApiCamelCase(configuration);
            //CrossOrginSetup.Setup(configuration);
            appBuilder.UseWebApi(configuration);
            configuration.DependencyResolver = dependencyResolver;
            _configuration = configuration;

        }

        /// <summary>
        /// 
        /// </summary>
        public HttpConfiguration Configuration => _configuration;

        /// <summary>
        /// 
        /// </summary>
        public static WebApiSetup Instance
        {
            get
            {
                if (_instance == null) throw new Exception("Call Instance before using Intance.");
                return _instance;
            }
        }

        #region Private Methods

        private static void SetupRoutes(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();

            configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void SetupGlobalFilters(HttpConfiguration configuration)
        {
            configuration.Filters.Add(new CaptureExceptionFilter());
        }

        private static void SetApiCamelCase(HttpConfiguration configuration)
        {
            var jsonFormatter = configuration.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        #endregion

        #region Instance

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appBuilder"></param>
        /// <param name="dependencyResolver"></param>
        /// <returns></returns>
        public static WebApiSetup Initialize(IAppBuilder appBuilder, IDependencyResolver dependencyResolver)
        {
            if (_isInitialized) return _instance;
            lock (Locker)
            {
                if (!_isInitialized)
                {
                    _instance = new WebApiSetup(appBuilder, dependencyResolver);
                    _isInitialized = true;
                }
            }
            return _instance;
        }

        #endregion
    }
}