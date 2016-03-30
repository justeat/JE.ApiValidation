using FluentValidation;
using JE.ApiValidation.FluentValidation.StructureMap;
using NUnit.Framework;
using StructureMap;

namespace JE.ApiValidation.Tests.RequestValidity.StructureMap
{
    public abstract class WhenValidatorsAreLoadedFromStructureMap
    {
        private IContainer _container;
        protected StructureMapValidatorFactory ValidatorFactory { get; set; }

        [SetUp]
        public void Given()
        {
            var container = new Container(register =>
            {
                register.Scan(s =>
                {
                    s.AssemblyContainingType<RulesForRequest>();
                    s.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));
                });
            });

            ValidatorFactory = new StructureMapValidatorFactory(container);
        }
    }
}
