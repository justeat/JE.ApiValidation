using System.Net.Http;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using SpecsFor;

namespace JE.ApiValidation.Tests.RequestValidity.WebApi
{
    public abstract class WhenRequestsAreCheckedForValidity : SpecsFor<RejectInvalidRequestsAttribute>
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

        private static HttpRequestMessage GivenRequest()
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
}
