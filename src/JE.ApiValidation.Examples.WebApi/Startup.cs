using JE.ApiValidation.Examples.WebApi;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace JE.ApiValidation.Examples.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(new MyWebApiConfiguration());
        }
    }
}
