using DataAccess.Entities;
using EntityModels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Class which maps to the Ticket entity.
    /// </summary>
    public class Ticket
    {
        #region Properties

        /// <summary>
        /// The ticket unique identifer.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The ticket information in brief.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Detailed description for the ticket.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Priority of the ticket.
        /// </summary>
        public TicketPriority Priority { get; set; }

        /// <summary>
        /// Date ticket was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        //TODO: Use correct variable type once Identity Framework has been implemented
        public string Owner { get; set; }

        /// <summary>
        /// Comments made against the ticket.
        /// </summary>
        public ICollection<Comment> Comments { get; set; }

        //TODO: Use correct variable type once Identity Framework has been implemented
        public string Assignee { get; set; }

        #endregion
    }
}
