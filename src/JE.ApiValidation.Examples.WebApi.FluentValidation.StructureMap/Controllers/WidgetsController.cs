using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using JE.ApiValidation.Examples.Widgets;

namespace JE.ApiValidation.Examples.WebApi.FluentValidation.StructureMap.Controllers
{
    public class WidgetsController : ApiController
    {
        private readonly IValidator<InternalRepresentationOfWidget> _businessRules;

        public WidgetsController()
        {
            _businessRules = new BusinessRulesForWidgetProcessing();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(Widget w)
        {
            var ir = new InternalRepresentationOfWidget(w);
            await _businessRules.ValidateAndThrowAsync(ir).ConfigureAwait(false);
            return Ok(w);
        }
    }
}
