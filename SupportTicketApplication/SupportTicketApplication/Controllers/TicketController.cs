using AutoMapper;
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
    public class TicketController : Controller
    {
        #region Class level variables

        private ITicketRepository c_repository;

        #endregion

        #region Constructor

        public TicketController(ITicketRepository repository)
        {
            c_repository = repository;
        }

        #endregion

        #region Actions

        // GET: Tickets
        public ActionResult Index()
        {
            // call the Ticket repository to retrieve list of Ticket
            IList<Ticket> tickets = c_repository.RetrieveAllTickets();

            // convert the Tickets to TicketViewModels using Automapper
            IList<TicketViewModel> ticketsAsViewModels = Mapper.Map<IList<TicketViewModel>>(tickets);

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