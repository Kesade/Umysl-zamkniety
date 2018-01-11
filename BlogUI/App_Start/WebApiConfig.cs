//using System.Linq;
//using System.Net.Http.Formatting;
//using System.Web.Http;
//using Newtonsoft.Json.Serialization;

//namespace WebApi
//{
//    public static class WebApiConfig
//    {
//        public static void Register(HttpConfiguration config)
//        {
//            // Web API configuration and services

//            // Web API routes
//            config.MapHttpAttributeRoutes();

//            config.Routes.MapHttpRoute(
//                "DefaultApi",
//                "api/{controller}/{id}/{action}/{actionid}/{subaction}/{subactionid}",
//                new
//                {
//                    id = RouteParameter.Optional,
//                    action = RouteParameter.Optional,
//                    actionid = RouteParameter.Optional,
//                    subaction = RouteParameter.Optional,
//                    subactionid = RouteParameter.Optional
//                }
//            );
//            //config.Routes.MapHttpRoute(
//            //    name: "DefaultApi",
//            //    routeTemplate: "api/{controller}/{id}",
//            //    defaults: new { id = RouteParameter.Optional }
//            //);
//        }
//    }
//}

using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace BlogUI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.MapHttpAttributeRoutes();
            //config.Routes.MapHttpRoute(
            //    "Diaries",
            //    "api/Diaries/{diaryid}",
            //    new {controller = "diaries", diaryid = RouteParameter.Optional}
            //);

            //config.Routes.MapHttpRoute(
            //    "DiaryEntries",
            //    "api/Diaries/{diaryid}/Entries/{id}",
            //    new {controller = "GenericApiController", id = RouteParameter.Optional}
            //);

            //config.Routes.MapHttpRoute(
            //    name: "DiarySummary",
            //    routeTemplate: "api/user/diaries/{diaryid}/summary",
            //    defaults: new { controller = "diarysummary" }
            //);

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}