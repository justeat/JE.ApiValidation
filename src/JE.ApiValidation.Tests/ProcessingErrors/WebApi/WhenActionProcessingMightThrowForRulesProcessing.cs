using System.Net.Http;
using System.Threading;
using System.Web.Http.Filters;
using FluentValidation;
using SpecsFor;

namespace JE.ApiValidation.Tests.ProcessingErrors.WebApi
{
    public class WhenActionProcessingMightThrowForRulesProcessing : SpecsFor<FilterForProcessingErrors>
    {
        protected HttpActionExecutedContext Context;

        protected override void Given()
        {
            var ac = ContextUtil.CreateActionContext();
            var cc = ContextUtil.CreateControllerContext(request: GivenRequest());
            ac.SetPrivateFieldValue("_controllerContext", cc);
            Context = ContextUtil.CreateActionExecutedConected(GivenException(), ac);
            base.Given();
        }

        protected virtual ValidationException GivenException()
        {
            return null;
        }

        private static HttpRequestMessage GivenRequest()
        {
            return new HttpRequestMessage(HttpMethod.Get, "internal/tests/validation-exception-handling");
        }

        protected override void When()
        {
            SUT.OnExceptionAsync(Context, new CancellationToken());
        }
    }
}
