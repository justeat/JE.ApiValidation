using System.Web.Http.Filters;
using JE.ApiValidation.DTOs;
using JE.ApiValidation.WebApi.FluentValidation;

namespace JE.ApiValidation.Tests.ProcessingErrors.WebApi
{
    public class FilterForProcessingErrors : BaseResponseProcessingErrorAttribute
    {
        protected override void LogBadRequest(string message, HttpActionExecutedContext context, StandardErrorResponse response)
        {
            Logged = true;
        }

        public bool Logged { get; private set; }
    }
}