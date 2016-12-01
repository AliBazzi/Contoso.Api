using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace Contoso.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());
        }

        private class CustomDirectRouteProvider : DefaultDirectRouteProvider
        {
            protected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
            {
                return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(true);
            }

            protected override IReadOnlyList<RouteEntry> GetActionDirectRoutes(HttpActionDescriptor actionDescriptor, IReadOnlyList<IDirectRouteFactory> factories, IInlineConstraintResolver constraintResolver)
            {
                var _ = base.GetActionDirectRoutes(actionDescriptor, factories, constraintResolver);
                if (_.Count > 1)
                    return _.Take(1).ToList();
                return _;
            }
        }
    }
}
