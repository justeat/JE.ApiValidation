using System;
using FluentValidation;
using JE.ApiValidation.Examples.WebApi.Widgets;

namespace JE.ApiValidation.Examples.WebApi
{
    internal class ForExampleSitesValidatorFactoryButDontUseThisUseAContainer : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            return new RulesForWidgetRequests();
        }
    }
}