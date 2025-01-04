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
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        public ActionResult Index()
        {

            var categories = db.Categories.Include(c=>c.Services);
            categories.OrderByDescending(c => c.Id);

            return View(categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category,HttpPostedFileBase categoryPic)
        {
            try
            {
                category.Image = new byte[categoryPic.ContentLength];
                categoryPic.InputStream.Read(category.Image, 0, categoryPic.ContentLength);
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(category);
            }
            
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            var base64 = Convert.ToBase64String(category.Image);
            var imageSrc = String.Format("data:image/gif;base64,{0}", base64);
            ViewBag.Image=imageSrc;

            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category, HttpPostedFileBase categoryPic)
        {
            try
            {
                db.Entry(category).State = EntityState.Modified;
                var categoryToEdit = db.Categories.SingleOrDefault(c => c.Id == category.Id);
                if (categoryPic!=null)
                {
                    if (categoryToEdit != null)
                    {
                        categoryToEdit.Image = new byte[categoryPic.ContentLength];
                        categoryPic.InputStream.Read(categoryToEdit.Image, 0, categoryPic.ContentLength);
                    }
                }
                else
                {
                    if (categoryToEdit != null)
                    {
                        categoryToEdit.Image = category.Image;
                        
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(category);
            }
        }

        public ActionResult Delete(string id)
        {
            int Id = Convert.ToInt32(id);
            Category category = db.Categories.SingleOrDefault(c=>c.Id==Id);
            category.IsActive = !category.IsActive;
            db.SaveChanges();
            return Json(category,JsonRequestBehavior.AllowGet);
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
