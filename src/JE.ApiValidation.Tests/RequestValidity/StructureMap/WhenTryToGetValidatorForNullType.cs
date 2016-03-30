using NUnit.Framework;
using Should;

namespace JE.ApiValidation.Tests.RequestValidity.StructureMap
{
    public class WhenTryToGetValidatorForNullType : WhenValidatorsAreLoadedFromStructureMap
    {
        [Test]
        public void GivenTypeIsNull()
        {
            var result = ValidatorFactory.CreateInstance(null);

            result.ShouldBeNull();
        }
    }
}
