namespace DataAccess.Migrations
{
    using EntityModels;
    using EntityModels.Enums;
    using System;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.Contexts.SupportTicketContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "DataAccess.Contexts.SupportTicketContext";
        }

        /// <summary>
        /// Seed the database with example data.
        /// </summary>
        /// <param name="context"></param>
        /// <remarks>This method will be called after migrating to the latest version</remarks>
        protected override void Seed(Contexts.SupportTicketContext context)
        {
            // add the tickets
            context.Tickets.AddOrUpdate(                
                new Ticket { ID = 1, DateCreated = DateTime.Now, Description = "My monitor is showing a strange black line.", Owner = "testuser1@email.com", Priority = TicketPriority.Medium, Status = TicketStatus.Pending, Title = "Monitor issue" },
                new Ticket { ID = 2, DateCreated = DateTime.Now, Description = "My mouse isn't moving the cursor on the screen.", Owner = "testuser1@email.com", Priority = TicketPriority.Medium, Status = TicketStatus.Pending, Title = "Mouse issue" }
            ); 
        }
    }
}
