namespace DataAccess.Migrations
{
    using EntityModels;
    using EntityModels.Models.Enums;
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
        protected override void Seed(DataAccess.Contexts.SupportTicketContext context)
        {
            // 1xComment to add to Ticket 1
            Comment testComment = new Comment();
            testComment.ID = 1;
            testComment.DateCreateed = DateTime.Now;
            testComment.Order = 1;
            testComment.Owner = "Heath";
            testComment.Text = "This is my test comment";
            testComment.TicketID = 1;

            // comments to add to the 1st Ticket
            Collection<Comment> comments = new Collection<Comment>();
            comments.Add(testComment);

            // add the tickets
            context.Tickets.AddOrUpdate(                
                new Ticket { ID = 1, Comments = comments, DateCreated = DateTime.Now, Description = "My monitor is showing a strange black line.", Owner = "Harold", Priority = TicketPriority.Medium, Status = TicketStatus.Pending, Title = "Monitor issue" },
                new Ticket { ID = 2, Comments = null, DateCreated = DateTime.Now, Description = "My mouse isn't moving the cursor on the screen.", Owner = "Emily", Priority = TicketPriority.Medium, Status = TicketStatus.Pending, Title = "Mouse issue" }
            );

            // add the comments
            context.Comments.AddOrUpdate(
                testComment
            );

        }
    }
}
