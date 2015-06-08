using System;
using System.Linq;
using FluentValidation;

namespace JE.ApiValidation.Examples.WebApi.Widgets
{
    public class BusinessRulesForWidgetProcessing : AbstractValidator<InternalRepresentationOfWidget>
    {
        private readonly string[] _possibleNames = { "foo", "bar", "baz" }; // imagine this comes from a database, please.

        public BusinessRulesForWidgetProcessing()
        {
            RuleFor(x => x.Name)
                .Must(x => _possibleNames.Any(y => y.Equals(x, StringComparison.InvariantCultureIgnoreCase)))
                .WithMessage("Must be one of the possible names.");
        }
    }
}
