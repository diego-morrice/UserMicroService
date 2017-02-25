using System.Net.Http.Formatting;
using System.Web.Http;
using Infrastructure.CrossCutting.InversionOfControl;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartup(typeof(WebApi.User.Startup))]
namespace WebApi.User
{
    public class Startup
    {
        private readonly HttpConfiguration _config = new HttpConfiguration();

        public void Configuration(IAppBuilder appBuilder)
        {
            ConfigureWebApi(_config);
            ConfigureDefaultHeaders(appBuilder);
            _config.DependencyResolver = IoC.GetConfigurationForWebApi(_config);
            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.UseWebApi(_config);
        }

        private static void ConfigureDefaultHeaders(IAppBuilder appBuilder)
        {
            appBuilder.Use((context, next) =>
            {
                context.Response.Headers.Remove("Server");
                context.Response.Headers.Remove("X-Powered-By");
                return next();
            });
        }

        public static void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var formatters = config.Formatters;
            formatters.Clear();
            formatters.Add(new JsonMediaTypeFormatter());

            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

           // config.Routes.MapHttpRoute(
           //    name: "NotFound",
           //    routeTemplate: "{*path}",
           //    defaults: new { controller = "Error", action = "NotFound" }
           //);
        }
    }
}
