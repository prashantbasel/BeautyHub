using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using BeautyHub.Entities;
using BeautyHub.Models;
using Microsoft.AspNet.Identity.Owin;

namespace BeautyHub.Controllers
{
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointments
        public ActionResult Index()
        {
            var appointments = db.Appointments.Where(c=>c.IsActive==true).Include(a => a.Category)
                .Include(a => a.OfficeLocation)
                .Include(a => a.Service)
                .Include(a => a.ServiceTime)
                .Include(a => a.User);
            return View(appointments.ToList());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            var categories = db.Categories.Include(a => a.Services).ToList();
            ViewBag.CategoryId = new SelectList(db.Categories.Where(c=>c.IsActive==true), "Id", "Name");
            ViewBag.OfficeLocationId = new SelectList(db.OfficeLocations, "Id", "Address");
            ViewBag.ServiceId = new SelectList(db.Services.Where(c => c.IsActive == true), "Id", "Name");
            ViewBag.ServiceTimeId = new SelectList(db.ServiceTimes, "Id", "TimeRange");

            return View();
        }

        [NonAction]
        public string GenerateToken()
        {
            Random random = new Random();
            int length = 3;
            var rString = "";
            for (var i = 0; i < length; i++)
            {
                rString += ((char)(random.Next(65, 91))).ToString();
                rString += ((char)(random.Next(97, 123))).ToString();
                rString += ((char)(random.Next(48, 58))).ToString();
            }
            return rString;
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Appointment appointment)
        {
            try
            {
                //token generation
                while (true)
                {
                    var token = GenerateToken();

                    if (db.Appointments.Any(c => c.Token == token))
                    {
                        continue;
                    }
                    else
                    {
                        appointment.Token = token;
                        break;
                    }
                }

                if (User.Identity.IsAuthenticated)
                {
                    if (User.IsInRole("User"))
                    {
                        var user = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
                        appointment.UserId = user.Id;
                        appointment.FirstName = user.FirstName;
                        appointment.LastName = user.LastName;
                        appointment.Email = user.Email;
                        appointment.PhoneNumber = user.PhoneNumber;
                    }

                    var company = db.Companies.FirstOrDefault();
                    var officeLocation = db.OfficeLocations.Where(m => m.Id == appointment.OfficeLocationId).FirstOrDefault();
                    var serviceTime = db.ServiceTimes.Where(m => m.Id == appointment.ServiceTimeId).FirstOrDefault();
                    var service = db.Services.Where(m => m.Id == appointment.ServiceId).FirstOrDefault();
                    var fullName = appointment.FirstName + " " + appointment.LastName;

                    var subject = "Appointment Confirmed";
                    var message = "Dear " + fullName + ", \n" +
                        "You have successfully booked an appointment with " + company.Name + ". \n" +
                        "Appointment Details: \n" +
                        appointment.AppointmentDate.ToString("dd/MM/yyyy") + "\n" +
                        officeLocation.Address + "\n" +
                        serviceTime.TimeRange + "\n" +
                        service.Name + "\n" +
                        "Rs." + service.Price + "\n" +
                        "The appointment token is " + appointment.Token + "." +
                        "\n \n" +
                        "Regards, \n" +
                        company.Name ;

                    SendEmail(appointment.Email, fullName, subject, message);
                }

                db.Appointments.Add(appointment);
                db.SaveChanges();
                if (Request.IsAuthenticated)
                {
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    if (User.IsInRole("User"))
                    {
                        return RedirectToAction("Dashboard", "Users");
                    }
                }
                return RedirectToAction("Index", "Home");
                
            }
            catch
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", appointment.CategoryId);
                ViewBag.OfficeLocationId = new SelectList(db.OfficeLocations, "Id", "Address", appointment.OfficeLocationId);
                ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name", appointment.ServiceId);
                ViewBag.ServiceTimeId = new SelectList(db.ServiceTimes, "Id", "TimeRange", appointment.ServiceTimeId);
                ViewBag.Error = "Something went wrong";
                return View(appointment);
            }
        }

        [NonAction]
        public void SendEmail(string receiver, string receiverName, string subject, string message)
        {
            //var email = ConfigurationManager.AppSettings["email"];
            //var password = ConfigurationManager.AppSettings["password"];
            //var companyName = db.Companies.FirstOrDefault().Name;

            //var senderEmail = new MailAddress(email, companyName);
            //var receiverEmail = new MailAddress(receiver, receiverName);
            //var sub = subject;
            //var body = message;
            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(email, password)
            //};
            //using (var mess = new MailMessage(senderEmail, receiverEmail)
            //{
            //    Subject = subject,
            //    Body = body
            //})
            //{
            //    smtp.Send(mess);
            //}

            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;
                    client.Timeout = 10000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;

                    //string htmlString = body;
                    var hostEmail = "prashantbasel2@gmail.com";
                    var GoogleAppPassword = "ujpz uqmg wmjj fftm";
                    client.Credentials = new NetworkCredential(hostEmail, GoogleAppPassword);

                    //client.Credentials = new System.Net.NetworkCredential(hostEmail, hostPassword);
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.Priority = MailPriority.High;
                        mail.From = new MailAddress(hostEmail);
                        mail.To.Add(new MailAddress(receiver, receiverName));
                        mail.Subject = subject;
                        mail.IsBodyHtml = true;
                        mail.Body = message;
                        client.Send(mail);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ActionResult GetServices(int categoryId)
        {
            var services = db.Services.Where(c=>c.CategoryId == categoryId).ToList();

            return Json( services, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetServiceTimes(DateTime AppointmentDate)
        {
            List <ServiceTime> serviceTimes = getAvailableServiceTimes(AppointmentDate);

            return Json(serviceTimes, JsonRequestBehavior.AllowGet);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }

            if (User.IsInRole("Admin"))
            {
                ViewBag.UserId = appointment.UserId;
                ViewBag.FirstName = appointment.FirstName;
                ViewBag.LastName = appointment.LastName;
                ViewBag.Email = appointment.Email;
                ViewBag.PhoneNumber = appointment.PhoneNumber;
            }

            List<ServiceTime> serviceTimes = getAvailableServiceTimes(appointment.AppointmentDate);
            serviceTimes.Add(db.ServiceTimes.Find(appointment.ServiceTimeId));

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", appointment.CategoryId);
            ViewBag.OfficeLocationId = new SelectList(db.OfficeLocations, "Id", "Address", appointment.OfficeLocationId);
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name", appointment.ServiceId);
            ViewBag.ServiceTimeId = new SelectList(serviceTimes, "Id", "TimeRange", appointment.ServiceTimeId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Appointment appointment)
        {
            try
            {
                var user = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
                appointment.UserId = user.Id;

                if (User.IsInRole("User"))
                {
                    appointment.FirstName = user.FirstName;
                    appointment.LastName = user.LastName;
                    appointment.Email = user.Email;
                    appointment.PhoneNumber = user.PhoneNumber;
                }

                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();

                if (User.IsInRole("User"))
                {
                    return RedirectToAction("Dashboard", "Users");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", appointment.CategoryId);
                ViewBag.OfficeLocationId = new SelectList(db.OfficeLocations, "Id", "Address", appointment.OfficeLocationId);
                ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name", appointment.ServiceId);
                ViewBag.ServiceTimeId = new SelectList(db.ServiceTimes, "Id", "TimeRange", appointment.ServiceTimeId);
                return View(appointment);
            }
        }

        public ActionResult Delete(string id)
        {
            int Id = Convert.ToInt32(id);
            Appointment appointment = db.Appointments.Find(Id);
            appointment.IsActive = false;
            db.SaveChanges();
            return Json(appointment, JsonRequestBehavior.AllowGet);
        }

        // GET: Appointments/Cancel
        public ActionResult Cancel()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(Appointment appointment)
        {
            try
            {
                Appointment appointmentToCancel = db.Appointments.Where(m => m.PhoneNumber == appointment.PhoneNumber).Where(m => m.Token == appointment.Token).SingleOrDefault();
                appointmentToCancel.IsActive = false;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ViewBag.Error = "Phone Number and Token do not match";
                return View(appointment);
            }

        }
        public List<ServiceTime> getAvailableServiceTimes(DateTime AppointmentDate)
        {
            var appointments = db.Appointments.Where(c => c.AppointmentDate == AppointmentDate).Where(c => c.IsActive == true).ToList();

            List<ServiceTime> sts = db.ServiceTimes.ToList();
            List<ServiceTime> serviceTimes = db.ServiceTimes.ToList();

            foreach (var st in sts)
            {
                foreach (var appointment in appointments)
                {
                    if (appointment.ServiceTimeId == st.Id)
                    {
                        if (serviceTimes.Contains(st))
                        {
                            serviceTimes.Remove(st);
                        }
                    }
                }
            }

            return serviceTimes;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
