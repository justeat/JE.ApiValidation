using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Routing;
using Moq;

namespace JE.ApiValidation.Tests
{
    internal static class ContextUtil
    {
        public static HttpControllerContext CreateControllerContext(HttpConfiguration configuration = null,
                                                                    IHttpController instance = null, IHttpRouteData routeData = null, HttpRequestMessage request = null)
        {
            var config = configuration ?? new HttpConfiguration();
            var route = routeData ?? new HttpRouteData(new HttpRoute());
            var req = request ?? new HttpRequestMessage();
            req.SetConfiguration(config);
            req.SetRouteData(route);

            var context = new HttpControllerContext(config, route, req);
            if (instance != null)
            {
                context.Controller = instance;
            }
            context.ControllerDescriptor = CreateControllerDescriptor(config);

            return context;
        }

        public static HttpActionContext CreateActionContext(HttpControllerContext controllerContext = null,
                                                            HttpActionDescriptor actionDescriptor = null)
        {
            var context = controllerContext ?? CreateControllerContext();
            var descriptor = actionDescriptor ?? CreateActionDescriptor();
            descriptor.ControllerDescriptor = context.ControllerDescriptor;
            return new HttpActionContext(context, descriptor);
        }

        public static HttpActionExecutedContext CreateActionExecutedConected(Exception exception, HttpActionContext context = null)
        {
            if (context == null)
            {
                context = CreateActionContext();
            }
            return new HttpActionExecutedContext(context, exception);
        }

        public static HttpActionContext GetHttpActionContext(HttpRequestMessage request)
        {
            var actionContext = CreateActionContext();
            actionContext.ControllerContext.Request = request;
            return actionContext;
        }

        public static HttpActionExecutedContext GetActionExecutedContext(HttpRequestMessage request,
                                                                         HttpResponseMessage response)
        {
            var actionContext = CreateActionContext();
            actionContext.ControllerContext.Request = request;
            var actionExecutedContext = new HttpActionExecutedContext(actionContext, null)
                                            {
                                                Response = response
                                            };
            return actionExecutedContext;
        }

        public static HttpControllerDescriptor CreateControllerDescriptor(HttpConfiguration config = null)
        {
            if (config == null)
            {
                config = new HttpConfiguration();
            }
            return new HttpControllerDescriptor { Configuration = config, ControllerName = "FooController" };
        }

        public static HttpActionDescriptor CreateActionDescriptor()
        {
            var mock = new Mock<HttpActionDescriptor> { CallBase = true };
            mock.SetupGet(d => d.ActionName).Returns("Bar");
            return mock.Object;
        }
    }
}