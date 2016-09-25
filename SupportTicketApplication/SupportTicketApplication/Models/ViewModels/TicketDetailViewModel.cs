using EntityModels;
using EntityModels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SupportTicketApplication.Models.ViewModels
{
    /// <summary>
    /// View model for the Ticket Detail controller view. Used on the page which shows all information for a Ticket.
    /// </summary>
    public class TicketDetailViewModel
    {
        #region Properties

        /// <summary>
        /// The ticket unique identifer.
        /// </summary>
        [Required]
        public int ID { get; set; }

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

        /// <summary>
        /// Date ticket was created.
        /// </summary>      
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date created")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Ticket owner.
        /// </summary>
        [Required]        
        public string Owner { get; set; }        

        /// <summary>
        /// Status of the ticket.
        /// </summary>
        [Required]
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
            this.Status = new TicketStatus();
        }

        #endregion
    }
}