using EntityModels.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SupportTicketApplication.Models.ViewModels
{
    /// <summary>
    /// View model for the Ticket Create controller view. Used on the page which allows user to create a ticket.
    /// </summary>
    public class TicketCreateViewModel
    {
        #region Properties

        /// <summary>
        /// The ticket information in brief.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; }

        /// <summary>
        /// Detailed description for the ticket.
        /// </summary>
        [Required]
        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string Description { get; set; }

        /// <summary>
        /// Priority of the ticket.
        /// </summary>
        [Required]
        public TicketPriority Priority { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor for TicketCreateViewModel.
        /// </summary>
        public TicketCreateViewModel()
        {
            this.Title = string.Empty;
            this.Description = string.Empty;
            this.Priority = new TicketPriority();                       
        }

        #endregion
    }
}