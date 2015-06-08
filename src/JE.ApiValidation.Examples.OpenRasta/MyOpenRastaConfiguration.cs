using JE.ApiValidation.Examples.OpenRasta.Widgets;
using JE.ApiValidation.OpenRasta;
using OpenRasta.Configuration;
using OpenRasta.DI;
using OpenRasta.OperationModel.Interceptors;

namespace JE.ApiValidation.Examples.OpenRasta
{
    public class MyOpenRastaConfiguration : IConfigurationSource
    {
        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
            {
                ResourceSpace.Has.ResourcesOfType<object>().AtUri("api/widgets").HandledBy<WidgetsHandler>().TranscodedBy<JsonCodec>();

                ResourceSpace.Uses.CustomDependency<IOperationInterceptor, FluentValidationOperationInterceptor>(DependencyLifetime.Transient);
                ResourceSpace.Uses.CustomDependency<IOperationInterceptor, FluentValidationResponseProcessingOperationInterceptor>(DependencyLifetime.Transient);
            }
        }
    }
}
