using System;
using FluentValidation;
using JE.ApiValidation.Examples.Widgets;

namespace JE.ApiValidation.Examples
{
    public class ForExampleSitesValidatorFactoryButDontUseThisUseAContainer : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            return new RulesForWidgetRequests();
        }
    }
}
