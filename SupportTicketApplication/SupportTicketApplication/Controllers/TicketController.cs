using AutoMapper;
using BusinessLogic;
using DataAccess.Repositories;
using EntityModels;
using SupportTicketApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SupportTicketApplication.Controllers
{
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
            // new instance of the Ticket Helper service
            TicketHelper ticketHelper = new TicketHelper(c_repository);
            
            // call the Ticket repository to retrieve list of Ticket
            IList<Ticket> tickets = ticketHelper.RetrieveAllTickets();

            // convert the Tickets to the appropriate view models using Automapper
            IList<TicketIndexViewModel> ticketsAsViewModels = Mapper.Map<IList<TicketIndexViewModel>>(tickets);

            return View(ticketsAsViewModels);
        }

        // GET: Tickets/Details?ID={ID}
        public ActionResult Details(int ID)
        {
            TicketHelper ticketHelper = new TicketHelper(c_repository);

            // retrieve ticket
            Ticket ticket = ticketHelper.RetrieveTicket(ID);

            // convert the Ticket to the appropriate view model using Automapper
            TicketDetailViewModel ticketAsViewModel = Mapper.Map<TicketDetailViewModel>(ticket);
            
            return View(ticketAsViewModel);
        }

        // GET: Tickets/Edit?ID={ID}
        public ActionResult Edit(int ID)
        {
            TicketHelper ticketHelper = new TicketHelper(c_repository);

            // retrieve ticket
            Ticket ticket = ticketHelper.RetrieveTicket(ID);

            // convert the Ticket to the appropriate view model using Automapper
            TicketDetailViewModel ticketAsViewModel = Mapper.Map<TicketDetailViewModel>(ticket);

            return View(ticketAsViewModel);
        }

        // POST: Tickets/Edit?ID={5}
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost([Bind(Include= "ID,Title,Description,Priority,DateCreated,Owner,Comments,Assignee,Status")] Ticket ticket)
        {
            TicketDetailViewModel updatedTicketAsViewModel = null;

            // ensure the model state is valid before updating the model
            if (ModelState.IsValid)
            {
                TicketHelper ticketHelper = new TicketHelper(c_repository);

                // update the ticket and return the updated ticket
                Ticket updatedTicket = ticketHelper.UpdateTicket(ticket);

                // map Ticket to view model equivalent
                updatedTicketAsViewModel = Mapper.Map<TicketDetailViewModel>(updatedTicket);
            }

            if(updatedTicketAsViewModel != null)
            {
                return View(updatedTicketAsViewModel);
            }
            else
            {
                // redirect to error view
            }
        }

        #endregion
    }
}