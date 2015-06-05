using FluentValidation;

namespace JE.ApiValidation.Tests.RequestValidity.OpenRasta
{
    public class RulesForRequest : AbstractValidator<Request>
    {
        public RulesForRequest()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().Length(10, 14);
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
