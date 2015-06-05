using JE.ApiValidation.OpenRasta;
using OpenRasta.DI;
using OpenRasta.OperationModel;
using OpenRasta.Web;

namespace JE.ApiValidation.Tests.RequestValidity.OpenRasta
{
    public class ConcreteFvoi : FluentValidationOperationInterceptor
    {
        public ConcreteFvoi(IDependencyResolver resolver, ICommunicationContext context)
            : base(resolver, context)
        {
        }

        protected override void LogBadRequest(IOperation operation, ResponseForInvalidRequest body)
        {
            Logged = true;
        }

        public bool Logged { get; private set; }
    }
}