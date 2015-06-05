using System.Web.Http.Controllers;
using JE.ApiValidation.DTOs;
using JE.ApiValidation.WebApi;

namespace JE.ApiValidation.Tests.RequestValidity.WebApi
{
    // ReSharper disable ClassNeverInstantiated.Global
    public class RejectInvalidRequestsAttribute : FilterForInvalidRequestsAttribute
        // ReSharper restore ClassNeverInstantiated.Global
    {
        public bool Logged { get; private set; }

        protected override void LogBadRequest(string message, HttpActionContext context, StandardErrorResponse response)
        {
            Logged = true;
        }
    }
}
