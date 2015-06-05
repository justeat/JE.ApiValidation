using System.Diagnostics;
using JE.ApiValidation.DTOs;
using NUnit.Framework;
using OpenRasta.Web;
using Shouldly;

namespace JE.ApiValidation.Tests.RequestValidity.OpenRasta
{
    public class WhenRequestIsInvalid : WhenValidatorsAreFound
    {
        protected override Request GivenRequestBody()
        {
            return new Request {Email = "ksjhdf"};
        }

        [Test]
        public void ShouldHaveLogged()
        {
            SUT.Logged.ShouldBe(true);
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
            OpResult.ResponseResource.ShouldBeAssignableTo<StandardErrorResponse>();
            var body = OpResult.ResponseResource as StandardErrorResponse;
            Debug.Assert(body != null, "body != null");
            body.Code.ShouldBe((int)ErrorCodes.RequestWasInvalid);
            body.Errors.Keys.Count.ShouldBe(3);
        }
    }
}
