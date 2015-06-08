using JE.ApiValidation.Examples.OpenRasta;
using Microsoft.Owin;
using OpenRasta.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace JE.ApiValidation.Examples.OpenRasta
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.UseOpenRasta(new MyOpenRastaConfiguration(), new MyOpenRastaDependencyResolverAccessor());
        }
    }
}
