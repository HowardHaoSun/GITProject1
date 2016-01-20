using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Http.Routing.Constraints;
using System.Web.Routing;
namespace SportsStore
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            //声名路由的初始化限制
            DefaultInlineConstraintResolver constrainResolver = new DefaultInlineConstraintResolver();

            // Web API 路由
            //应用限制
            config.MapHttpAttributeRoutes(constrainResolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
