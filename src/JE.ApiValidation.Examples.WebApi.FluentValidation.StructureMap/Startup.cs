using FluentValidation;
using Microsoft.Owin;
using Owin;
using JE.ApiValidation.Examples.WebApi.FluentValidation.StructureMap;
using JE.ApiValidation.Examples.Widgets;
using StructureMap;

[assembly: OwinStartup(typeof(Startup))]
namespace JE.ApiValidation.Examples.WebApi.FluentValidation.StructureMap
{
    public class Startup
    {
        private readonly IContainer _container;

        public Startup()
        {
            _container = new Container(register =>
            {
                register.Scan(s =>
                {
                    s.AssemblyContainingType<RulesForWidgetRequests>();
                    s.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));
                });
            });
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(new MyWebApiConfiguration(_container));
        }
    }
}