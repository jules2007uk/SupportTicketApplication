using EntityModels;
using EntityModels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace SupportTicketApplication.Models.ViewModels
{
    /// <summary>
    /// View model for the Ticket Index controller view. Used on the page which displays a list of Tickets and their headline information.
    /// </summary>
    public class TicketIndexViewModel
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
        /// Priority of the ticket.
        /// </summary>
        public TicketPriority Priority { get; set; }

        /// <summary>
        /// Date ticket was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        //TODO: Use correct variable type once Identity Framework has been implemented
        public string Owner { get; set; }

        //TODO: Use correct variable type once Identity Framework has been implemented
        public string Assignee { get; set; }

        /// <summary>
        /// Status of the ticket.
        /// </summary>
        public TicketStatus Status { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor for TicketsIndexViewModel.
        /// </summary>
        public TicketIndexViewModel()
        {
            this.ID = 0;
            this.Title = string.Empty;            
            this.Priority = new TicketPriority();
            this.DateCreated = new DateTime();
            this.Owner = string.Empty;            
            this.Assignee = string.Empty;
            this.Status = new TicketStatus();
        }

        #endregion
    }
}