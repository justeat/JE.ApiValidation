using FluentValidation;

namespace JE.ApiValidation.Examples.WebApi.Widgets
{
    public class RulesForWidgetRequests : AbstractValidator<Widget>
    {
        public RulesForWidgetRequests()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(3, 10);
        }
    }
}
