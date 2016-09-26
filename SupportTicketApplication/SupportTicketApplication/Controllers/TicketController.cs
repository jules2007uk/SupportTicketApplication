using AutoMapper;
using BusinessLogic;
using DataAccess.Repositories;
using EntityModels;
using Microsoft.AspNet.Identity.Owin;
using SupportTicketApplication.App_Start;
using SupportTicketApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SupportTicketApplication.Controllers
{
    /// <summary>
    /// Tickets controller, which requires users to be authorised (e.g. logged in) in order to use.
    /// </summary>
    [Authorize]
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
                // return view with no bound model as no model to attach
                return View();
            }
        }

        // GET: Tickets/Details?ID={ID}
        public ActionResult Details(int ID)
        {
            TicketHelper ticketHelper = new TicketHelper(c_repository);

            // retrieve ticket
            Ticket ticket = ticketHelper.RetrieveTicket(ID);

            if(ticket != null)
            {
                // convert the Ticket to the appropriate view model using Automapper
                TicketDetailViewModel ticketAsViewModel = Mapper.Map<TicketDetailViewModel>(ticket);
            
                return View(ticketAsViewModel);
            }
            else
            {
                // return http not found because Ticket could not be found
                return HttpNotFound();
            }
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

                // get the user name of the logged in user and assign as the Ticket owner
                ticketToAdd.Owner = User.Identity.Name;

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
            // check that ID was supplied
            if(ID != 0)
            {
                TicketHelper ticketHelper = new TicketHelper(c_repository);

                // retrieve ticket
                Ticket ticket = ticketHelper.RetrieveTicket(ID);

                // check that the user is either an Admin, or owns the ticket.
                // if they are neither then we're not continuing with the edit because they don't have permission
                if(User.IsInRole("Admin") || ticket.Owner == User.Identity.Name)
                {
                    if (ticket != null)
                    {
                        // convert the Ticket to the appropriate view model using Automapper
                        TicketDetailViewModel ticketAsViewModel = Mapper.Map<TicketDetailViewModel>(ticket);

                        return View(ticketAsViewModel);
                    }
                    else
                    {
                        // return not found error because ticket could not be found
                        return HttpNotFound();
                    }
                }
                else
                {
                    // user does not have permission to edit this ticket
                    return new HttpUnauthorizedResult("The user does not have permission to perform this action.");
                }                
            }
            {
                // no ticket ID supplied, return bad request error
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No ticket ID was supplied.");
            }      
        }

        // POST: Tickets/Edit[TicketDetailViewModel] 
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost([Bind(Include = "ID,Title,Description,Priority,DateCreated,Owner,Status")] TicketDetailViewModel incomingTicket)
        {            
            // ensure the model state is valid before updating the model
            if (ModelState.IsValid)
            {
                TicketHelper ticketHelper = new TicketHelper(c_repository);
                
                // fetch ticket to update, as we'll then update that ticket and pass it back to the business logic layer to save
                // although it might be more appropriate to hand off this work to the business logic layer?  
                // the best solution to this is to change the code so that the incomingTicket can be mapped to a new ticket instance and then overwrite the entity in the DB in one call
                // therefore there would be no need to load the existing ticket in the DB and modify its properties      
                Ticket ticketToUpdate = ticketHelper.RetrieveTicket(incomingTicket.ID);

                if(ticketToUpdate != null)
                {
                    // map the updatable properties supplied back via parameter to the loaded Ticket
                    // (this mapping could probably be done more elegantly, perhaps with AutoMapper)                        
                    ticketToUpdate.Description = incomingTicket.Description;
                    ticketToUpdate.Priority = incomingTicket.Priority;
                    ticketToUpdate.Status = incomingTicket.Status;
                    ticketToUpdate.Title = incomingTicket.Title;
                    
                    // try and update the ticket, did it go ok?
                    if (ticketHelper.DoUpdateTicket(ticketToUpdate))
                    {                        
                        // ticket updated succesfully - insert info message into TempData so that message can be shown on tickets list view
                        this.TempData["Success"] = "Ticket updated succesfully.";

                        // return the ticket that was updated to the view
                        return View(incomingTicket);
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
                    return new HttpNotFoundResult("The ticket could not be updated, because the ticket could not be found.");
                }
            }
            else
            {
                // model wasn't valid, reload same view
                return View(incomingTicket);
            }
        }

        // GET: Tickets/Delete/{ID}
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int ID)
        {
            // check the ticket ID supplied
            if (ID != 0)
            {
                TicketHelper ticketHelper = new TicketHelper(c_repository);

                // retrieve the Ticket matching the id supplied
                Ticket ticketToDelete = ticketHelper.RetrieveTicket(ID);

                // was the ticket found?
                if(ticketToDelete != null)
                {
                    // map the Ticket object to the appropriate view model for return
                    // return the view with the Ticket to delete as the model
                    return View(Mapper.Map<TicketDetailViewModel>(ticketToDelete));
                }
                else
                {
                    // no matching ticket found, return http not found error
                    return HttpNotFound();
                }                            
            }
            else
            {
                // no ticket ID supplied, return bad request error
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No ticket ID was supplied.");
            }            
        }

        // POST: Tickets/Delete/{ID}
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int ID)
        {
            // check the ticket ID supplied
            if (ID != 0)
            {
                TicketHelper ticketHelper = new TicketHelper(c_repository);

                // retrieve the ticket to delete
                Ticket ticketToDelete = ticketHelper.RetrieveTicket(ID);

                if(ticketToDelete != null)
                {
                    // try the Ticket delete
                    if (ticketHelper.DoRemoveTicket(ticketToDelete))
                    {
                        // ticket was deleted so add message to TempData
                        this.TempData["Success"] = "Ticket deleted succesfully.";

                        // redirect to tickets list view
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // ticket was not deleted so add message to TempData
                        this.TempData["Failure"] = "Ticket was not able to be deleted at this time. Please try again later.";

                        // redirect to tickets list view
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return new HttpNotFoundResult("The ticket could not be found, and therefore could not be deleted.");
                }
            }
            else
            {
                // no ticket ID supplied, return bad request error
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No ticket ID was supplied.");
            }
        }

        #endregion
    }
}