using System;
using System.Linq;
using FluentValidation;
using StructureMap;

namespace JE.ApiValidation.FluentValidation.StructureMap
{
    public class StructureMapValidatorFactory : ValidatorFactoryBase
    {
        private readonly IContainer _container;

        public StructureMapValidatorFactory(IContainer container)
        {
            _container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            if (validatorType == null) return null;

            if (validatorType.GenericTypeArguments != null && validatorType.GenericTypeArguments.Any())
            {
                validatorType = validatorType.GenericTypeArguments.First();
            }

            var abstractValidatorType = typeof(AbstractValidator<>);
            var validatorForType = abstractValidatorType.MakeGenericType(validatorType);

            var validator = _container.TryGetInstance(validatorForType);

            return validator as IValidator;
        }
    }
}