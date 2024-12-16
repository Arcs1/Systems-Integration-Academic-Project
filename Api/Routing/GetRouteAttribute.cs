using System.Net.Http;

namespace Api.Routing
{
    // Search in https://stackoverflow.com/questions/23094584/multiple-controller-types-with-same-route-prefix-asp-net-web-api/23097445?noredirect=1#23097445
    class GetRouteAttribute : MethodConstraintedRouteAttribute
    {
        public GetRouteAttribute(string template) : base(template ?? "", HttpMethod.Get) { }
    }

    class PostRouteAttribute : MethodConstraintedRouteAttribute
    {
        public PostRouteAttribute(string template) : base(template ?? "", HttpMethod.Post) { }
    }
}