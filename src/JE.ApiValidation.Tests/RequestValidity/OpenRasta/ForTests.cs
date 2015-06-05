using System.Diagnostics;
using JE.ApiValidation.DTOs;
using JE.ApiValidation.OpenRasta;
using OpenRasta.DI;
using OpenRasta.OperationModel;
using OpenRasta.Web;

namespace JE.ApiValidation.Tests.RequestValidity.OpenRasta
{
    public class ForTests : FluentValidationOperationInterceptor
    {
        public ForTests(IDependencyResolver resolver, ICommunicationContext context)
            : base(resolver, context)
        {
        }

        protected override void LogBadRequest(IOperation operation, StandardErrorResponse body)
        {
            Logged = true;
            Debug.Assert(operation != null);
            Debug.Assert(body != null);
        }

        public bool Logged { get; private set; }
    }
}
