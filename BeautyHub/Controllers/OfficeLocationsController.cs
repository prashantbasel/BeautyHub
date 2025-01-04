using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeautyHub.Entities;
using BeautyHub.Models;
using PagedList;

namespace BeautyHub.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OfficeLocationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OfficeLocations
        public ActionResult Index()
        {

            var officeLocations = from ol in db.OfficeLocations select ol;
            officeLocations.OrderByDescending(c => c.Id);

            return View(officeLocations.ToList());
        }

        // GET: OfficeLocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeLocation officeLocation = db.OfficeLocations.Find(id);
            if (officeLocation == null)
            {
                return HttpNotFound();
            }
            return View(officeLocation);
        }

        // GET: OfficeLocations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OfficeLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Address,Email,PhoneNumber,MapLink")] OfficeLocation officeLocation)
        {
            if (ModelState.IsValid)
            {
                db.OfficeLocations.Add(officeLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(officeLocation);
        }

        // GET: OfficeLocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeLocation officeLocation = db.OfficeLocations.Find(id);
            if (officeLocation == null)
            {
                return HttpNotFound();
            }
            return View(officeLocation);
        }

        // POST: OfficeLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OfficeLocation officeLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(officeLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(officeLocation);
        }

        public ActionResult Delete(string id)
        {
            int Id = Convert.ToInt32(id);
            OfficeLocation location = db.OfficeLocations.Find(Id);
            db.OfficeLocations.Remove(location);
            db.SaveChanges();
            return Json(location, JsonRequestBehavior.AllowGet);
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
