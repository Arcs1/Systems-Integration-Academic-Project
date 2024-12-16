using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Api.Routing
{
    // Search in https://stackoverflow.com/questions/23094584/multiple-controller-types-with-same-route-prefix-asp-net-web-api/23097445?noredirect=1#23097445
    class MethodConstraint : IHttpRouteConstraint
    {
        public HttpMethod Method { get; private set; }

        public MethodConstraint(HttpMethod method)
        {
            Method = method;
        }

        public bool Match(HttpRequestMessage request,
                          IHttpRoute route,
                          string parameterName,
                          IDictionary<string, object> values,
                          HttpRouteDirection routeDirection)
        {
            return request.Method == Method;
        }
    }
}