﻿using NUnit.Framework;
using Should;

namespace JE.ApiValidation.Tests.RequestValidity.StructureMap
{
    public class WhenNoValidatorExistsForType : WhenValidatorsAreLoadedFromStructureMap
    {
        [Test]
        public void ShouldReturnNull()
        {
            var result = ValidatorFactory.CreateInstance(typeof(UnValidatedClass));

            result.ShouldBeNull();
        }

        private class UnValidatedClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
