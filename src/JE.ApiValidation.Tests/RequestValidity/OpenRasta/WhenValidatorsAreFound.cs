using System;
using FluentValidation;
using Moq;
using OpenRasta.DI;

namespace JE.ApiValidation.Tests.RequestValidity.OpenRasta
{
    public abstract class WhenValidatorsAreFound : WhenFluentValidationOperationInterceptorRuns
    {
        protected override void GivenResolverStubs()
        {
            var dr = GetMockFor<IDependencyResolver>();
            dr.Setup(x => x.HasDependency(It.Is<Type>(y => y.IsAssignableFrom(typeof(IValidator<Request>)))))
                .Returns(true);
            dr.Setup(x => x.Resolve(It.Is<Type>(y => y.IsAssignableFrom(typeof(IValidator<Request>)))))
                .Returns(new RulesForRequest());
        }
    }
}
