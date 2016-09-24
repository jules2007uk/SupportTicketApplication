using DataAccess.Contexts;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Responsible for data access for the Ticket model.
    /// </summary>
    public class TicketRepository : ITicketRepository
    {
        #region Class level variables

        private SupportTicketContext c_context;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="context">The context object used for querying of Tickets.</param>
        public TicketRepository(SupportTicketContext context)
        {
            c_context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves all support tickets from the repository.
        /// </summary>
        /// <returns>IList of all tickets found in the repository in order of date created.
        /// Returns null if no tickets are found.</returns>
        public IList<Ticket> RetrieveAllTickets()
        {
            IList<Ticket> tickets = null;

            try
            {
                // retrieve all tickets and their associated comments using eager loading
                tickets = c_context.Tickets.Include("Comments").OrderBy(x => x.DateCreated).ToList<Ticket>();
            }
            catch(Exception ex)
            {
                // TODO: Log the exception                
            }

            // finally return the tickets object
            return tickets;
        }

        /// <summary>
        /// Retrieves a single support ticket for the ticket ID supplied.
        /// </summary>
        /// <param name="ticketId">The ID of the ticket to retrieve.</param>
        /// <returns>Returns Ticket with the ticket ID supplied.
        /// Returns null if no ticket was found with the ID supplied.</returns>
        public Ticket RetrieveTicket(int ticketId)
        {
            Ticket ticket = null;

            // check the parameter passed
            if(ticketId > 0)
            {
                try
                {
                    // retrieve ticket for ID supplied
                    ticket = c_context.Tickets.FirstOrDefault<Ticket>(t => t.ID == ticketId);
                }
                catch (Exception ex)
                {
                    // TODO: Log the exception
                }
            }

            // return ticket object
            return ticket;
        }

        /// <summary>
        /// Updates the ticket supplied.
        /// </summary>
        /// <param name="ticket">The ticket to save.</param>
        /// <returns>Returns the updated ticket.
        /// Returns null if the ticket to update was not found.</returns>
        public Ticket UpdateTicket(Ticket ticket)
        {
            Ticket ticketToUpdate = null;

            // check the ticket passed via parameter
            if(ticket != null)
            {
                try
                {
                    // retrieve the existing ticket from the db
                    ticketToUpdate = c_context.Tickets.Find(ticket.ID);
                }
                catch (Exception ex)
                {
                    // TODO: Log the exception - there was a problem whilst trying to retrieve the ticket
                }

                if (ticketToUpdate != null)
                {
                    try
                    {
                        // map the updatable properties to the object to update
                        // (this mapping could probably be done more elegantly)
                        ticketToUpdate.Assignee = ticket.Assignee;
                        ticketToUpdate.Description = ticket.Description;
                        ticketToUpdate.Priority = ticket.Priority;
                        ticketToUpdate.Status = ticket.Status;
                        ticketToUpdate.Title = ticket.Title;

                        // set the ticket entity to state modified                
                        c_context.Entry(ticketToUpdate).State = System.Data.Entity.EntityState.Modified;

                        // save the change
                        c_context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        // TODO: Log the exception - there was a problem attempting to update the existing Ticket entity
                    }
                }
            }

            // hopefully return the updated ticket
            return ticketToUpdate;
        }

        /// <summary>
        /// Adds a new Ticket.
        /// </summary>
        /// <param name="ticket">The ticket to add.</param>
        /// <returns>Returns the added ticket.
        /// Returns null if there was a problem during the add process</returns>
        public Ticket AddTicket(Ticket ticket)
        {
            
            // check the ticket supplied via parameter
            if (ticket != null)
            {
                try
                {
                    // add the ticket to the context
                    c_context.Tickets.Add(ticket);

                    // save the change to the context
                    c_context.SaveChanges();

                    // return the added ticket
                    return ticket;
                }
                catch (Exception ex)
                {
                    // TODO: Log the exception - there was a problem attempting to create the ticket
                }
            }

            // return null because if the code has reached here then there must've been a problem within the try block above
            return null;
        }

        #endregion
    }
}
