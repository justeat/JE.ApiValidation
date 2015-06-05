using System.Collections.Generic;
using Moq;
using OpenRasta.Binding;
using OpenRasta.DI;
using OpenRasta.Hosting.InMemory;
using OpenRasta.OperationModel;
using OpenRasta.TypeSystem;
using OpenRasta.Web;

namespace JE.ApiValidation.Tests.RequestValidity.OpenRasta
{
    public abstract class WhenFluentValidationOperationInterceptorRuns : SpecsFor.SpecsFor<ConcreteFvoi>
    {
        protected bool Result;
        protected Request RequestBody;
        protected IOperation Operation;
        private ICommunicationContext _context;

        protected OperationResult OpResult;

        protected override void InitializeClassUnderTest()
        {
            base.InitializeClassUnderTest();
            _context = new InMemoryCommunicationContext();
            SUT = new ConcreteFvoi(GetMockFor<IDependencyResolver>().Object, _context);
        }

        protected override void Given()
        {
            RequestBody = GivenRequestBody();
            Operation = GivenOperation();
            GivenResolverStubs();
        }

        protected virtual Request GivenRequestBody()
        {
            return new Request();
        }

        protected abstract void GivenResolverStubs();

        protected override void When()
        {
            Result = SUT.BeforeExecute(Operation);
            OpResult = _context.OperationResult;
        }

        protected virtual IOperation GivenOperation()
        {
            var operation = GetMockFor<IOperation>();
            operation.Setup(x => x.Inputs).Returns(GivenOperationInputs());
            return operation.Object;
        }

        protected virtual IEnumerable<InputMember> GivenOperationInputs()
        {
            var binder = GetMockFor<IObjectBinder>();
            binder.Setup(x => x.BuildObject()).Returns(BindingResult.Success(RequestBody));
            yield return new InputMember(GetMockFor<IMember>().Object, binder.Object, true);
        }
    }
}
