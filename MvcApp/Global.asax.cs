using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.ActiveRecord.Framework;//Castle.net映射
using Castle.ActiveRecord;
using com.fxm.MVCHibernate.Core;

namespace MvcApp
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private Container container;

        protected void Application_Start()
        {
            //初始化Castle.net的配置元素 
            IConfigurationSource source = System.Configuration.ConfigurationManager.GetSection("activerecord") as IConfigurationSource;
            //初始化Castle.net实体类集合
            ActiveRecordStarter.Initialize(typeof(com.fxm.MVCHibernate.Domain.SysUser).Assembly, source); 
            container = Container.Instance;


            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}