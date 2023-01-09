using ManagementApi.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ManagementApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // 添加跨域设置
            // 跨域访问CORS
            var enableCors = ConfigurationManager.AppSettings[AppSettingKeys.EnableCors];
            if (enableCors != null && enableCors == "true")
            {
                var cors = new EnableCorsAttribute(ConfigurationManager.AppSettings[AppSettingKeys.Origin], "*", "*");
                config.EnableCors(cors);
            }
            else
            {
                config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            }


            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
