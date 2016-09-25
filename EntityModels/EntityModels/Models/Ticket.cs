using EntityModels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModels
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
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Ticket owner.
        /// </summary>
        [Required]
        public string Owner { get; set; }

        /// <summary>
        /// Comments made against the ticket.
        /// </summary>
        public ICollection<Comment> Comments { get; set; }        

        /// <summary>
        /// Status of the ticket.
        /// </summary>
        [Required]
        public TicketStatus Status { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor for Ticket.
        /// </summary>
        public Ticket()
        {
            this.ID = 0;
            this.Title = string.Empty;
            this.Description = string.Empty;
            this.Priority = new TicketPriority();
            this.DateCreated = DateTime.Now;
            this.Owner = string.Empty;
            this.Comments = new Collection<Comment>();            
            this.Status = TicketStatus.Pending;
        }

        #endregion
    }
}
