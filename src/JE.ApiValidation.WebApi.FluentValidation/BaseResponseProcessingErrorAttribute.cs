using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using FluentValidation;
using JE.ApiValidation.DTOs;

namespace JE.ApiValidation.WebApi.FluentValidation
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    // ReSharper disable UnusedMember.Global
    public abstract class BaseResponseProcessingErrorAttribute : ExceptionFilterAttribute
    // ReSharper restore UnusedMember.Global
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ValidationException)
            {
                RespondWithBadRequest(context);
            }
        }

        public override Task OnExceptionAsync(HttpActionExecutedContext context, CancellationToken cancellationToken)
        {
            if (context.Exception is ValidationException)
            {
                RespondWithBadRequest(context);
            }
            return base.OnExceptionAsync(context, cancellationToken);
        }

        private void RespondWithBadRequest(HttpActionExecutedContext context)
        {
            var validationException = context.Exception as ValidationException;
            var response = new ResponseForProcessingError(validationException);
            LogBadRequest("Request pre-condition(s) failed", context, response);
            // we intentionally don't use context.Request.CreateErrorResponse, so that we can customise the response body. Otherwise it's too limited.
            context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, response);
        }

        protected abstract void LogBadRequest(string message, HttpActionExecutedContext context, StandardErrorResponse response);
    }
}
