using EntityModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class SupportTicketContext : DbContext
    {
        #region Properties

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comment> Comments { get; set; }

        #endregion

        #region Constructors

        public SupportTicketContext() : base("SupportTicketContext")
        {
            // create the database if it does not already exist
            Database.CreateIfNotExists();
        }

        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // remove the convention to make the table name the pluralised version of the model name
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        #endregion        
    }
}
