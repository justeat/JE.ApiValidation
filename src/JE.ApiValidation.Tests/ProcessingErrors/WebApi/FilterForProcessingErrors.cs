using System.Diagnostics;
using System.Web.Http.Filters;
using JE.ApiValidation.DTOs;
using JE.ApiValidation.WebApi.FluentValidation;

namespace JE.ApiValidation.Tests.ProcessingErrors.WebApi
{
    // ReSharper disable ClassNeverInstantiated.Global
    public class FilterForProcessingErrors : ResponseProcessingErrorAttribute
        // ReSharper restore ClassNeverInstantiated.Global
    {
        protected override void LogBadRequest(string message, HttpActionExecutedContext context, StandardErrorResponse response)
        {
            Logged = true;
            Debug.Assert(message != null);
            Debug.Assert(context != null);
            Debug.Assert(response != null);
        }

        public bool Logged { get; private set; }
    }
}
