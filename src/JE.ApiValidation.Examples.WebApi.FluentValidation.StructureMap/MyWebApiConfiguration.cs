using System.Web.Http;
using FluentValidation.WebApi;
using JE.ApiValidation.FluentValidation.StructureMap;
using JE.ApiValidation.WebApi;
using JE.ApiValidation.WebApi.FluentValidation;
using StructureMap;

namespace JE.ApiValidation.Examples.WebApi.FluentValidation.StructureMap
{
    public class MyWebApiConfiguration : HttpConfiguration
    {
        public MyWebApiConfiguration(IContainer container)
        {
            ConfigureRouting();
            ConfigureRequestValidation(container);
            ConfigureResponseProcessingErrorHandling();
        }

        private void ConfigureResponseProcessingErrorHandling()
        {
            Filters.Add(new ResponseProcessingErrorAttribute());
        }

        private void ConfigureRequestValidation(IContainer container)
        {
            FluentValidationModelValidatorProvider.Configure(this, provider => provider.ValidatorFactory = new StructureMapValidatorFactory(container));
            Filters.Add(new FilterForInvalidRequestsAttribute());
        }

        private void ConfigureRouting()
        {
            Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
        }
    }
}
