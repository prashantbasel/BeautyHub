using BeautyHub.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyHub.Controllers
{
    [RoutePrefix("Users")]
    [Authorize]
    public class UsersController : Controller
    {

        ApplicationDbContext context = new ApplicationDbContext();

        [Route()]
        [Authorize(Roles = "Admin")]
        public ActionResult UserList(int? page, string searchString, string currentFilter, string sortOrder)
        {
           

            var superAdmin = context.Roles.Single(c => c.Name.ToLower() == "superadmin");
            var users = context.Users.Include(c => c.Role).Where(e => e.Roles.All(r => r.RoleId != superAdmin.Id));
           
            return View(users.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UserStatus(string id, string status)
        {

            ApplicationUser user;
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            user = userManager.FindById(id);
            switch (status)
            {
                case "Active":
                    user.LockoutEnabled = true;
                    user.LockoutEndDateUtc = DateTime.UtcNow.AddYears(5);
                    user.Status = "Blocked";
                    break;
                case "Blocked":
                    user.LockoutEnabled = false;
                    user.LockoutEndDateUtc = null;
                    user.Status = "Active";
                    break;
                default:
                    break;
            }

            userManager.Update(user);
            return Json(user,JsonRequestBehavior.AllowGet);
        }

        [Route("dashboard")]
        [Authorize(Roles = "User")]
        public ActionResult Dashboard()
        {
            var userId = User.Identity.GetUserId();
            var appointments = context.Appointments.Where(c => c.UserId == userId)
                .Where(c => c.IsActive == true)
                .Include(c => c.ServiceTime)
                .Include(c => c.Service)
                .Include(c => c.OfficeLocation);

            var totalAppointmentsCount = appointments.Count();
            var UpcomingAppointments = appointments.Where(c => c.AppointmentDate >= DateTime.Today)
                                            .Where(c => c.IsActive == true)
                                            .Where(c => c.IsServed == false);

            var UpcomingAppointmentsCount = UpcomingAppointments.Count();

            var rewards = context.Rewards.Where(c => c.UserId == userId).FirstOrDefault();
            var rewardPoints = rewards.Star;
            var ExpenseLeftForARewardPoint = 500 - rewards.Expense;

            ViewBag.NumberOfTotalAppointments = totalAppointmentsCount;
            ViewBag.NumberOfUpcomingAppointments = UpcomingAppointmentsCount;
            ViewBag.rewardPoints = rewardPoints;
            ViewBag.ExpenseLeftForARewardPoint = ExpenseLeftForARewardPoint;
            return View(UpcomingAppointments);
        }

        [Authorize(Roles = "User")]
        public ActionResult Appointments()
        {
            var userId = User.Identity.GetUserId();
            var appointments = context.Appointments.Where(c => c.UserId == userId)
                .Where(c => c.IsActive == true)
                .Include(c => c.ServiceTime)
                .Include(c => c.Service)
                .Include(c => c.OfficeLocation);

            return View(appointments);
        }
    }
}