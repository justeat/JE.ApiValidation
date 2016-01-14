using NUnit.Framework;
using Should;

namespace JE.ApiValidation.Tests.RequestValidity.StructureMap
{
    public class WhenValidatorExistsForType : WhenValidatorsAreLoadedFromStructureMap
    {
        [Test]
        public void GivenRequestingValidatorForTypeThatIsRegistered()
        {
            var result = ValidatorFactory.CreateInstance(typeof (Request));

            result.ShouldNotBeNull();
            result.ShouldBeType<RulesForRequest>();
        }
    }
}
