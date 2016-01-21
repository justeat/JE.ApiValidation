using JE.ApiValidation.Examples.OpenRasta;

namespace JE.ApiValidation.Examples.Tests.OpenRasta
{
    public class AgainstOpenRasta : WhenMakingRequests<Startup>
    {
        protected override string GetControllerName()
        {
            return "widgets";
        }
    }
}
