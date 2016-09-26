using DataAccess;
using EntityModels;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Repositories;

namespace SupportTicketApplication.Tests
{

    /// <summary>
    /// // TODO: The unit tests aren't configured correctly yet, but I've added a couple of unit tests as an example of the type of tests you might set up in a real world scenario
    /// </summary>
    [TestClass]
    public class TicketTests
    {

        #region Class level variables

        private ITicketRepository c_repository;

        /// <summary>
        /// Constructor - set up any variables in time for the unit tests.
        /// </summary>
        public TicketTests()
        {            
            c_repository = new TicketRepository(new DataAccess.Contexts.SupportTicketContext());  
        }

        #endregion

        #region Adding Tickets

        #region Data Access Layer Tests

        /// <summary>
        /// Tries to add a Ticket, and should be able to add a ticket.
        /// </summary>
        [TestMethod]
        public void AddTicket_ShouldAddTicket()
        {
            try
            {
                Ticket ticketToAdd = null;                
                Ticket addedTicket = null;

                // Arrange
                ticketToAdd = new Ticket();                
                ticketToAdd.Title = "Unit Test 1 - Title";
                ticketToAdd.Description = "Unit Test 1 - Description";
                ticketToAdd.Priority = EntityModels.Enums.TicketPriority.Medium;

                // Act                
                addedTicket = c_repository.AddTicket(ticketToAdd);

                // Assert
                Assert.AreSame(ticketToAdd, addedTicket);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Tries to add a Ticket, and should not be able to add a ticket due to the title being too long.
        /// </summary>
        [TestMethod]
        public void AddTicket_ShouldNotAddTicket_TitleTooLong()
        {
            try
            {
                Ticket ticketToAdd = null;
                Ticket addedTicket = null;

                // Arrange
                ticketToAdd = new Ticket();
                ticketToAdd.Title = "ExampleTitle ExampleTitle ExampleTitle ExampleTitle ExampleTitle ExampleTitle ExampleTitle Example Ex";
                ticketToAdd.Description = "Unit Test 1 - Description";
                ticketToAdd.Priority = EntityModels.Enums.TicketPriority.Medium;

                // Act                
                addedTicket = c_repository.AddTicket(ticketToAdd);

                // Assert
                Assert.AreSame(ticketToAdd, addedTicket);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // TODO: Add applicable unit test methods

        #endregion

        #endregion
    }
}
