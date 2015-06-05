using NUnit.Framework;
using Shouldly;

namespace JE.ApiValidation.Tests.RequestValidity.OpenRasta
{
    public class WhenThereAreNoValidatorsForParameters : WhenFluentValidationOperationInterceptorRuns
    {
        [Test]
        public void ResultShouldBeTrue()
        {
            Result.ShouldBe(true);
        }

        protected override void GivenResolverStubs()
        {

        }
    }
}