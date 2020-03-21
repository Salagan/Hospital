using HospitalWebApi.Models;
using HospitalWebApi.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;

namespace HospitalWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static IMapper Config { get; set; }
        protected void Application_Start()
        {
            Database.SetInitializer(new HospitalDbInitializer());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var config = new AutoMapper.MapperConfiguration(c =>
            {
                c.AddProfile(new AutoMapperProfile());
            });
             Config = config.CreateMapper();

        }

    }
         
}

