using NUnit.Framework;
using Shouldly;

namespace JE.ApiValidation.Tests.RequestValidity.OpenRasta
{
    public class WhenRequestIsValid : WhenValidatorsAreFound
    {
        protected override Request GivenRequestBody()
        {
            return new Request
                       {
                           PhoneNumber = "12345678901",
                           Email = "foo@bar.com",
                           Name = "foo bar"
                       };
        }

        [Test]
        public void ResultShouldBeTrue()
        {
            Result.ShouldBe(true);
        }
    }
}