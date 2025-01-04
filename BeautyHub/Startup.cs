using BeautyHub.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(BeautyHub.Startup))]
namespace BeautyHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            CreateRolesAndUsers();
        }

        public void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
            //Creating new Super Admin in the beginning
            if (!roleManager.RoleExists("SuperAdmin"))
            {
                var role = new IdentityRole();
                role.Name = "SuperAdmin";
                roleManager.Create(role);
                var Dob = new DateTime(1996, 09, 29);
                var user = new ApplicationUser
                {
                    UserName = "gurau.anil@gmail.com",
                    Email = "gurau.anil@gmail.com",
                    FirstName = "Anil",
                    LastName = "Gurau",
                    PhoneNumber = "9867137162",
                    EmailConfirmed = true,
                    DateOfBirth=Dob.Date,
                    Role=role,
                    Status="Active"
                };
                var password = "SuperAdmin@123";
                var usr = userManager.Create(user, password);

                if (usr.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "SuperAdmin");
                }
            }

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    UserName = "beauty.hobb@gmail.com",
                    Email = "beauty.hobb@gmail.com",
                    FirstName = "Beauty",
                    LastName = "Hub",
                    PhoneNumber = "9812233382",
                    EmailConfirmed = true,
                    DateOfBirth = DateTime.Now,
                    Role=role,
                    Status = "Active"
                };
                var password = "Admin@123";
                var usr = userManager.Create(user, password);

                if (usr.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Admin");
                }
            }

        }
    }
}
