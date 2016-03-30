namespace JE.ApiValidation.Examples.Tests.WebApi
{
    public class AgainstWebApiWithStructureMap : WhenMakingRequests<JE.ApiValidation.Examples.WebApi.FluentValidation.StructureMap.Startup>
    {
        protected override string GetControllerName()
        {
            return "morewidgets";
        }
    }
}
