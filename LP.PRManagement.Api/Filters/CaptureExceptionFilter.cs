using log4net;
using LP.PRManagement.Common;
using LP.PRManagement.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http.Filters;

namespace LP.PRManagement.Api.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class CaptureExceptionFilter : ExceptionFilterAttribute
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            Exception exception = context.Exception.ToFirstExceptionOfException();
            if (IsSomeSortOfValidationError(exception))
            {
                RespondWithBadRequest(context, exception);
            }
            else
            {
                RespondWithInternalServerException(context, exception);
            }
        }

        #region Private Methods
        
        private static void RespondWithBadRequest(HttpActionExecutedContext context, Exception exception)
        {
            var errorMessage = new ErrorMessage(exception.Message);
            context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, errorMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static bool IsSomeSortOfValidationError(Exception exception)
        {
            return exception is ValidationException ||
                   exception is ArgumentException ||
                   exception is InvalidOperationException;
        }

        private void RespondWithInternalServerException(HttpActionExecutedContext context, Exception exception)
        {
            const HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            var errorMessage =
                new ErrorMessage("An internal system error has occurred. The developers have been notified.");
            Log.Error(exception.Message, exception);
#if DEBUG
            errorMessage.AdditionalDetail = exception.Message;
#endif
            context.Response = context.Request.CreateResponse(httpStatusCode, errorMessage);
        }

        #endregion
    }
}