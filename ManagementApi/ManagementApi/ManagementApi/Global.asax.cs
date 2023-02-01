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
            //��Ӷ�ʱ������Ϊ ʹ��FluentSchedulerService�����ƶ���ʱ����
            JobManager.Initialize(new FluentSchedulerService());  //������ʱ��
                                                                  //JobManager.Stop();   //ֹͣ��ʱ��
            FleckHelper.WebSocketInit();  //����websocket
        }
    }
}
