using System.Net;
using System.Web.Http.ModelBinding;
using NUnit.Framework;
using Shouldly;

namespace JE.ApiValidation.Tests.RequestValidity.WebApi
{
    public class AndTheRequestIsInvalid : WhenRequestsAreCheckedForValidity
    {
        protected override ModelStateDictionary GivenModelState()
        {
            var state = base.GivenModelState();
            state.AddModelError("foo", "bar");
            return state;
        }

        [Test]
        public void ShouldRespondWith400()
        {
            Context.Response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Test]
        public void ShouldHaveLogged()
        {
            SUT.Logged.ShouldBe(true);
        }
    }
}
