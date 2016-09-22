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
        /// <param name="context"></param>
        public TicketRepository()
        {
            c_context = new SupportTicketContext();
        }
        //public TicketRepository(SupportTicketContext context)
        //{
        //    c_context = context;
        //}        

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
                // TODO: Hook up to EF to replace hardcoded ticket creation

                

                // return the tickets in date created order
                return c_context.Tickets.OrderBy(x => x.DateCreated).ToList<Ticket>();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
