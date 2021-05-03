namespace GardenPlannerData.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GardenPlannerData.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GardenPlannerData.ApplicationDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Create Admin Role
            string roleName = "Admin";
            IdentityResult adminResult;

            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                adminResult = RoleManager.Create(new IdentityRole(roleName));
            }
            // Create Moderator Role
            roleName = "Moderator";
            IdentityResult moderatorResult;

            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                moderatorResult = RoleManager.Create(new IdentityRole(roleName));
            }
            // Create Moderator Role
            roleName = "User";
            IdentityResult userResult;

            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                userResult = RoleManager.Create(new IdentityRole(roleName));
            }
            //Create Admin User
            var user = new ApplicationUser();
            user.UserName = "Admin";
            user.Email = "Admin@email.com";
            string userPW = "Password0!";

            var chkUser = UserManager.Create(user, userPW);
            if (chkUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
