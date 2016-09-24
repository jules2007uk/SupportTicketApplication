using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using DataAccess;
using DataAccess.Contexts;
using DataAccess.Repositories;
using EntityModels;
using SupportTicketApplication.App_Start;
using SupportTicketApplication.Controllers;
using SupportTicketApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SupportTicketApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //TODO: Clean up this class
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            #region Configure Autofac

            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // register the TicketRepository as the implementation of ITicketRepository, with the context parameter specified
            builder.RegisterType<TicketRepository>().As<ITicketRepository>().WithParameter("context", new SupportTicketContext());

            //// OPTIONAL: Register model binders that require DI.
            //builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            //builder.RegisterModelBinderProvider();

            //// OPTIONAL: Register web abstractions like HttpContextBase.
            //builder.RegisterModule<AutofacWebTypesModule>();

            //// OPTIONAL: Enable property injection in view pages.
            //builder.RegisterSource(new ViewRegistrationSource());

            //// OPTIONAL: Enable property injection into action filters.
            //builder.RegisterFilterProvider();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            #endregion

            #region Configure Automapper

            // set up the Automapper for mapping objects to other objects
            Mapper.Initialize(config =>
            {
               config.CreateMap<Ticket, TicketIndexViewModel>().ReverseMap();
               config.CreateMap<Ticket, TicketDetailViewModel>().ReverseMap();
               config.CreateMap<Ticket, TicketCreateViewModel>().ReverseMap();
            });

            #endregion

            #region Configure Identity Framework

            // TODO: Configure ASP.Net Identity Framework

            #endregion

        }
    }
}
