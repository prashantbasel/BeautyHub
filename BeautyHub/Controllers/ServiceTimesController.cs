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

namespace BeautyHub.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ServiceTimesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ServiceTimes
        public ActionResult Index()
        {
            return View(db.ServiceTimes.ToList());
        }

        // GET: ServiceTimes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceTime serviceTime = db.ServiceTimes.Find(id);
            if (serviceTime == null)
            {
                return HttpNotFound();
            }
            return View(serviceTime);
        }

        // GET: ServiceTimes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceTimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IEnumerable<ServiceTime> serviceTimes)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in serviceTimes)
                {
                    db.ServiceTimes.Add(item);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceTimes);
        }
        public ActionResult GetServiceTime(string time)
        {
            ServiceTime serviceTime = new ServiceTime();
            ViewBag.value = time;
            return PartialView("~/Views/Shared/EditorTemplates/ServiceTime.cshtml", serviceTime);
        }

        // GET: ServiceTimes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceTime serviceTime = db.ServiceTimes.Find(id);
            if (serviceTime == null)
            {
                return HttpNotFound();
            }
            return View(serviceTime);
        }

        // POST: ServiceTimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Time")] ServiceTime serviceTime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceTime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceTime);
        }

        // GET: ServiceTimes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceTime serviceTime = db.ServiceTimes.Find(id);
            if (serviceTime == null)
            {
                return HttpNotFound();
            }
            return View(serviceTime);
        }

        // POST: ServiceTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceTime serviceTime = db.ServiceTimes.Find(id);
            db.ServiceTimes.Remove(serviceTime);
            db.SaveChanges();
            return RedirectToAction("Index");
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
