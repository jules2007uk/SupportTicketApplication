using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Interface which defines the structure for data access for the Ticket model. 
    /// </summary>
    public interface ITicketRepository
    {
        #region Methods

        /// <summary>
        /// Retrieves all support tickets from the repository.
        /// </summary>
        /// <returns>IList of all tickets found in the repository.</returns>
        IList<Ticket> RetrieveAllTickets();

        /// <summary>
        /// Retrieves a single support ticket for the ticket ID supplied.
        /// </summary>
        /// <param name="ticketId">The ID of the ticket to retrieve.</param>
        /// <returns></returns>
        Ticket RetrieveTicket(int ticketId);

        /// <summary>
        /// Updates the ticket supplied.
        /// </summary>
        /// <param name="ticket">The ticket to save.</param>
        /// <returns>Returns the updated ticket.</returns>
        Ticket UpdateTicket(Ticket ticket);

        #endregion
    }
}
