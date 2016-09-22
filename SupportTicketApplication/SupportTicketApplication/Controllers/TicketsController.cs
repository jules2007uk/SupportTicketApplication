using DataAccess;
using DataAccess.Repositories;
using EntityModels.Models;
using SupportTicketApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportTicketApplication.Controllers
{
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

            // call the Ticket repository to retrieve list of Ticket
            IList<Ticket> tickets = c_repository.RetrieveAllTickets();
            IList<TicketViewModel> ticketsAsViewModels = new List<TicketViewModel>();

            // TODO: map the list of Ticket to the list of TicketViewModel

            // TODO: return the list of TicketViewModel

            return View(ticketsAsViewModels);
        }

        #endregion
    }
}