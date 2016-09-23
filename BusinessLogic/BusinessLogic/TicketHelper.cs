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
        /// <returns></returns>
        public IList<Ticket> RetrieveAllTickets()
        {
            // fetch all tickets from the repository supplied in the constructor method and return them
            return c_repository.RetrieveAllTickets();
        }

        /// <summary>
        /// Retrieves specific ticket from the repository supplied in the constructor.
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        public Ticket RetrieveTicket(int ticketId)
        {
            // fetch ticket for ticket ID supplied
            return c_repository.RetrieveTicket(ticketId);
        }

        /// <summary>
        /// Saves the ticket supplied to the repository supplied in the constructor.
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns>Returns the updated ticket.</returns>
        public Ticket UpdateTicket(Ticket ticket)
        {
            return c_repository.UpdateTicket(ticket);
        }

        #endregion
    }
}
