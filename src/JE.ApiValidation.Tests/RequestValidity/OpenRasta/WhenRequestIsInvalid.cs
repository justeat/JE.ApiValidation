using JE.ApiValidation.DTOs;
using JE.ApiValidation.OpenRasta;
using NUnit.Framework;
using OpenRasta.Web;
using Shouldly;

namespace JE.ApiValidation.Tests.RequestValidity.OpenRasta
{
    public class WhenRequestIsInvalid : WhenValidatorsAreFound
    {
        protected override Request GivenRequestBody()
        {
            return new Request() {Email = "ksjhdf"};
        }

        [Test]
        public void ResultShouldBeTrue()
        {
            Result.ShouldBe(false);
        }

        [Test]
        public void OpResultShouldBe400()
        {
            OpResult.ShouldBeOfType<OperationResult.BadRequest>();
        }

        [Test]
        public void ResultBodyShouldBeStandard()
        {
            OpResult.ResponseResource.ShouldBeOfType<ResponseForInvalidRequest>();
            var body = OpResult.ResponseResource as ResponseForInvalidRequest;
            body.Code.ShouldBe((int)ErrorCodes.RequestWasInvalid);
            body.Errors.Keys.Count.ShouldBe(3);
        }
    }
}
