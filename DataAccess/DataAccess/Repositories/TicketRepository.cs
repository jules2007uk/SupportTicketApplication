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
        /// <returns>IList of all tickets found in the repository in order of date created.</returns>
        public IList<Ticket> RetrieveAllTickets()
        {
            try
            {
                // return all tickets and their associated comments using eager loading
                return c_context.Tickets.Include("Comments").OrderBy(x => x.DateCreated).ToList<Ticket>();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a single support ticket for the ticket ID supplied.
        /// </summary>
        /// <param name="ticketId">The ID of the ticket to retrieve.</param>
        /// <returns></returns>
        public Ticket RetrieveTicket(int ticketId)
        {
            try
            {
                // return ticket for ID supplied
                return c_context.Tickets.FirstOrDefault<Ticket>(t => t.ID == ticketId);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the ticket supplied.
        /// </summary>
        /// <param name="ticket">The ticket to save.</param>
        /// <returns>Returns the updated ticket.</returns>
        public Ticket UpdateTicket(Ticket ticket)
        {
            try
            {
                // retrieve the existing ticket from the db
                Ticket ticketToUpdate = c_context.Tickets.Find(ticket.ID);

                // map the updatable properties to the object to update
                // (this could probably be done more elegantly)
                ticketToUpdate.Assignee = ticket.Assignee;                
                ticketToUpdate.Description = ticket.Description;
                ticketToUpdate.Priority = ticket.Priority;
                ticketToUpdate.Status = ticket.Status;
                ticketToUpdate.Title = ticket.Title;                

                // set the ticket entity to state modified                
                c_context.Entry(ticketToUpdate).State = System.Data.Entity.EntityState.Modified;                
                
                // save the change
                c_context.SaveChanges();

                // return the updated ticket
                return ticketToUpdate;              
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
