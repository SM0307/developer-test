using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using OrangeBricks.Web.Models;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.Web.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using log4net;
using OrangeBricks.Web.Shared;

namespace OrangeBricks.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new Container();

            // DB Context
            container.Register<IOrangeBricksContext, ApplicationDbContext>();
            
            //Initialize Mapper (for Auto-mapping). We can create our own mapper class for mapping modal data and viewmodels. 
            Mapper.Initialize(config =>
            {
                config.CreateMap<Property, PropertyViewModel>().ReverseMap();
            });

            // Auth
            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationDbContext()));
            container.Register<IAuthenticationManager>(() => HttpContext.Current.GetOwinContext().Authentication);
            
            // MVC
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            DependencyResolver.SetResolver(
                new SimpleInjectorDependencyResolver(container));
        }
    }
}
