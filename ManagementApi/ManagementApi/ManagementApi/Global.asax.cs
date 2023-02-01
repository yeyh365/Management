using FluentScheduler;
using Management.Application.Services.Impl;
using ManagementApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ManagementApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
            //添加定时管理认为 使用FluentSchedulerService服务制定定时任务
            JobManager.Initialize(new FluentSchedulerService());  //开启定时器
                                                                  //JobManager.Stop();   //停止定时器
            FleckHelper.WebSocketInit();  //开启websocket
        }
    }
}
