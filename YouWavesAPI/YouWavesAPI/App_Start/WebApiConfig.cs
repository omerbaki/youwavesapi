using ForecastAnalysisReport;
using Framework;
using IsramarWaveAnalyzer;
using LevYamWaveAnalyzer;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WaveAnalyzerCommon;

namespace YouWavesAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            
            container.AddNewExtension<FrameworkExtendedContainer>();
            container.AddNewExtension<WaveAnalyzerCommonExtendedContainer>();
            container.AddNewExtension<WindAlertWaveAnalyzerExtendedContainer>();
            container.AddNewExtension<IsramarWaveAnalyzerExtendedContainer>();
            container.AddNewExtension<ForecastAnalysisReportExtendedContainer>();

            config.DependencyResolver = new UnityResolver(container);            

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
