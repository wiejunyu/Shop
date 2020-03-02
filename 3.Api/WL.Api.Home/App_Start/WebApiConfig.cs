using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WL.API.Filters;

namespace WL.Api.Home
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            //首字母转为小写
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Filters.Add(new LoginApiAttribute());

            //跨域配置
            var allowedMethed = System.Configuration.ConfigurationManager.AppSettings["cors:allowedMethed"];
            var allowedOrigin = System.Configuration.ConfigurationManager.AppSettings["cors:allowedOrigin"];
            var allowedHeaders = System.Configuration.ConfigurationManager.AppSettings["cors:allowedHeaders"];
            var geduCors = new EnableCorsAttribute(allowedOrigin, allowedHeaders, allowedMethed)
            {
                SupportsCredentials = true
            };
            config.EnableCors(geduCors);

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
