using System.Collections.Generic;
using OpenRasta.Binding;
using OpenRasta.DI;
using OpenRasta.Hosting.InMemory;
using OpenRasta.OperationModel;
using OpenRasta.TypeSystem;
using OpenRasta.Web;

namespace JE.ApiValidation.Tests.RequestValidity.OpenRasta
{
    public abstract class WhenFluentValidationOperationInterceptorRuns : SpecsFor.SpecsFor<ForTests>
    {
        protected bool Result;
        private Request _requestBody;
        private IOperation _operation;
        private ICommunicationContext _context;

        protected OperationResult OpResult;

        protected override void InitializeClassUnderTest()
        {
            base.InitializeClassUnderTest();
            _context = new InMemoryCommunicationContext();
            SUT = new ForTests(GetMockFor<IDependencyResolver>().Object, _context);
        }

        protected override void Given()
        {
            _requestBody = GivenRequestBody();
            _operation = GivenOperation();
            GivenResolverStubs();
        }

        protected virtual Request GivenRequestBody()
        {
            return new Request();
        }

        protected abstract void GivenResolverStubs();

        protected override void When()
        {
            Result = SUT.BeforeExecute(_operation);
            OpResult = _context.OperationResult;
        }

        private IOperation GivenOperation()
        {
            var operation = GetMockFor<IOperation>();
            operation.Setup(x => x.Inputs).Returns(GivenOperationInputs());
            return operation.Object;
        }

        private IEnumerable<InputMember> GivenOperationInputs()
        {
            var binder = GetMockFor<IObjectBinder>();
            binder.Setup(x => x.BuildObject()).Returns(BindingResult.Success(_requestBody));
            yield return new InputMember(GetMockFor<IMember>().Object, binder.Object, true);
        }
    }
}
