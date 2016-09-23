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
    /// View model for the Ticket Detail controller view. Used on the page which shows all information for a Ticket, including the comments.
    /// </summary>
    public class TicketDetailViewModel
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

        /// <summary>
        /// Status of the ticket.
        /// </summary>
        public TicketStatus Status { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor for TicketDetailViewModel.
        /// </summary>
        public TicketDetailViewModel()
        {
            this.ID = 0;
            this.Title = string.Empty;
            this.Description = string.Empty;
            this.Priority = new TicketPriority();
            this.DateCreated = new DateTime();
            this.Owner = string.Empty;
            this.Comments = new Collection<Comment>();
            this.Assignee = string.Empty;
            this.Status = new TicketStatus();
        }

        #endregion
    }
}