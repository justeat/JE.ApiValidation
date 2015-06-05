using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using OpenRasta.DI;
using OpenRasta.OperationModel;
using OpenRasta.OperationModel.Interceptors;
using OpenRasta.Web;

namespace JE.ApiValidation.OpenRasta
{
    public abstract class FluentValidationOperationInterceptor : OperationInterceptor
    {
        private readonly ICommunicationContext _context;
        private readonly IDependencyResolver _resolver;

        protected FluentValidationOperationInterceptor(IDependencyResolver resolver, ICommunicationContext context)
        {
            _resolver = resolver;
            _context = context;
        }

        public override bool BeforeExecute(IOperation operation)
        {
            var accumulatedErrors = new List<ValidationFailure>();
            foreach (var input in operation.Inputs)
            {
                if (input == null)
                {
                    continue;
                }
                var bindingResult = input.Binder.BuildObject();
                if (bindingResult == null || !bindingResult.Successful)
                {
                    continue;
                }
                var validatorType = typeof(IValidator<>).MakeGenericType(bindingResult.Instance.GetType());
                if (!_resolver.HasDependency(validatorType))
                {
                    continue;
                }
                var validator = _resolver.Resolve(validatorType) as IValidator;
                if (validator == null)
                {
                    continue;
                }
                var resultForParameter = validator.Validate(bindingResult.Instance);
                accumulatedErrors.AddRange(resultForParameter.Errors);
            }
            if (!accumulatedErrors.Any())
            {
                return true;
            }
            var body = new ResponseForInvalidRequest(accumulatedErrors);
            _context.OperationResult = new OperationResult.BadRequest { ResponseResource = body };
            LogBadRequest(operation, body);
            return false;
        }

        protected abstract void LogBadRequest(IOperation operation, ResponseForInvalidRequest body);
    }
}
