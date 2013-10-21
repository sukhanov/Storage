using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;

namespace Storage
{
    using System;
    using App_Start;
    using Microsoft.Practices.Unity;
    using Models;

    public class MvcApplication : HttpApplication
    {
        public static NLog.Logger Log = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var container = new UnityContainer();
            UnityConfig.RegisterTypes(container);
            MapperConfig.RegisterMappers();
        }

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    var ex = Server.GetLastError();
        //    if (ex.InnerException != null)
        //    {
        //        ex = ex.InnerException;
        //    }

        //    Log.Error(ex.Message);

        //    Response.Clear();
        //    Server.ClearError();
        //}
    }
}