using BeautyHub.Entities;
using BeautyHub.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyHub.Controllers
{
    [RoutePrefix("Admin")]
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Admin
        [Route("dashboard")]
        [Route()]
        public ActionResult Dashboard()
        {
            var appointments = context.Appointments
                .Where(c=>c.IsActive==true)
                .Include(c => c.Service)
                .Include(c => c.ServiceTime)
                .Include(c => c.User).ToList();

            var appointmentsToday = appointments.Where(b => b.AppointmentDate == DateTime.Today);
            var appointmentsInAMonth = appointments.Where(b => b.AppointmentDate.Month == DateTime.Today.Month);

            var appointmentsInAWeek = new List<Appointment>();
            foreach (var appointment in appointments)
            {
                if (Math.Abs(appointment.AppointmentDate.Day - DateTime.Today.Day) < 7 && ((appointment.AppointmentDate.Day > DateTime.Today.Day) == ((int)appointment.AppointmentDate.DayOfWeek > (int)DateTime.Today.DayOfWeek)))
                {
                    appointmentsInAWeek.Add(appointment);
                }
            }

            ViewBag.BookingsInAWeek = appointmentsInAWeek.ToList().Count().ToString();
            ViewBag.BookingsToday = appointmentsToday.ToList().Count().ToString();
            ViewBag.BookingsInAMonth = appointmentsInAMonth.ToList().Count().ToString();
            return View(appointmentsToday);
        }

        public ActionResult UserStatus(string id, string status)
        {

            ApplicationUser user;
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            user = userManager.FindById(id);
            switch(status)
            {
                case "deactivate":
                    user.LockoutEnabled = true;
                    user.LockoutEndDateUtc = DateTime.UtcNow.AddYears(5);
                    user.Status = "Blocked";
                    break;
                case "activate":
                    user.LockoutEnabled = false;
                    user.LockoutEndDateUtc = null;
                    user.Status = "Active";
                    break;
                default:
                    user.LockoutEnabled = false;
                    user.LockoutEndDateUtc = null;
                    user.Status = "Active";
                    break;
            }
            userManager.Update(user);
            return RedirectToAction("UserList","Users");
        }

        public ActionResult AppointmentStatus(string id)
        {
            int appointmentId = Convert.ToInt32(id);
            var appointmentToEdit = context.Appointments.SingleOrDefault(c => c.Id == appointmentId);
            if (appointmentToEdit.UserId != null)
            {
                ExpenseAndStarUpdater(appointmentToEdit.UserId, appointmentToEdit.ServiceId);
            }
            appointmentToEdit.IsServed = !appointmentToEdit.IsServed;
            context.SaveChanges();
            return Json(appointmentToEdit, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public void ExpenseAndStarUpdater(string userId,int serviceId)
        {
            Reward rewardUser=context.Rewards.SingleOrDefault(c => c.UserId == userId);
            Service service = context.Services.SingleOrDefault(c => c.Id == serviceId);
            var star = 0;

            rewardUser.Expense += service.Price;
            rewardUser.TotalExpense += service.Price;

            if (rewardUser.Expense >= 500)
            {
                star = (int)rewardUser.Expense / 500;
                rewardUser.Star += star;
                rewardUser.Expense = (int)rewardUser.Expense % 500;
                if (rewardUser.Star > 5)
                {
                    rewardUser.Expense += (rewardUser.Star - 5) * 500;
                    rewardUser.Star = 5;
                }
            }
        }

        public ActionResult Rewards(int? page)
        {
            var rewards = from reward in context.Rewards.Include(c => c.User)
                          select reward;
            return View(rewards.ToList());
        }
        public ActionResult RewardDetail(int id)
        {
            var result = context.Rewards.Include(c => c.User).SingleOrDefault(c => c.Id == id);
            return PartialView("_RewardCard", result);
        } 

        public ActionResult CollectReward(string id)
        {
            int Id = Convert.ToInt32(id);
            Reward reward = context.Rewards.SingleOrDefault(c => c.Id == Id);
            if(reward.Star<5)
            {
                
            }
            else if(reward.Star==5)
            {
                if(reward.Expense>0 && reward.Expense<500)
                {
                    reward.Star = 0;
                }
                else
                {
                    reward.Star = (int)reward.Expense / 500;
                    reward.Expense = (int)reward.Expense % 500;
                    if (reward.Star > 5)
                    {
                        reward.Expense += (reward.Star - 5) * 500;
                        reward.Star = 5;
                    }
                }
                
            }

            context.SaveChanges();
            return Json(reward,JsonRequestBehavior.AllowGet);
        }

    }
}