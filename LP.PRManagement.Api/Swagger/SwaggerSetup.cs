using LP.PRManagement.Api.Controllers;
using LP.PRManagement.Common;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;

namespace LP.PRManagement.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerSetup
    {
        private static bool _isInitialized;
        private static readonly object _locker = new object();
        private static SwaggerSetup _instance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        protected SwaggerSetup(HttpConfiguration configuration)
        {
            string version = GetVersion();

            configuration.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "LP.PRManagement API")
                    .Description(EmbededResourceHelper.ReadResource("LP.PRManagement.Api.Swagger.loginForm.html",
                        typeof(SwaggerSetup).Assembly)); ;
                c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\bin\LP.PRManagement.Api.XML");
                c.OperationFilter<AddAuthorizationResponseCodes>();
                c.RootUrl((x) => "http://localhost:51256");
            })
                .EnableSwaggerUi(c =>
                {
                    Assembly resourceAssembly = typeof(SwaggerSetup).Assembly;
                    c.InjectJavaScript(resourceAssembly, "LP.PRManagement.Api.Swagger.onComplete.js");
                });

        }

        #region Private Methods

        private static string GetVersion()
        {
            return typeof(UserController).Assembly.GetName().Version.ToString().Split('.').Take(3).StringJoin(".");
        }

        #endregion

        #region Initialize

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public static void Initialize(HttpConfiguration configuration)
        {
            if (_isInitialized) return;
            lock (_locker)
            {
                if (!_isInitialized)
                {
                    _instance = new SwaggerSetup(configuration);
                    _isInitialized = true;
                }
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public class AddAuthorizationResponseCodes : IOperationFilter
        {
            #region IOperationFilter Members

            public void Apply(Operation operation, SchemaRegistry dataTypeRegistry, ApiDescription apiDescription)
            {
                if (apiDescription.ActionDescriptor.GetFilters().OfType<AuthorizeAttribute>().Any())
                {
                    operation.responses.Add(ToIntDisplayValue(HttpStatusCode.Unauthorized), new Response
                    {
                        description = "Authentication required"
                    });

                }
            }

            private static string ToIntDisplayValue(HttpStatusCode httpStatusCode)
            {
                return ((int)httpStatusCode).ToString(CultureInfo.InvariantCulture);
            }

            #endregion   
        }
    }
}