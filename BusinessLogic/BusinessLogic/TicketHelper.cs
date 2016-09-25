using DataAccess.Contexts;
using DataAccess.Repositories;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    /// <summary>
    /// Helper class for business logic around the Ticket entities.
    /// </summary>
    public class TicketHelper
    {
        #region Class level variables

        /// <summary>
        /// The implementation of ITicketRepository.
        /// </summary>
        private ITicketRepository c_repository;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="repository">The repository for the tickets.</param>
        public TicketHelper(ITicketRepository repository)
        {
            c_repository = repository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves all tickets from the repository supplied in the constructor.
        /// </summary>
        /// <returns>IList of all tickets found in the repository in order of date created.
        /// Returns null if no tickets are found.</returns>
        public IList<Ticket> RetrieveAllTickets()
        {
            try
            {
                // fetch all tickets from the repository supplied in the constructor method and return them
                return c_repository.RetrieveAllTickets();
            }
            catch(ArgumentNullException ex)
            {
                throw;
            }            
        }

        /// <summary>
        /// Retrieves specific ticket from the repository supplied in the constructor.
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns>Returns Ticket with the ticket ID supplied.
        /// Returns null if no ticket was found with the ID supplied.</returns>
        public Ticket RetrieveTicket(int ticketId)
        {
            try
            {
                // fetch ticket for ticket ID supplied
                return c_repository.RetrieveTicket(ticketId);
            }
            catch (ArgumentNullException ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Saves the ticket supplied to the repository supplied in the constructor.
        /// </summary>
        /// <param name="ticket">The new version of the ticket which needs updating.</param>
        /// <returns>Returns true if ticket updated OK, false if not.</returns>
        public bool DoUpdateTicket(Ticket ticket)
        {
            try
            {
                return c_repository.DoUpdateTicket(ticket);
            }            
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds the ticket supplied to the repository supplied in the constructor.
        /// </summary>
        /// <param name="ticket">The new ticket to add.</param>
        /// <returns>Returns the created ticket.
        /// Returns null if the ticket was not created.</returns>
        public Ticket AddTicket(Ticket ticket)
        {
            try
            {
                return c_repository.AddTicket(ticket);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes the ticket from the respotiory supplied in the constructor.
        /// </summary>
        /// <param name="ticketId">The ticket to remove.</param>
        /// <returns>Returns true for success, false for failure.</returns>
        public bool DoRemoveTicket(Ticket ticket)
        {
            try
            {
                return c_repository.DoRemoveTicket(ticket);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
