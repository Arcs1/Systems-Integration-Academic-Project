using System.Web.Http;

namespace Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/somiod/{controller:alpha}/{id:int}/{action:alpha}",
                defaults: new {
                    controller = RouteParameter.Optional,
                    id = RouteParameter.Optional,
                    action = RouteParameter.Optional
                }
            ); 
        }
    }
}
