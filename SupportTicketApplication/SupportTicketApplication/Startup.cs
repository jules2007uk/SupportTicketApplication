using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static SupportTicketApplication.Models.IdentityModels;

[assembly: OwinStartupAttribute(typeof(SupportTicketApplication.Startup))]
namespace SupportTicketApplication
{
    public partial class Startup
    {
        #region Methods

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // set up system roles and users
            CreateRolesandUsers();
        }

        /// <summary>
        /// This method creates Standard and Admin user roles.
        /// </summary>
        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // check if Admin user role exists, if not then create it    
            if (!roleManager.RoleExists("Admin"))
            {
                IdentityRole role = new IdentityRole();
                ApplicationUser adminSuperUser = new ApplicationUser();
                string adminSuperUserPassword = "Pa$$word0";

                // define Admin role 
                role.Name = "Admin";
                roleManager.Create(role);

                // define Admin super user to join Admin role               
                adminSuperUser.UserName = "julian.willing@live.co.uk";
                adminSuperUser.Email = "julian.willing@live.co.uk";

                // attempt to create the Admin user
                // and if succeeded, add the user to the Admin role   
                if (UserManager.Create(adminSuperUser, adminSuperUserPassword).Succeeded)
                {
                    // add the Admin super user to the Admin role
                    UserManager.AddToRole(adminSuperUser.Id, "Admin");
                }
            }

            // check if Standard user role exists  
            // if it doesn't then create it
            if (!roleManager.RoleExists("Standard"))
            {
                IdentityRole role = new IdentityRole();

                // define the Standard user role
                role.Name = "Standard";
                roleManager.Create(role);
            }
        }

        #endregion
    }
}