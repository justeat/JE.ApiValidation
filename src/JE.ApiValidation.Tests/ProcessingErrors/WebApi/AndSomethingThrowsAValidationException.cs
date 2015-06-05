using System.Net;
using FluentValidation;
using FluentValidation.Results;
using NUnit.Framework;
using Shouldly;

namespace JE.ApiValidation.Tests.ProcessingErrors.WebApi
{
    public class AndSomethingThrowsAValidationException : WhenActionProcessingMightThrowForRulesProcessing
    {
        protected override ValidationException GivenException()
        {
            return new ValidationException(new[] { new ValidationFailure("foo", "bar") });
        }

        [Test]
        public void ShouldRespondWith400()
        {
            Context.Response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Test]
        public void WarningShouldBeLogged()
        {
            SUT.Logged.ShouldBe(true);
        }
    }
}