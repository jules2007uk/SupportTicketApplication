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
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureAutofac();

            ConfigureAutoMapper();
        }

        /// <summary>
        /// Configures Autofac for dependency injection.
        /// </summary>
        /// <remarks>Injects the SupportTicketContext into the Tickets controller.</remarks>
        private void ConfigureAutofac()
        {
            ContainerBuilder builder = new ContainerBuilder();
            IContainer container = null;

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // register the TicketRepository as the implementation of ITicketRepository, with the context parameter specified
            builder.RegisterType<TicketRepository>().As<ITicketRepository>().WithParameter("context", new SupportTicketContext());

            // Set the dependency resolver to be Autofac.
            container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        /// <summary>
        /// Configures AutoMapper custom mappings.
        /// </summary>
        /// <remarks>Creates mappings for the Ticket entity to TicketIndexViewModel, TicketDetailViewModel, and TicketCreateViewModel respectively.</remarks>
        private void ConfigureAutoMapper()
        {
            // set up Automapper for mapping Ticket objects to view model equivalents
            Mapper.Initialize(config =>
            {
                config.CreateMap<Ticket, TicketIndexViewModel>().ReverseMap();
                config.CreateMap<Ticket, TicketDetailViewModel>().ReverseMap();
                config.CreateMap<Ticket, TicketCreateViewModel>().ReverseMap();
            });
        }
    }
}
