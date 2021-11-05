using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StackExchange.Profiling;
using StackExchange.Profiling.Mvc;

namespace MiniProfilerDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitProfilerSettings();
        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal) MiniProfiler.StartNew();
        }
        
        protected void Application_EndRequest() => MiniProfiler.Current?.Stop();

        private static void InitProfilerSettings()
        {
            MiniProfiler.Configure(new MiniProfilerOptions
                    {
                        RouteBasePath = "~/profiler",
                        PopupMaxTracesToShow = 10, // defaults to 15
                    }
                    .AddViewProfiling() // Add MVC view profiling
            );
        }
    }
}