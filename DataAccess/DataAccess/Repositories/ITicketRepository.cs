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
        /// <returns>IList of all tickets found in the repository.
        /// Returns null if there was an issue during the retrieval.</returns>
        IList<Ticket> RetrieveAllTickets();

        /// <summary>
        /// Retrieves a single support ticket for the ticket ID supplied.
        /// </summary>
        /// <param name="ticketId">The ID of the ticket to retrieve.</param>
        /// <returns>Returns the Ticket.
        /// Returns null if there was an issue during the retrieval.</returns>
        Ticket RetrieveTicket(int ticketId);

        /// <summary>
        /// Updates the ticket supplied.
        /// </summary>
        /// <param name="ticket">The ticket to save.</param>
        /// <returns>Returns the updated ticket.
        /// Returns null if there was an issue during the update.</returns>
        Ticket UpdateTicket(Ticket ticket);

        /// <summary>
        /// Adds a new Ticket.
        /// </summary>
        /// <param name="ticket">The ticket to add.</param>
        /// <returns>Returns the added ticket.
        /// Returns null if there was an issue during the add process.</returns>
        Ticket AddTicket(Ticket ticket);
        
        /// <summary>
        /// Removes a Ticket.
        /// </summary>
        /// <param name="ticketId">The ID of the ticket to remove.</param>
        /// <returns>Returns true if ticket was removed, or false if it wasn't.</returns>
        bool DoRemoveTicket(int ticketId);

        #endregion
    }
}
