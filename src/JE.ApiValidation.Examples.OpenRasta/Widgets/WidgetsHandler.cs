using FluentValidation;
using JE.ApiValidation.Examples.Widgets;
using OpenRasta.Web;

namespace JE.ApiValidation.Examples.OpenRasta.Widgets
{
    public class WidgetsHandler
    {
        private readonly IValidator<InternalRepresentationOfWidget> _validator;

        public WidgetsHandler()
        {
            _validator = new BusinessRulesForWidgetProcessing();
        }

        [HttpOperation(HttpMethod.POST)]
        public OperationResult Post(Widget w)
        {
            var ir = new InternalRepresentationOfWidget(w);
            _validator.ValidateAndThrow(ir);
            return new OperationResult.OK(new object());
        }
    }
}
