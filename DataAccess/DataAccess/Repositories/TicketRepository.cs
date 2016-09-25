using DataAccess.Contexts;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        /// <exception cref="ArgumentNullException"></exception>
        public IList<Ticket> RetrieveAllTickets()
        {
            IList<Ticket> tickets = null;

            try
            {
                // retrieve all tickets                
                tickets = c_context.Tickets.OrderBy(x => x.DateCreated).ToList<Ticket>();
            }
            catch(ArgumentNullException ex)
            {
                throw;              
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
        /// <exception cref="ArgumentNullException"></exception>
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
                catch (ArgumentNullException ex)
                {
                    throw;
                }
            }

            // return ticket object
            return ticket;
        }

        /// <summary>
        /// Updates the ticket supplied.
        /// </summary>
        /// <param name="ticket">The ticket to save.</param>
        /// <returns>Returns true if ticket updated OK, false if not.</returns>        
        /// <exception cref="Exception"></exception>
        public bool DoUpdateTicket(Ticket ticket)
        {
            bool wasUpdated = false;
                    
            // check the ticket passed via parameter
            if(ticket != null)
            {  
                try
                {
                    // set the ticket entity to state modified                
                    c_context.Entry(ticket).State = EntityState.Modified;

                    // save the change
                    c_context.SaveChanges();

                    wasUpdated = true;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return wasUpdated;
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
                    throw;
                }
            }

            // return null because if the code has reached here then there must've been a problem within the try block above
            return null;
        }

        /// <summary>
        /// Removes a Ticket.
        /// </summary>
        /// <param name="ticketId">The ID of the ticket to remove.</param>
        /// <returns>Returns true if ticket was removed, or false if it wasn't.</returns>
        public bool DoRemoveTicket(Ticket ticket)
        {
            // return flag to denote whether the remove action took place OK
            bool wasRemoved = false;

            // check parameter supplied - the ticket must not be null
            if (ticket != null)
            {
                try
                {                    
                    // try the remove            
                    c_context.Entry(ticket).State = EntityState.Deleted;

                    // save the context changes
                    c_context.SaveChanges();                    

                    // set flag for successful removal of ticket
                    wasRemoved = true;
                }
                catch(Exception ex)
                {
                    throw;
                }
            }

            // return success or failure
            return wasRemoved;
        }

        #endregion
    }
}
