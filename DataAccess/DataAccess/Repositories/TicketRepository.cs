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
        #region Constructor
        // TODO: Dependency injection in order to set TicketContext and ITicketRepository
        #endregion

        #region Methods

        public IList<Ticket> RetrieveAllTickets()
        {
            try
            {
                // TODO: Hook up to EF to replace hardcoded ticket creation

                List<Ticket> tickets = new List<Ticket>();
                Ticket t1 = new Ticket();
                t1.ID = 1;
                t1.Title = "MyTitle";
                t1.Description = "MyDescription";
                tickets.Add(t1);

                Ticket t2 = new Ticket();
                t2.ID = 2;
                t2.Title = "MyTitle2";
                t2.Description = "MyDescription2";
                tickets.Add(t2);

                // return the tickets
                return tickets;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
