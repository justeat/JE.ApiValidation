namespace JE.ApiValidation.Examples.Tests.WebApi
{
    public class AgainstWebApi : WhenMakingRequests<JE.ApiValidation.Examples.WebApi.Startup>
    {
        protected override string GetControllerName()
        {
            return "widgets";
        }
    }
}