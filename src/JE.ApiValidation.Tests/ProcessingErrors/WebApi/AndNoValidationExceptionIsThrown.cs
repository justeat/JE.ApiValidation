using NUnit.Framework;
using Shouldly;

namespace JE.ApiValidation.Tests.ProcessingErrors.WebApi
{
    public class AndNoValidationExceptionIsThrown : WhenActionProcessingMightThrowForRulesProcessing
    {
        [Test]
        public void ResponseShouldBeLeftUnSetByTheFilterBecauseTheRulesAllPassed()
        {
            Context.Response.ShouldBe(null);
        }
    }
}