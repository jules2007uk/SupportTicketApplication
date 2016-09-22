using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupportTicketApplication.Models.ViewModels
{
    /// <summary>
    /// ViewModel for the Ticket entity model.
    /// </summary>
    public class TicketViewModel
    {
        #region Properties

        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        #endregion
    }
}