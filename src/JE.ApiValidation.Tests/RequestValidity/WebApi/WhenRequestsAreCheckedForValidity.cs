using System.Net.Http;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using JE.ApiValidation.DTOs;
using JE.ApiValidation.WebApi;
using SpecsFor;

namespace JE.ApiValidation.Tests.RequestValidity.WebApi
{
    public abstract class WhenRequestsAreCheckedForValidity : SpecsFor<ConcreteRejectInvalidRequestsAttribute>
    {
        protected HttpActionContext Context;

        protected override void Given()
        {
            Context = ContextUtil.CreateActionContext();
            Context.SetPrivateFieldValue("_modelState", GivenModelState());
            var cc = ContextUtil.CreateControllerContext(request: GivenRequest());
            Context.SetPrivateFieldValue("_controllerContext", cc);
            base.Given();
        }

        protected virtual HttpRequestMessage GivenRequest()
        {
            return new HttpRequestMessage(HttpMethod.Post, "internal/tests/request-validation")
                       {
                           Content =
                               new StringContent
                               ("foobar")
                       };
        }

        protected virtual ModelStateDictionary GivenModelState()
        {
            return new ModelStateDictionary();
        }

        protected override void When()
        {
            SUT.OnActionExecutingAsync(Context, new CancellationToken());
        }
    }

    public class ConcreteRejectInvalidRequestsAttribute : RejectInvalidRequestsAttribute
    {
        public bool Logged { get; private set; }

        protected override void LogBadRequest(string message, HttpActionContext context, StandardErrorResponse response)
        {
            Logged = true;
        }
    }
}
