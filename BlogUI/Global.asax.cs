using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace BlogUI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    var roles = authTicket.UserData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                }
            }
        }
    }


    //public class HybridActionSelector : ApiControllerActionSelector
    //{
    //    private readonly IDictionary<ReflectedHttpActionDescriptor, string[]> _actionParams =
    //        new Dictionary<ReflectedHttpActionDescriptor, string[]>();

    //    public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
    //    {
    //        object actionName, subactionName;
    //        var hasActionName = controllerContext.RouteData.Values.TryGetValue("action", out actionName);
    //        var hasSubActionName = controllerContext.RouteData.Values.TryGetValue("subaction", out subactionName);

    //        var method = controllerContext.Request.Method;
    //        var allMethods =
    //            controllerContext.ControllerDescriptor.ControllerType.GetMethods(
    //                BindingFlags.Instance | BindingFlags.Public);
    //        var validMethods = Array.FindAll(allMethods, IsValidActionMethod);

    //        var actionDescriptors = new HashSet<ReflectedHttpActionDescriptor>();

    //        foreach (var actionDescriptor in validMethods.Select(
    //            m => new ReflectedHttpActionDescriptor(controllerContext.ControllerDescriptor, m)))
    //        {
    //            if (!actionDescriptors.Contains(actionDescriptor))
    //                actionDescriptors.Add(actionDescriptor);

    //            if (!_actionParams.ContainsKey(actionDescriptor))
    //                _actionParams.Add(
    //                    actionDescriptor,
    //                    actionDescriptor.ActionBinding.ParameterBindings
    //                        .Where(b => !b.Descriptor.IsOptional &&
    //                                    b.Descriptor.ParameterType.UnderlyingSystemType.IsPrimitive)
    //                        .Select(b => b.Descriptor.Prefix ?? b.Descriptor.ParameterName).ToArray());
    //        }

    //        IEnumerable<ReflectedHttpActionDescriptor> actionsFoundSoFar;

    //        if (hasSubActionName)
    //            actionsFoundSoFar =
    //                actionDescriptors.Where(
    //                    i => i.ActionName.ToLowerInvariant() == subactionName.ToString().ToLowerInvariant() &&
    //                         i.SupportedHttpMethods.Contains(method)).ToArray();
    //        else if (hasActionName)
    //            actionsFoundSoFar =
    //                actionDescriptors.Where(
    //                    i =>
    //                        i.ActionName.ToLowerInvariant() == actionName.ToString().ToLowerInvariant() &&
    //                        i.SupportedHttpMethods.Contains(method)).ToArray();
    //        else
    //            actionsFoundSoFar = actionDescriptors
    //                .Where(i => i.ActionName.ToLowerInvariant().Contains(method.ToString().ToLowerInvariant()) &&
    //                            i.SupportedHttpMethods.Contains(method)).ToArray();

    //        var actionsFound = FindActionUsingRouteAndQueryParameters(controllerContext, actionsFoundSoFar);

    //        if (actionsFound == null || !actionsFound.Any())
    //            throw new HttpResponseException(controllerContext.Request.CreateErrorResponse(HttpStatusCode.NotFound,
    //                "Cannot find a matching action."));
    //        if (actionsFound.Count() > 1)
    //            throw new HttpResponseException(
    //                controllerContext.Request.CreateErrorResponse(HttpStatusCode.Ambiguous, "Multiple matches found."));

    //        return actionsFound.FirstOrDefault();
    //    }

    //    private IEnumerable<ReflectedHttpActionDescriptor> FindActionUsingRouteAndQueryParameters(
    //        HttpControllerContext controllerContext, IEnumerable<ReflectedHttpActionDescriptor> actionsFound)
    //    {
    //        var routeParameterNames = new HashSet<string>(controllerContext.RouteData.Values.Keys,
    //            StringComparer.OrdinalIgnoreCase);

    //        if (routeParameterNames.Contains("controller")) routeParameterNames.Remove("controller");
    //        if (routeParameterNames.Contains("action")) routeParameterNames.Remove("action");
    //        if (routeParameterNames.Contains("subaction")) routeParameterNames.Remove("subaction");

    //        var hasQueryParameters = controllerContext.Request.RequestUri != null &&
    //                                 !string.IsNullOrEmpty(controllerContext.Request.RequestUri.Query);
    //        var hasRouteParameters = routeParameterNames.Count != 0;

    //        if (hasRouteParameters || hasQueryParameters)
    //        {
    //            var combinedParameterNames = new HashSet<string>(routeParameterNames, StringComparer.OrdinalIgnoreCase);
    //            if (hasQueryParameters)
    //                foreach (var queryNameValuePair in controllerContext.Request.GetQueryNameValuePairs())
    //                    combinedParameterNames.Add(queryNameValuePair.Key);

    //            actionsFound =
    //                actionsFound.Where(descriptor => _actionParams[descriptor].All(combinedParameterNames.Contains));

    //            if (actionsFound.Count() > 1)
    //                actionsFound = actionsFound
    //                    .GroupBy(descriptor => _actionParams[descriptor].Length)
    //                    .OrderByDescending(g => g.Key)
    //                    .First();
    //        }
    //        else
    //        {
    //            actionsFound = actionsFound.Where(descriptor => _actionParams[descriptor].Length == 0);
    //        }

    //        return actionsFound;
    //    }

    //    private static bool IsValidActionMethod(MethodInfo methodInfo)
    //    {
    //        if (methodInfo.IsSpecialName) return false;
    //        return !methodInfo.GetBaseDefinition().DeclaringType.IsAssignableFrom(typeof(ApiController));
    //    }
    //}
}