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

        #endregion
    }
}
