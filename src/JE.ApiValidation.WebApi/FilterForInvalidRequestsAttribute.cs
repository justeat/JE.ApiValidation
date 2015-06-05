using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using JE.ApiValidation.DTOs;

namespace JE.ApiValidation.WebApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    // ReSharper disable UnusedMember.Global
    public class FilterForInvalidRequestsAttribute : ActionFilterAttribute
        // ReSharper restore UnusedMember.Global
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            if (!context.ModelState.IsValid)
            {
                RespondWithBadRequest(context);
            }
        }

        public override Task OnActionExecutingAsync(HttpActionContext context, CancellationToken cancellationToken)
        {
            if (!context.ModelState.IsValid)
            {
                RespondWithBadRequest(context);
            }
            return base.OnActionExecutingAsync(context, cancellationToken);
        }

        private void RespondWithBadRequest(HttpActionContext context)
        {
            var response = new ResponseForInvalidRequest(context.ModelState);
            LogBadRequest("Request pre-condition(s) failed", context, response);
            // we intentionally don't use context.Request.CreateErrorResponse, so that we can customise the response body. Otherwise it's too limited.
            context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, response);
        }

        protected virtual void LogBadRequest(string message, HttpActionContext context, StandardErrorResponse response)
        {

        }
    }
}
