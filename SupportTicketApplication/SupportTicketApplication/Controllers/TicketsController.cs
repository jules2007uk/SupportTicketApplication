using AutoMapper;
using BusinessLogic;
using DataAccess;
using DataAccess.Contexts;
using DataAccess.Repositories;
using EntityModels;
using EntityModels.Models;
using SupportTicketApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportTicketApplication.Controllers
{
    //[Err]
    public class TicketsController : Controller
    {
        #region Class level variables

        private ITicketRepository c_repository;

        #endregion

        #region Constructor

        public TicketsController(ITicketRepository repository)
        {
            c_repository = repository;
        }

        #endregion

        #region Actions

        // GET: Tickets
        public ActionResult Index()
        {
            // new instance of the Ticket Helper service
            TicketHelper ticketHelper = new TicketHelper(c_repository);
            
            // call the Ticket repository to retrieve list of Ticket
            IList<Ticket> tickets = ticketHelper.RetrieveAllTickets();

            // convert the Tickets to TicketsIndexViewModels using Automapper
            IList<TicketsIndexViewModel> ticketsAsViewModels = Mapper.Map<IList<TicketsIndexViewModel>>(tickets);

            return View(ticketsAsViewModels);
        }

        #endregion
    }

    public class Err : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");

            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(model)
            };
        }
    }
}