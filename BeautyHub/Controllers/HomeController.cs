using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeautyHub.Entities;
using BeautyHub.Models;
using BeautyHub.ViewModels;
using PagedList;

namespace BeautyHub.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var services = db.Services.Include(c => c.Category).Take(3).OrderByDescending(c=>c.Id);
            var branches = db.OfficeLocations;
            var categories = db.Categories.Include(a => a.Services).ToList();
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.OfficeLocationId = new SelectList(db.OfficeLocations, "Id", "Address");
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "Name");
            ViewBag.ServiceTimeId = new SelectList(db.ServiceTimes, "Id", "TimeRange");
            HomeViewModel model = new HomeViewModel
            {
                Services = services.ToList(),
                Locations = branches.ToList(),
                Appointment= new Appointment()
            };
            return View(model);
        }
        public ActionResult Services(int? page)
        {
            var services = from service in db.Services.Include(c => c.Category)
                           orderby service.Id descending
                           select service;

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(services.ToList().ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles ="Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Test()
        {
            return View();
        }
    }
}