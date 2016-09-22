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

        IList<Ticket> RetrieveAllTickets();

        #endregion
    }
}
