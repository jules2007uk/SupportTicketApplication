using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModels
{
    /// <summary>
    /// Class which maps to the Comment entity.
    /// </summary>
    /// <remarks></remarks>
    public class Comment
    {
        #region Properties

        /// <summary>
        /// Unique comment identifier.
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// The comment text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The comment order position.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// The date comment was created.
        /// </summary>
        public DateTime DateCreateed { get; set; }

        /// <summary>
        /// The owner of the comment.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Ticket to which the comment belongs.
        /// </summary>
        // TODO: Set FK constraint
        public int TicketID { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Comment()
        {
            this.ID = 0;
            this.Text = string.Empty;
            this.Order = 0;
            this.DateCreateed = DateTime.Now;
            this.Owner = string.Empty;
            this.TicketID = 0;
        }

        #endregion
    }
}
