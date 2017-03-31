using Public.Holiday.WebAPI.Business_Logic;
using Public.Holiday.WebAPI.Data_Accessors;
using System;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Public.HolidayWebAPI
{
    public static class WebApiConfig
    {
   
        public static void Register(HttpConfiguration config)
        {
            PublicHolidayManager.DataAccessor = new FileAccessor();
            PublicHolidayManager.RegisterServiceProvider(new EnricoServiceProvider());

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{year}",
                defaults: new { year = DateTime.Today.Year }
            );

            //Extra Formatters
            config.Formatters.JsonFormatter.AddUriPathExtensionMapping("json", "application/json");
            config.Formatters.XmlFormatter.AddUriPathExtensionMapping("xml", "application/xml");
        }
    }
}
