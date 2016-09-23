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

        #endregion
    }
}
