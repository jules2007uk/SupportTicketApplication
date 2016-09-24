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

            if(tickets != null)
            {
                // convert the Tickets to the appropriate view models using Automapper
                IList<TicketIndexViewModel> ticketsAsViewModels = Mapper.Map<IList<TicketIndexViewModel>>(tickets);

                // return the tickets to the view
                return View(ticketsAsViewModels);
            }
            else
            {
                // TODO: deal with the fact that no tickets were returned
                return View();
            }
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

        // GET: Tickets/Create
        public ActionResult Create()
        {            
            return View();
        }

        // POST: Tickets/Create[TicketCreateViewModel]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description,Priority")] TicketCreateViewModel incomingTicket)
        {
            // check that the incoming model is in a valid state
            if (ModelState.IsValid)
            {
                // attempt conversion into Ticket
                Ticket ticketToAdd = Mapper.Map<Ticket>(incomingTicket);                
                TicketHelper ticketHelper = null;
                Ticket addedTicket = null;

                // populate the owner property of Ticket as this has a pre-determined value                
                // TODO: Assign owner as logged in user
                ticketToAdd.Owner = "Mr Hardcoded";

                // instantiate the ticket helper
                ticketHelper = new TicketHelper(c_repository);

                // call method to add new ticket and assign created ticket to variable
                addedTicket = ticketHelper.AddTicket(ticketToAdd);

                if(addedTicket != null)
                {
                    // ticket added succesfully - insert info message into TempData so that message can be shown on tickets list view
                    this.TempData["Success"] = "Ticket created succesfully.";

                    // return user to the tickets list view
                    return RedirectToAction("Index");
                }
                else
                {
                    // ticket was not created because Ticket object returned by helper method was empty
                    // issue an error to the model state to reflect this
                    ModelState.AddModelError("", "Unable to create the ticket at this time. Please try again.");

                    // return view
                    return View(incomingTicket);
                }                
            }
            else
            {
                // return back to the page
                return View(incomingTicket);
            }
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

        // POST: Tickets/Edit[TicketDetailViewModel] 
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost([Bind(Include= "ID,Title,Description,Priority,DateCreated,Owner,Comments,Assignee,Status")] TicketDetailViewModel incomingTicket)
        {
            TicketDetailViewModel updatedTicketAsViewModel = null;

            // ensure the model state is valid before updating the model
            if (ModelState.IsValid)
            {
                TicketHelper ticketHelper = new TicketHelper(c_repository);
                Ticket updatedTicket = null;

                // convert the view model version of the ticket to Ticket type
                // update the ticket and return the updated ticket
                updatedTicket = ticketHelper.UpdateTicket(Mapper.Map<Ticket>(incomingTicket));

                if(updatedTicket != null)
                {
                    // map Ticket to view model equivalent
                    updatedTicketAsViewModel = Mapper.Map<TicketDetailViewModel>(updatedTicket);

                    // ticket updated succesfully - insert info message into TempData so that message can be shown on tickets list view
                    this.TempData["Success"] = "Ticket updated succesfully.";

                    // return the ticket that was updated to the view
                    return View(updatedTicketAsViewModel);
                }
                else
                {
                    // add model error to model state as we were unable to update the ticket
                    // this will display a message in the ValidationSummary label within the Edit view
                    ModelState.AddModelError("", "Unable to update the ticket at this time. Please try again.");                    

                    // return view
                    return View(incomingTicket);
                }
            }
            else
            {
                // model wasn't valid, reload same view
                return View(incomingTicket);
            }

            
        }

        #endregion
    }
}