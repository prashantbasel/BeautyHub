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
    public class ServicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Services
        public ActionResult Index()
        {
            var services = db.Services.Include(s => s.Category);
            services.OrderByDescending(c => c.Id);

            return View(services.ToList());

        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.Where(c=>c.IsActive==true), "Id", "Name");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Service service, HttpPostedFileBase servicePic)
        {
            try
            {
                service.Image = new byte[servicePic.ContentLength];
                servicePic.InputStream.Read(service.Image, 0, servicePic.ContentLength);
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", service.CategoryId);
                return View(service);
            }

        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", service.CategoryId);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service service, HttpPostedFileBase servicePic)
        {
            try
            {
                db.Entry(service).State = EntityState.Modified;
                var serviceToEdit = db.Services.SingleOrDefault(c => c.Id == service.Id);
                if (servicePic != null)
                {
                    if (serviceToEdit != null)
                    {
                        serviceToEdit.Image = new byte[servicePic.ContentLength];
                        servicePic.InputStream.Read(serviceToEdit.Image, 0, servicePic.ContentLength);
                    }
                }
                else
                {
                    if (serviceToEdit != null)
                    {
                        serviceToEdit.Image = service.Image;

                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", service.CategoryId);
                return View(service);
            }

        }

        public ActionResult Delete(string id)
        {
            int Id = Convert.ToInt32(id);
            Service service = db.Services.SingleOrDefault(c=>c.Id==Id);
            service.IsActive = !service.IsActive;
            db.SaveChanges();
            return Json(service, JsonRequestBehavior.AllowGet);
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
