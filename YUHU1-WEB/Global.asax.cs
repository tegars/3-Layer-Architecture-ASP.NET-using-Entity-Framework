using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YUHU1.BLL.Kelas.MapperProfile;

namespace YUHU1_WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //automapper
            Mapper.Initialize(map =>
            {
                map.AddProfile<KelasMapper>();
            });
                
        }
    }
}
