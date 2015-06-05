using NUnit.Framework;
using Shouldly;

namespace JE.ApiValidation.Tests.RequestValidity
{
    public class AndTheRequestIsValid : WhenRequestsAreCheckedForValidity
    {
        [Test]
        public void ResponseShouldBeLeftUnSetByTheFilterBecauseTheRulesAllPassed()
        {
            Context.Response.ShouldBe(null);
        }

        [Test]
        public void ShouldNotHaveLogged()
        {
            SUT.Logged.ShouldBe(false);
        }
    }
}
