using FluentValidation;
using JE.ApiValidation.Examples.Widgets;
using OpenRasta.DI;
using OpenRasta.Owin;

namespace JE.ApiValidation.Examples.OpenRasta
{
    public class MyOpenRastaDependencyResolverAccessor : IDependencyResolverAccessor
    {
        private readonly IDependencyResolver _resolver;

        public MyOpenRastaDependencyResolverAccessor()
        {
            var resolver = new InternalDependencyResolver();
            resolver.AddDependency<OwinCommunicationContext>(DependencyLifetime.PerRequest);
            resolver.AddDependency<IValidator<Widget>, RulesForWidgetRequests>(DependencyLifetime.PerRequest);
            _resolver = resolver;
        }

        public IDependencyResolver Resolver
        {
            get
            {
                return _resolver;
            }
        }
    }
}
