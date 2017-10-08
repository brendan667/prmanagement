using LP.PRManagement.Api.Controllers;
using LP.PRManagement.Common;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
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
                c.SingleApiVersion(version, "LP.PRManagement API");
                c.IncludeXmlComments(String.Format(@"{0}\bin\LP.PRManagement.Api.XML", AppDomain.CurrentDomain.BaseDirectory));
                c.OperationFilter<AddAuthorizationResponseCodes>();
                c.RootUrl((x) => "http://localhost:51256");
            })
                .EnableSwaggerUi(c =>
                {
                    c.DisableValidator();
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